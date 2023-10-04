using MyLibrary.Server.Models.RequestDto;

namespace MyLibrary.Server.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenresAsync();
}