using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Server.Models;
using MyLibrary.Server.Models.Entities;
using MyLibrary.Server.Models.RequestDto;
using MyLibrary.Server.Models.ResponseDto;
using MyLibrary.Server.Models.Result;

namespace MyLibrary.Server.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public BookService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<BookActionResult> CreateBookAsync(CreateBookDto createBookDto, string username)
    {
        try
        {
            var user = _userManager.FindByNameAsync(username);

            var newBook = new Book()
            {
                Author = createBookDto.Author,
                Title = createBookDto.Title,
                Genre = createBookDto.Genre,
                PublishDate = createBookDto.PublishDate

            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return BookActionResult.Succeed("Book created succesfully");
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");
        }
    }

    public async Task<UpdateBookResult> UpdateBookAsync(UpdateBookDto updateBookDto, int id, string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            var bookFound = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .Include(b => b.Title)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookFound != null)
            {
                bookFound.Author = updateBookDto.Author;
                bookFound.Title = updateBookDto.Title;
                bookFound.Genre = updateBookDto.Genre;
                bookFound.PublishDate = updateBookDto.PublishDate;


                _context.Books.Update(bookFound);
                await _context.SaveChangesAsync();

                var resultBook = new BookPreviewResponseDto()
                {
                    Author = updateBookDto.Author,
                    Title = updateBookDto.Title,
                    Genre = updateBookDto.Genre,
                    PublishDate = updateBookDto.PublishDate
                };
                return UpdateBookResult.Success(resultBook);
            }

            return UpdateBookResult.Fail();
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("There was an error on the server");
        }

    }

    public async Task<DeleteBookResult> DeleteBookAsync(int id, string username)
    {
        try
        {
            if (id <= 0 || string.IsNullOrEmpty(username))
            {
                return DeleteBookResult.ServerError(); 
            }

            var user = await _userManager.FindByNameAsync(username);
            var bookToDelete = await _context.Books
                .Include(b => b.Title)
                .FirstOrDefaultAsync(e =>  e.Id == id);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                await _context.SaveChangesAsync();
                return DeleteBookResult.Success();
            }
            return DeleteBookResult.BookNotFound();
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }
}