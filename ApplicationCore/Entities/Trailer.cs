namespace ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }
    public string TrailerUrl { get; set; }
    public string Name { get; set; }
    
    // reference to Movie Id FK
    public int MovieId { get; set; }
    
    // Navigation property
    public Movie Movie { get; set; }
}