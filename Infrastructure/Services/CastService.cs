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
    public CastDetailsModel GetCastDetails(int id)
    {
        var cast = _castRepository.GetById(id);

        var castDetails = new CastDetailsModel
        {
            Id = cast.Id, Gender = cast.Gender, Name = cast.Name,
            ProfilePath = cast.ProfilePath, TmdbUrl = cast.TmdbUrl
        };

        castDetails.Movies = new List<MovieDetailsModel>();
        foreach (var movie in castDetails.Movies)
        {
            castDetails.Movies.Add(new MovieDetailsModel
            {
                Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl
            });
        }

        return castDetails;
    }
}