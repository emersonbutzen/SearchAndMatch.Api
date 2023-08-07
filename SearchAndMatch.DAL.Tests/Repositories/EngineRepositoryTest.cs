using AutoFixture.Xunit2;
using FluentAssertions;
using SearchAndMatch.DAL.Context;
using SearchAndMatch.DAL.Repositories;
using SearchAndMatch.Domain.Entities;

namespace SearchAndMatch.DAL.Tests.Repositories
{
    [Trait("Category", "Unit")]
    public class EngineRepositoryTest
    {
        [Theory, AutoMoq]
        public async Task Given_AnEngine_ShouldAdd(
           EngineRepository sut,
           Engine engine)
        {
            var result = await sut.AddAsync(engine);

            result.Should().BeTrue();
        }

        [Theory, AutoMoq]
        public async Task Given_AnExistentEngine_ShouldUpdate(
            EngineRepository sut,
            Engine engine)
        {
            await sut.AddAsync(engine);

            engine.Endpoint = "updated";
            var result = await sut.UpdateAsync(engine);

            var getById = await sut.FindAsync(engine.Id);

            result.Should().BeTrue();
            getById.Endpoint.Should().BeEquivalentTo(engine.Endpoint);
        }

        [Theory, AutoMoq]
        public async Task Given_AnExistentEngine_ShouldDelete(
            EngineRepository sut,
            Engine engine)
        {
            await sut.AddAsync(engine);

            var delete = await sut.DeleteAsync(engine.Id);
            delete.Should().BeTrue();

            var getById = await sut.FindAsync(engine.Id);
            getById.Should().BeNull();
        }

        [Theory, AutoMoq]
        public void FindAsync_ShouldReturnCorrectEngine(
            [Frozen] SearchAndMatchContext _searchAndMatchContext,
            EngineRepository sut,
            List<Engine> Engines)
        {
            // Arrange
            _searchAndMatchContext.Engines.AddRange(Engines);
            _searchAndMatchContext.SaveChanges();
            Engine engine = Engines[0];

            // Act
            var result = sut.FindAsync(engine.Id);

            // Assert
            result.Result.Should().BeOfType<Engine>();
            result.Result.Should().BeEquivalentTo(engine);
        }

        [Theory, AutoMoq]
        public async Task Given_AnExistentEngineId_ShouldReturnEngine(
            EngineRepository sut,
            Engine engine)
        {
            await sut.AddAsync(engine);

            var result = sut.FindAsync(engine.Id);

            result.Result.Should().BeEquivalentTo(engine);
        }

        [Theory, AutoMoq]
        public void ListAsync_ShouldReturnAllEngines(
            [Frozen] SearchAndMatchContext _searchAndMatchContext,
            EngineRepository sut,
            List<Engine> engines)
        {
            // Arrange
            _searchAndMatchContext.Engines.AddRange(engines);
            _searchAndMatchContext.SaveChanges();

            // Act
            var result = sut.FindAllAsync();

            // Assert
            result.Result.Should().BeEquivalentTo(engines);
        }
    }
}
