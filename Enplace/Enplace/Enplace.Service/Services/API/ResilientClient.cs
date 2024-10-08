using Enplace.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Enplace.Service.Services.API
{
    public class ResilientClient
    {
        public static async Task<(HttpStatusCode, T)> GetAsync<T>(Task<HttpResponseMessage> request, int allowedRetries = 0) where T : class, new()
        {
            int attempts = 1;
            var result = await request;
            T? parsedResponse = null;
            do
            {
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        try
                        {
                            parsedResponse = await JsonSerializer.DeserializeAsync<T>(await result.Content.ReadAsStreamAsync());
                        }
                        catch
                        {
                            return (HttpStatusCode.InternalServerError, new T());
                        }
                        if (parsedResponse is not null)
                        {
                            return (HttpStatusCode.OK, parsedResponse);
                        }
                        else
                        {
                            return (HttpStatusCode.InternalServerError, new T());
                        }
                    default:
                        result = await request;
                        break;
                }
            } while (attempts < allowedRetries);
            return (result.StatusCode, new T());
        }
    }
}
