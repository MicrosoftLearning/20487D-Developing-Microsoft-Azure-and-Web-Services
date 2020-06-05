
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BlueYonder.Flights.Service.Models;

namespace BlueYonder.Flights.Service.Formatter
{
    public class ImageFormatter : OutputFormatter
    {
        public ImageFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/png"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/jpeg"));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            try
            {
                var image = new ImageUrl { Url = (string)context.Object };
                if (image == null) return;

                using (WebClient client = new WebClient())
                {
                    byte[] imageData = await client.DownloadDataTaskAsync(image.Url); ;
                    response.Body.Write(imageData);
                }

            }
            catch (Exception ex)
            {
                await response.WriteAsync("Error check the url in AircraftController");
            }

        }

    }
}
