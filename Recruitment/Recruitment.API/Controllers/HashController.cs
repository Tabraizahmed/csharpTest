using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Recruitment.Contracts;

namespace Recruitment.API.Controllers
{
    public class HashController : Controller
    {
        private static readonly HttpClient Client = new HttpClient();

        [Route("api/hash")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] HashModel request)
        {
            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest();
            }
            return Ok(await GetHashValue(request));
        }

        private static async Task<HashModel> GetHashValue(HashModel request)
        {
            var response = request;
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var functionResponse = await Client.PostAsync("http://localhost:7071/api/Function1", data);
            response.Hash_Value = await functionResponse.Content.ReadAsStringAsync();

            return response;
        }
    }
}