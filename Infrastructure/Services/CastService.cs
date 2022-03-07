using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;
    
    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }
    public async Task<CastDetailsModel> GetCastDetails(int id)
    {
        var cast = await _castRepository.GetById(id);

        var castDetails = new CastDetailsModel
        {
            Id = cast.Id, Gender = cast.Gender, Name = cast.Name,
            ProfilePath = cast.ProfilePath, TmdbUrl = cast.TmdbUrl
        };

        castDetails.Movies = new List<MovieModel>();
        foreach (var movie in cast.MovieCasts)
        {
            castDetails.Movies.Add(new MovieModel
            {
                Id = movie.MovieId, Title = movie.Movie.Title
            });
        }

        return castDetails;
    }
}