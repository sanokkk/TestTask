using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestData.DAL;
using TestData.DAL.Implementations;
using TestData.Domain;

namespace TestData.Tests.Repository;

public class HumanRepoTest
{
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new ApplicationDbContext(options);
        var rnd = new Random();
        databaseContext.Database.EnsureCreated();
        if (!databaseContext.Humans.Any())
        {
            for (int i = 0; i < 13; i++)
            {
                databaseContext.Humans.Add(new Human()
                {
                    Id = i.ToString(),
                    Age = rnd.Next(10, 80),
                    Name = Guid.NewGuid().ToString().Substring(0, rnd.Next(5, 12)) 
                           + " " + Guid.NewGuid().ToString().Substring(0, rnd.Next(5, 12)),
                    Sex = (Sex)rnd.Next(1,2)
                });
                await databaseContext.SaveChangesAsync();
            }
        }

        return databaseContext;
    }

    [Fact]
    public async void HumanRepo_GetHumans_ReturnsHumans()
    {
        //Arrange
        var db = await GetDatabaseContext();
        var humanRepo = new HumanRepo(db);
        //Act
        var result = await humanRepo.GetAll();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Human[]>();
    }

    [Fact]
    public async void HumanRepo_GetHumanById_ReturnsHuman()
    {
        //Arrange
        var id = "1";
        var db = await GetDatabaseContext();
        var humanRepo = new HumanRepo(db);
        
        //Act
        var result = await humanRepo.GetById(id);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Human>();
    }
}