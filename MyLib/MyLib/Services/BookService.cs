using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;
using MyLib.Models.Result.GoogleSearchResult;
using Newtonsoft.Json;

namespace MyLib.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly HttpClient _httpClient = new HttpClient();

    public BookService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<BookActionResult> CreateBookAsync(CreateBookDto createBookDto, string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);

            var newBook = new Book()
            {
                Author = createBookDto.Author,
                Title = createBookDto.Title,
                Genre = createBookDto.Genre,
                PublishDate = createBookDto.PublishDate,
                User = user,
                Description = createBookDto.Description,
                SmallCoverImage = createBookDto.SmallCoverImage

            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return BookActionResult.Succeed("Book created succesfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occured on the server");
        }
    }

    public async Task<UpdateBookResult> UpdateBookAsync(UpdateBookDto updateBookDto, Guid id, string username)
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
                bookFound.Description = updateBookDto.Description;
                bookFound.SmallCoverImage = updateBookDto.SmallCoverImage;


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

    public async Task<DeleteBookResult> DeleteBookAsync(Guid id, string username)
    {
        try
        {
            if (string.IsNullOrEmpty(username))
            {
                return DeleteBookResult.ServerError();
            }

            var user = await _userManager.FindByNameAsync(username);
            var bookToDelete = await _context.Books
                .Include(b => b.Title)
                .FirstOrDefaultAsync(e => e.Id == id);
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

    public async Task<IEnumerable<Book>> GetAllBooksAsync(string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var userBooks = await _context.Books
                .Where(b => b.User == user)
                .ToListAsync();

            if (userBooks == null || !userBooks.Any())
            {
                throw new Exception("User has no books.");
            }

            return userBooks;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }

    public async Task<IEnumerable<BookSearchResultDto>> SearchGoogleBooksAsync(string query, IConfiguration configuration)
    {
        {
            try
            {
                string? apiKey = configuration.GetSection("GoogleBooksApi")["ApiKey"];
                // Define the Google Books API endpoint
                string apiUrl = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(query)}&={apiKey}";

                // Make the HTTP request to the Google Books API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var searchResult = JsonConvert.DeserializeObject<GoogleBookSearchResult>(json);

                    // Extract the relevant information from the API response
                    var books = searchResult?.Items?.Select(item => new BookSearchResultDto
                    {
                        Title = item.VolumeInfo.Title,
                        Author = item.VolumeInfo.Authors.FirstOrDefault(),
                        PublishedDate = item.VolumeInfo.PublishedDate,
                        Genre = item.VolumeInfo.Categories.FirstOrDefault(),
                        Description = item.VolumeInfo.Description,
                        SmallCoverImage = item.VolumeInfo.ImageLinks,
                    });

                    return books ?? Enumerable.Empty<BookSearchResultDto>();
                }
                else
                {
                    throw new Exception("Google Books API request failed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("An error occurred while searching for books.");
            }
        }
    }

    public async Task<AddToCollectionResult> AddToUserCollectionAsync(BookDto bookDto, string? username)
    {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return AddToCollectionResult.Failed("User not found");
                }
                var user = await _userManager.FindByNameAsync(username);
                // Create a new Book entity and populate it with data from the BookDto
                var newBook = new Book
                {
                    Author = bookDto.Author,
                    Title = bookDto.Title,
                    Genre = bookDto.Genre,
                    PublishDate = bookDto.PublishDate,
                    User = user,
                    Description = bookDto.Description,
                    SmallCoverImage = bookDto.SmallCoverImage
                };

                // Add the book to the database
                await _context.Books.AddAsync(newBook);
                await _context.SaveChangesAsync();

                return AddToCollectionResult.Succeed("Book added to collection successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return AddToCollectionResult.Failed("An error occurred while adding the book to the collection");
            }
        
    }
}