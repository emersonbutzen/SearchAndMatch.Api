using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using SearchAndMatch.DAL.Context;

namespace SearchAndMatch.DAL.Tests
{
    public class AutoMoqAttribute : AutoDataAttribute
    {
        public AutoMoqAttribute()
            : base(() => new Fixture()
               .Customize(new AutoMoqCustomization())
               .Customize(new InMemoryDbCustomization()))
        {
        }

        public class InMemoryDbCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() =>
                {
                    var options = new DbContextOptionsBuilder<SearchAndMatchContext>()
                       .UseInMemoryDatabase(fixture.Create<string>())
                       .EnableSensitiveDataLogging()
                       .Options;
                    var dbContext = new SearchAndMatchContext(options);

                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();

                    return dbContext;
                });
            }
        }
    }    
}