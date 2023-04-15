using TestData.Domain;
using TestData.Domain.HumanDto;

namespace TestData.Services.Interfaces;

public interface IHumanService
{
    Task<HumanReadDto[]> GetByFilters(Sex sexFilter = Sex.NoOne, int? ageBorderLeft = null, int? ageBorderRight = null);
    Task<Human> GetByIdAsync(string id);
}