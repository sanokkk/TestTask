using AutoMapper;
using TestData.Domain;
using TestData.Domain.HumanDto;

namespace TestData.Mapps;

public class HumanProfile: Profile
{
    public HumanProfile()
    {
        CreateMap<Human, HumanReadDto>();
    }
}