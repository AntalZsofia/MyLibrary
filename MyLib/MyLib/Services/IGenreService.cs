using MyLib.Models.RequestDto;

namespace MyLib.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllGenresAsync();
}