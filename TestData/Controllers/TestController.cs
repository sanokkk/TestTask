using Microsoft.AspNetCore.Mvc;
using TestData.Domain;
using TestData.Services.Interfaces;

namespace TestData.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController: ControllerBase
{
    private const int PAGE_SIZE = 4;
    private readonly IHumanService _human;
    private readonly ILogger<TestController> _logger;

    public TestController(IHumanService human, ILogger<TestController> logger)
    {
        _human = human;
        _logger = logger;
    }
    

    /// <summary>
    /// Метод, возвращающий список жителей (без возраста);
    /// Поддерживает фильтры по полу, возрасту;
    /// С пагинацией
    /// </summary>
    /// <param name="sexFilter">Пол: 1 - мужской, 2 - женский</param>
    /// <param name="ageBorderLeft">Левая граница возраста</param>
    /// <param name="ageBorderRight">Правая граница возраста</param>
    /// <param name="page">Номер страницы для пагинации: 0 по умолчанию</param>
    /// <returns></returns>
    [HttpGet("{page}")]
    public  async Task<ActionResult> GetData(Sex sexFilter = Sex.NoOne, int? ageBorderLeft = null, int? ageBorderRight = null, int page = 0)
    {
        var response = await _human.GetByFilters(sexFilter, ageBorderLeft, ageBorderRight);
        
        _logger.Log(LogLevel.Information, $"Get page {page} of humans with filters");
        
        return Ok(response.Skip(page * PAGE_SIZE).Take(PAGE_SIZE));
    }



    /// <summary>
    /// Метод, возвращающий жителя города Х по его id (содержит возраст)
    /// </summary>
    /// <param name="id">id жителя</param>
    /// <returns></returns>
    [HttpGet]
    [Route("/GetById/{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var Human = await _human.GetByIdAsync(id);
        if (Human is not null)
        {
            _logger.Log(LogLevel.Information, $"Getting citizen by ID: {id}");
            return Ok(Human);    
        }
        _logger.Log(LogLevel.Warning, "Citizen with this id not found");
        return NoContent();

    }
    
}