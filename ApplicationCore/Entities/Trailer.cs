namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }
    public string TrailerURL { get; set; }
    public string Name { get; set; }
    
    public int MovieId { get; set; }
    
    //Nav Property
    public Movie Movie { get; set; }
    
}