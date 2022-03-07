using System.Collections;
using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class CastDetailsModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProfilePath { get; set; }
    public string Gender { get; set; }
    public string TmdbUrl { get; set; }
    public List<MovieModel> Movies { get; set; }
}