using FluentAssertions;
using SearchAndMatch.Domain.Entities;
using SearchAndMatch.Helper;

namespace SearchAndMatch.Domain.Tests.Entities
{
    [Trait("Category", "Unit")]
    public class EngineTest
    {
        [Theory, AutoMoq]
        public void CreateNewEngine_ShouldSetInformations(Engine engineInformations)
        {

            var engine = new Engine()
            {
                Id = engineInformations.Id,
                Endpoint = engineInformations.Endpoint,
                Schema = engineInformations.Schema
            };

            engine.Id.Should().Be(engineInformations.Id);
            engine.Endpoint.Should().Be(engineInformations.Endpoint);
            engine.Schema.Should().Be(engineInformations.Schema);
        }
    }
}