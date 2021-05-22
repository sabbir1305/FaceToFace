using FacesAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacesController : ControllerBase
    {
        private IFacesHelper facesHelper;

        public FacesController(IFacesHelper facesHelper)
        {
            this.facesHelper = facesHelper;
        }
        [HttpPost]
        public async Task<List<byte[]>> ReadFaces()
        {
            using (var ms = new MemoryStream(2048))
            {
                await Request.Body.CopyToAsync(ms);
                var faces = facesHelper.GetFaces(ms.ToArray());
                return faces;
            }
        }


    }
}
