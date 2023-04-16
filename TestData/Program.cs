using Microsoft.EntityFrameworkCore;
using TestData;
using TestData.DAL;
using TestData.DAL.Implementations;
using TestData.DAL.Interfaces;
using TestData.Services.Inplementations;
using TestData.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRouting(r =>
{
    r.LowercaseUrls = true;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Pg")));

builder.Services.AddLogging(l =>
{
    l.AddConsole();
    l.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
});

builder.Services.AddScoped<IHumanRepo, HumanRepo>();
builder.Services.AddScoped<IHumanService, HumanService>();



builder.Services.AddSwaggerGen(s =>
{
    var path = Path.Combine(System.AppContext.BaseDirectory, "TestData.xml");
    s.IncludeXmlComments(path);
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Initializer.Initialize(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
