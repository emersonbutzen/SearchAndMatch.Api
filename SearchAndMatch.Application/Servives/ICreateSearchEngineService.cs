using SearchAndMatch.Application.DTOs;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.Application.Servives
{
    public interface ICreateSearchEngineService
    {
        Task EngineMatching1(EngineRequest1 engineRequest1, Engine engine);
        Task EngineMatching2(EngineRequest2 engineRequest2, Engine engine);
    }
}
