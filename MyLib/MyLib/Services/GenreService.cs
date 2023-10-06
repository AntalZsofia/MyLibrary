using MyLib.Models.Enums;
using MyLib.Models.RequestDto;

namespace MyLib.Services;

public class GenreService : IGenreService
{
    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
    {
        var genres = Enum.GetValues(typeof(Genre))
            .Cast<Genre>()
            .Select(g => new GenreDto
            {
                Id = (int)g,
                Name = g.ToString()
            })
            .ToList();

        return await Task.FromResult(genres);
    }
}