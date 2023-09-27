using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Server.Models.Enums;
using MyLibrary.Server.Models.RequestDto;
using MyLibrary.Server.Models.ResponseDto;
using MyLibrary.Server.Services;

namespace MyLibrary.Server.Controllers;

    [Authorize(Roles = "User")]
public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
//Get all book
    [HttpGet]
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

    [HttpPost]
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

    [HttpPut]
    public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto, int id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;
            var result = await _bookService.UpdateBookAsync(updateBookDto, id, username!);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, new Response(){Message = "An error occured on the server."});
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var username = HttpContext.User.Identity!.Name;

            if (id <= 0)
            {
                return BadRequest("Invalid book id");
            }

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
}