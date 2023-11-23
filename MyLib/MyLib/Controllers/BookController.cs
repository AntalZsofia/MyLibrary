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
                return Ok(new Response() { Message = "No books found." });
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
    //Get currently reading books
    [HttpGet("/reading-status/{status}")]
    public async Task<ActionResult<BookListResponseDto>> GetReadingStatus(ReadingStatus status)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var readingStatus = await _bookService.GetReadingStatusAsync(userName!, status);
            if (readingStatus == null)
            {
                return Ok(new Response() { Message = "No books found." });
            }

            var responseDto = new BookListResponseDto
            {
                Books = readingStatus.ToList()
            };
            return Ok(responseDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    }
    
    
    //Add book to currently reading collection
    [HttpPut("/change-reading-status/{id}")]
    public async Task<IActionResult> UpdateReadingStatus([FromBody]ReadingStatusDto readingStatusDto, Guid id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var book = await _bookService.GetBookByIdAsync(username!, id);
            if (book != null)
            {
            var addToCurrentlyReadingResult = await _bookService.UpdateReadingStatusAsync(readingStatusDto, username);
            if (!addToCurrentlyReadingResult.Succeeded)
            {
                return BadRequest(new Response() { Message = "Failed to update reading status" });

            }
           
            return Ok(new Response() { Message = "Reading status updated successfully" });
            
                
            }

            return NotFound();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred while adding the book to the currently reading collection" });
        }
    }
    
    
    
    //Get book by id
    
    [HttpGet("/get-book/{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        try
        {
            var userName = HttpContext.User.Identity!.Name;
            var book = await _bookService.GetBookByIdAsync(userName!, id);

            if (book != null)
            {
                return Ok(book);
            }
            return NotFound();
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
    
    //Delete all books
    [HttpDelete("/delete-books")]
    public async Task<IActionResult> DeleteAllBook()
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;

            var deleteBooksResult = await _bookService.DeleteAllBooksAsync(username);

            if (!deleteBooksResult.Succeeded)
            {
                if (deleteBooksResult.Error == ErrorType.BookNotFound)
                {
                    return NotFound(new Response() { Message = "Books not found" });
                }

                if (deleteBooksResult.Error == ErrorType.UserNotFound)
                {
                    return NotFound(new Response() { Message = "User not found" });
                }

                return StatusCode(500, new Response() { Message = "An error occured on the server" });
            }

            return Ok(new Response() { Message = "All books deleted" });
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
    
    
    //Search book by query
    [HttpGet("/search-book/")]
    public async Task<IActionResult> SearchBookByQuery([FromQuery] string query)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var booksQuery = await _bookService.SearchBookAsync(query, username);

            if (booksQuery.Count > 0)
            {
               
                return Ok(booksQuery);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred on the server." });
        }
    } 
    
    //Add a review and rating to book
    [HttpPut("/add-review/{id}")]
    public async Task<IActionResult> AddReviewToBookById([FromBody] BookReviewDto bookReviewDto, Guid id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var book = await _bookService.GetBookByIdAsync(username!, id);
            
            if (book != null)
            {
                var addReviewResult = await _bookService.AddReviewToBookAsync(bookReviewDto, username!);
                if (!addReviewResult.Succeeded)
                {
                    return BadRequest(new Response() { Message = "Failed to add review" });

                } 
                return Ok(new Response() { Message = "Review added successfully" });
                
            }
            return NotFound();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response() { Message = "An error occurred while adding the review" });
        }
    }
        //Get all reviews
        [HttpGet("/reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _bookService.GetAllReviewsAsync();
                if (reviews == null)
                {
                    return Ok(new Response() { Message = "No reviews found." });
                }

                return Ok(reviews);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
}