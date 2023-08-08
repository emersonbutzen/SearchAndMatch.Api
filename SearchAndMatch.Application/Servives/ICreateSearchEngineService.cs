using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Application.Servives
{
    public interface ICreateSearchEngineService
    {
        Task<EndpointResponse> EngineMatching1Async(EngineRequest1 engineRequest1, Engine engine);
        Task<EndpointResponse> EngineMatching2Async(EngineRequest2 engineRequest2, Engine engine);
    }
}
