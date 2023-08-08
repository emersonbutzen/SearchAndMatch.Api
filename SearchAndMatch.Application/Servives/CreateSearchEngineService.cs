using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Application.Servives
{
    public class CreateSearchEngineService : ICreateSearchEngineService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public CreateSearchEngineService(IHttpClientFactory httpClientFactory,
            ILogger<CreateSearchEngineService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<EndpointResponse> EngineMatching1Async(EngineRequest1 engineRequest1, Engine engine)
        {
            try
            {
                _logger.LogInformation("EngineRequest : {@engineRequest}", engineRequest1);
                var httpClient = _httpClientFactory.CreateClient("Client");
                var json = JsonConvert.SerializeObject(engineRequest1);
                _logger.LogInformation("Serialized {paramName} json to send to client : {json}", nameof(engineRequest1), json);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(engine.Endpoint, data);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<EndpointResponse>(responseContent);
                return result;
            }
            catch (HttpRequestException exception)
            {
                _logger.LogWarning("Post not accepted.", exception);
                throw exception;
            }
        }

        public async Task<EndpointResponse> EngineMatching2Async(EngineRequest2 engineRequest2, Engine engine)
        {
            try
            {
                _logger.LogInformation("EngineRequest : {@engineRequest}", engineRequest2);
                var httpClient = _httpClientFactory.CreateClient("Client");
                var json = JsonConvert.SerializeObject(engineRequest2);
                _logger.LogInformation("Serialized {paramName} json to send to client : {json}", nameof(engineRequest2), json);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(engine.Endpoint, data);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<EndpointResponse>(responseContent);
                return result;
            }
            catch (HttpRequestException exception)
            {
                _logger.LogWarning("Post not accepted.", exception);
                throw exception;
            }

        }
    }
}
