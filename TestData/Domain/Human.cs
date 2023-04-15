namespace TestData.Domain;

public class Human
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public Sex Sex { get; set; }
    
    public int Age { get; set; }
    
}



public enum Sex
{
    NoOne = 0,
    male = 1,
    female = 2,
}