using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.RequestDto;
using MyLibrary.Server.Models.Result;

namespace MyLibrary.Server.Services;

public interface IBookService
{
    Task<BookActionResult> CreateBookAsync(CreateBookDto createBookDto, string username);
    Task<UpdateBookResult> UpdateBookAsync(UpdateBookDto updateBookDto, int id, string username);
    Task<DeleteBookResult> DeleteBookAsync(int id, string username);

    Task<IEnumerable<Book>> GetAllBooksAsync(string username);
    Task<IEnumerable<BookSearchResultDto>> SearchGoogleBooksAsync(string query, IConfiguration configuration);
}