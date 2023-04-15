using TestData.Domain;
using TestData.Domain.HumanDto;

namespace TestData.DAL.Interfaces;

public interface IHumanRepo
{
    Task<Human[]> GetAll();

    Task<Human> GetById(string id);
}