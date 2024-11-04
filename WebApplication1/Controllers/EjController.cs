using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjController : ControllerBase
    {
        [HttpGet("blue")]
        public async Task<string> Cotizacion()
        {
            string responseBody = string.Empty;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dolarapi.com/v1/dolares/blue");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }

        [HttpPost("convert")]
        public async Task<string> GetSpecificQuote([FromBody] Currency currency)
        {
            string responseBody;

            string apiUrl = currency.Code switch
            {
                "Bolsa" => "https://dolarapi.com/v1/dolares/bolsa",
                "Blue" => "https://dolarapi.com/v1/dolares/blue",
                "Cripto" => "https://dolarapi.com/v1/dolares/cripto",
                _ => throw new ArgumentException("Código de moneda no válido")
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            return responseBody;
        }
    }
}