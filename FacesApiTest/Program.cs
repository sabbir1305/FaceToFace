using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacesApiTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var imagePath = @"oscars-2017.jpg";
            string urlAddress = "http://localhost:5000/api/faces";

            ImageUtility imageUtility = new ImageUtility();

            var bytes = imageUtility.ConvertToBytes(imagePath);

            List<byte[]> facelist = new List<byte[]>();

            var byteContent = new ByteArrayContent(bytes);

            byteContent.Headers.ContentType  = new System.Net.Http.Headers.MediaTypeHeaderValue("application/cotet-stream");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(urlAddress,byteContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    facelist = JsonConvert.DeserializeObject<List<byte[]>>(apiResponse);
                }
            }
            if (facelist.Count > 0)
            {
                for (int i = 0; i < facelist.Count; i++)
                {
                    imageUtility.FromBytesToImage(facelist[i], "face" + i);
                }
            }


        }
    }
}
