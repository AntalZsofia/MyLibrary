using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyLib.Models.Enums;
using MyLib.Models.RequestDto;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;
using MyLib.Services;

namespace MyLib.Controllers;

[EnableCors("_myAllowSpecificOrigins")]
[Authorize]//(Roles = "User")          
[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IConfiguration _configuration;
    private readonly IGenreService _genreService;

    public BookController(IBookService bookService, IConfiguration configuration, IGenreService genreService)
    {
        _bookService = bookService;
        _configuration = configuration;
        _genreService = genreService;
    }
//Get all book
    [HttpGet("/all-books")]
    public async Task<ActionResult<BookListResponseDto>> GetAllBook()
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var allBooks = await _bookService.GetAllBooksAsync(userName!);
            if (allBooks == null)
            {
                return NotFound(new Response() { Message = "No books found." });
            }

            var responseDto = new BookListResponseDto
            {
                Books = allBooks.ToList()
            };
            return Ok(responseDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }

//Create new book

    [HttpPost("/create-book")]
    public async Task<ActionResult> CreateBook(CreateBookDto createBookDto)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            
                
            
            var createBookResult = await _bookService.CreateBookAsync(createBookDto, userName!);

            if (createBookResult.Succeeded)
            {
                return Ok(createBookResult.Response);
            }
            else
            {
                return BadRequest(new Response() { Message = "Could not create book" });
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
        }
    }
    
    //Update book

    [HttpPut("/update-book/{id}")]
    public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto, Guid id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var updateBookResult = await _bookService.UpdateBookAsync(updateBookDto, id, username!);

            if (updateBookResult.Succeeded)
            {
                return Ok(updateBookResult.Data);
            }
            return BadRequest(updateBookResult.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
        }
    }

    //Delete a book
    [HttpDelete("/delete-book/{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            
            var deleteBookResult = await _bookService.DeleteBookAsync(id, username!);

            if (!deleteBookResult.Succeeded)
            {
                if (deleteBookResult.Error == ErrorType.BookNotFound)
                {
                    return NotFound(new Response() { Message = "Book not found" });
                }

                if (deleteBookResult.Error == ErrorType.UserNotFound)
                {
                    return NotFound(new Response() { Message = "User not found" });
                }

                return StatusCode(500, new Response() { Message = "An error occured on the server" });
            }

            return Ok(new Response() { Message = "Book deleted" });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occured on the server" });
        }
    }
    
    //Search books on Google
    [HttpGet("/search-book-with-google")]
    public async Task<ActionResult<SearchBookResult>>SearchGoogleBooks([FromQuery] string query)
    {
        try
        {
            var searchResult = await _bookService.SearchGoogleBooksAsync(query, _configuration);

            var bookSearchResultDtos = searchResult as BookSearchResultDto[] ?? searchResult.ToArray();
            if (!bookSearchResultDtos.Any())
            {
                return SearchBookResult.Fail();
            }

            return SearchBookResult.Success(bookSearchResultDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred while searching for books." });
        }
    }
    [HttpPost("/add-to-collection")]
    public async Task<IActionResult> AddToCollection([FromBody] BookDto bookDto)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            
            var addToCollectionResult = await _bookService.AddToUserCollectionAsync(bookDto, username);

            if (!addToCollectionResult.Succeeded)
            {
                return BadRequest(new Response() { Message = "Failed to add the book to the collection" });

            }
            else
            {
               return Ok(new Response() { Message = "Book added to collection successfully" });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred while adding the book to the collection" });
        }
    }
    
    //Get all genres
    [HttpGet("/genres")]
    public async Task<ActionResult<IEnumerable<GenreDto>>>GetGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return Ok(genres);
    }
}