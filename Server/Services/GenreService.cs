using MyLibrary.Server.Models.Enums;
using MyLibrary.Server.Models.RequestDto;

namespace MyLibrary.Server.Services;

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