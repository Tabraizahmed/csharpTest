using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Recruitment.Contracts;

namespace Recruitment.Functions
{
    public class HashFunction
    {
        private readonly HttpClient _client;
        private readonly IHashGenerator _hashGenerator;

        public HashFunction(HttpClient client, IHashGenerator hashGenerator)
        {
            _client = client;
            _hashGenerator = hashGenerator;
        }

        [FunctionName("HashFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var apiRequest = JsonConvert.DeserializeObject<HashModel>(requestBody);
            var hashValue = _hashGenerator.GenerateAsync(apiRequest.Login + apiRequest.Password);
            return new OkObjectResult(hashValue);
        }
    }
}