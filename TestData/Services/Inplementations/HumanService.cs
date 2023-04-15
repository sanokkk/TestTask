using AutoMapper;
using TestData.DAL.Interfaces;
using TestData.Domain;
using TestData.Domain.HumanDto;
using TestData.Services.Interfaces;

namespace TestData.Services.Inplementations;

public class HumanService: IHumanService
{
    private readonly IMapper _mapper;
    private readonly IHumanRepo _human;

    public HumanService(IMapper mapper, IHumanRepo human)
    {
        _mapper = mapper;
        _human = human;
    }

    public async Task<HumanReadDto[]> GetByFilters(Sex sexFilter = Sex.NoOne, int? ageBorderLeft = null, int? ageBorderRight = null)
    {
        var humanList = await _human.GetAll();
        
        if (sexFilter != Sex.NoOne)
        {
            humanList = humanList.Where(s => s.Sex == sexFilter).ToArray();
        }

        if (ageBorderLeft is not null && ageBorderRight is not null)
        {
            humanList = humanList.Where(a => a.Age >= ageBorderLeft && a.Age <= ageBorderRight).ToArray();
        }
        else if (ageBorderLeft is not null)
        {
            humanList = humanList.Where(a => a.Age >= ageBorderLeft).ToArray();
        }
        else if (ageBorderRight is not null)
        {
            humanList = humanList.Where(a => a.Age <= ageBorderRight).ToArray();
        }

        return _mapper.Map<HumanReadDto[]>(humanList);
    }

    public async Task<Human> GetByIdAsync(string id) => await _human.GetById(id);

}