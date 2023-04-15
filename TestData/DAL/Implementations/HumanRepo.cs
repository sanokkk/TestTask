using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestData.DAL.Interfaces;
using TestData.Domain;
using TestData.Domain.HumanDto;

namespace TestData.DAL.Implementations;

public class HumanRepo : IHumanRepo
{
    private readonly ApplicationDbContext _db;

    public HumanRepo(ApplicationDbContext db)
    {
        _db = db;
        
    }

    

    public async Task<Human[]> GetAll()
    {
        return await _db.Humans.ToArrayAsync();
    }

    public async Task<Human> GetById(string id) => await _db.Humans.FirstOrDefaultAsync(f => f.Id == id);
}
    