﻿using Faces.WebMVC.Models;
using Faces.WebMVC.ViewModels;
using MassTransit;
using Messaging.InterfacesConstants.Commands;
using Messaging.InterfacesConstants.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Faces.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusControl _busControl;

        public HomeController(ILogger<HomeController> logger , IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        [HttpGet]
        public IActionResult RegisterOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrder(OrderVM model)
        {
            using MemoryStream memory = new MemoryStream();
            using (var uploadFile = model.File.OpenReadStream())
            {
                await uploadFile.CopyToAsync(memory);
            }
            model.ImageData = memory.ToArray();
            model.ImageUrl = model.File.FileName;
            model.OrderId = Guid.NewGuid();

            var sendToUri = new Uri($"{RabbitMqMassTransitConstants.RabbitMqUrl}" 
                + $"{RabbitMqMassTransitConstants.RegisterOrderServiceQueue}");
            var endPoint = await _busControl.GetSendEndpoint(sendToUri);
            await endPoint.Send<IRegisterOrderCommand>(new
            {
                model.OrderId,
                model.UserEmail,
                model.ImageData,
                model.ImageUrl
            });

            return View("Thanks");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
