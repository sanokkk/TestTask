using System.Net;
using Newtonsoft.Json;
using TestData.DAL;
using TestData.Domain;

namespace TestData;

public static class Initializer
{
    public static void Initialize(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            SetData(scope.ServiceProvider.GetService<ApplicationDbContext>());
        }
    }

    private static void SetData(ApplicationDbContext db)
    {
        var client = new WebClient();
        var testStr = client.DownloadString("http://testlodtask20172.azurewebsites.net/task");

        var lst = JsonConvert.DeserializeObject<Human[]>(testStr);

        foreach (var el in lst)
        {
            var humanLink = client.DownloadString($"http://testlodtask20172.azurewebsites.net/task/{el.Id}");
            var currUserAge = JsonConvert.DeserializeObject<Human>(humanLink).Age;
            el.Age = currUserAge;
        }
        
        if (!db.Humans.Any())
        {
            Console.WriteLine("--> Adding Data");
            db.Humans.AddRange(lst);
            db.SaveChanges();    
        }
        else
            Console.WriteLine("--> Has Data");
    }
}