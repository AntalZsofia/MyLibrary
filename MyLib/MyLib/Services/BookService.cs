using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLib.Models;
using MyLib.Models.Entities;
using MyLib.Models.Enums;
using MyLib.Models.RequestDto;
using MyLib.Models.ResponseDto;
using MyLib.Models.Result;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == createBookDto.Author);
            if (author == null)
            {
                author = new Author
                {
                    Name = createBookDto.Author
                };
            }


            var newBook = new Book()
            {
                Author = author,
                Title = createBookDto.Title,
                Genre = createBookDto.Genre,
                PublishDate = createBookDto.PublishDate,
                User = user,
                Description = createBookDto.Description,
                SmallCoverImage = createBookDto.SmallCoverImage

            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return BookActionResult.Succeed("Book created succesfully and added to the collection");
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
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == updateBookDto.Author);
            if (author == null)
            {
                author = new Author
                {
                    Name = updateBookDto.Author
                };
            }

            var bookFound = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookFound != null)
            {
                bookFound.Author = author;
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
                    Description = updateBookDto.Description,
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
                .Include(b => b.Author)
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
                .Include(b => b.Author)
                .Where(b => b.User == user)
                .ToListAsync();

            if (userBooks == null || !userBooks.Any())
            {
                return new List<Book>();
            }

            return userBooks;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }

    public async Task<BookDto> GetBookByIdAsync(string username, Guid id)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var getBook = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);


            if (getBook == null)
            {
                throw new Exception("There is no book with this id.");
            }

            var getBookById = new BookDto()
            {
                Title = getBook.Title,
                Author = getBook.Author!.Name,
                Genre = getBook.Genre,
                Description = getBook.Description,
                PublishDate = getBook.PublishDate,
                SmallCoverImage = getBook.SmallCoverImage,
                ReadingStatus = getBook.ReadingStatus,
            };

            return getBookById;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }

    public async Task<IEnumerable<BookSearchResultDto>> SearchGoogleBooksAsync(string query,
        IConfiguration configuration)
    {
        try
        {
            string? apiKey = configuration.GetSection("GoogleBooksApi")["ApiKey"];
            // Define the Google Books API endpoint
            string apiUrl = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(query)}&key={apiKey}";

            // Make the HTTP request to the Google Books API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var searchResult = JsonConvert.DeserializeObject<GoogleBooksApiResponse>(json);

                if (searchResult.Items != null && searchResult.Items.Count > 0)
                {
                    // Extract the relevant information from the API response
                    var books = new List<BookSearchResultDto>();

                    foreach (var item in searchResult.Items)
                    {
                        var book = new BookSearchResultDto
                        {
                            Title = item.VolumeInfo.Title,
                            Author =
                                item.VolumeInfo.Authors != null ? string.Join(", ", item.VolumeInfo.Authors) : null,
                            PublishDate = item.VolumeInfo.PublishedDate,
                            Genre = item.VolumeInfo.Categories != null
                                ? string.Join(", ", item.VolumeInfo.Categories)
                                : null,
                            Description = item.VolumeInfo.Description,
                            SmallCoverImage = item.VolumeInfo.ImageLinks?.SmallThumbnail
                        };

                        books.Add(book);
                    }

                    return books;
                }
            }

            // Handle API errors or no results found
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred while searching for books.");
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
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == bookDto.Author);
            if (author == null)
            {
                author = new Author
                {
                    Name = bookDto.Author
                };
            }

            var newBook = new Book
            {
                Author = author,
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                PublishDate = bookDto.PublishDate,
                User = user,
                Description = bookDto.Description,
                SmallCoverImage = bookDto.SmallCoverImage,
                
            };
            if (bookDto.ReadingStatus == ReadingStatus.NotStarted)
            {
                newBook.ReadingStatus = ReadingStatus.NotStarted;
            }
            else if (bookDto.ReadingStatus == ReadingStatus.Reading)
            {
                newBook.ReadingStatus = ReadingStatus.Reading;
                newBook.DateStarted = DateTime.UtcNow;
            }
            else
            if (bookDto.ReadingStatus == ReadingStatus.Finished)
            {
                newBook.ReadingStatus = ReadingStatus.Finished;
                if (bookDto.Rating != null)
                {
                    newBook.Rating = bookDto.Rating;
                }

                if (bookDto.Review != null)
                {
                    newBook.Review = bookDto.Review;
                }
            }
            if (user?.MyBooks != null)
            {
                var bookAlreadyInCollection = user.MyBooks
                    .FirstOrDefault(b => b.Title == bookDto.Title && b.Author == author && b.User == user);
                if (bookAlreadyInCollection != null)
                {
                    return AddToCollectionResult.Failed("Book already in the collection");
                }
            }
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

    public async Task<ReadingStatusResult> UpdateReadingStatusAsync(ReadingStatusDto readingStatusDto, string? username)
    {
        try
        {
        var user = await _userManager.FindByNameAsync(username);
        var bookToUpdate = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == readingStatusDto.Id && b.User == user);

        if (bookToUpdate != null)
        {
            bookToUpdate.ReadingStatus = readingStatusDto.ReadingStatus;
            _context.Books.Update(bookToUpdate);
        await _context.SaveChangesAsync();
        return ReadingStatusResult.Succeed("Reading status updated successfully");
        }
        return ReadingStatusResult.Failed("Book not found");
        
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }
    

    public async Task<IEnumerable<Book>> GetReadingStatusAsync(string username, ReadingStatus readingStatus)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var userBooks = await _context.Books
                .Include(b => b.Author)
                .Where(b => b.User == user)
                .Where(b => b.ReadingStatus == readingStatus && b.Review == null)
                .ToListAsync();

            if (userBooks == null || !userBooks.Any())
            {
                return new List<Book>();
            }

            return userBooks;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }
    
    
    public async Task<List<Book>> SearchBookAsync(string? query, string? username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var queryToLower = (query ?? "").ToLower();
            var filteredBooks = await _context.Books
                .Include(b => b.Author)
                .Where(b =>
                    b.Title!.ToLower().Contains(queryToLower) ||
                    b.Genre!.ToLower().Contains(queryToLower) ||
                    b.Author!.Name.ToLower().Contains(queryToLower) ||
                    b.Description!.ToLower().Contains(queryToLower)
                )
                .ToListAsync();
            
                return filteredBooks;
           // var bookDtos = books.Select(b => MapBookToDto(b)).ToList();
           // return bookDtos;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }

    public async Task<List<BookSearchResultDto>> GetBooksBySameAuthorAsync(string author, string title, string username,
        IConfiguration configuration)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            string? apiKey = configuration.GetSection("GoogleBooksApi")["ApiKey"];
            // Define the Google Books API endpoint
            string apiUrl = $"https://www.googleapis.com/books/v1/volumes?q={Uri.EscapeDataString(author)}&key={apiKey}";

            // Make the HTTP request to the Google Books API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var searchResult = JsonConvert.DeserializeObject<GoogleBooksApiResponse>(json);

                if (searchResult.Items != null && searchResult.Items.Count > 0)
                {
                    // Extract the relevant information from the API response
                    var books = new List<BookSearchResultDto>();

                    foreach (var item in searchResult.Items)
                    {
                        var book = new BookSearchResultDto
                        {
                            Title = item.VolumeInfo.Title,
                            Author = item.VolumeInfo.Authors != null ? string.Join(", ", item.VolumeInfo.Authors) : null,
                            PublishDate = item.VolumeInfo.PublishedDate,
                            Genre = item.VolumeInfo.Categories != null
                                ? string.Join(", ", item.VolumeInfo.Categories)
                                : null,
                            Description = item.VolumeInfo.Description,
                            SmallCoverImage = item.VolumeInfo.ImageLinks?.SmallThumbnail
                        };

                        books.Add(book);
                    }
                        var recommendedBooks = books
                            .Where(b => b.Author == author && b.Title != title)
                            .Take(3)
                            .ToList();
                            
                     
                        var booksDto = recommendedBooks.Select(b => new BookSearchResultDto()
                        {
                            Author = b.Author!,
                            Title = b.Title,
                            Genre = b.Genre,
                            Description = b.Description,
                            PublishDate = b.PublishDate,
                            SmallCoverImage = b.SmallCoverImage
                            
                        }).ToList();
                    return booksDto;
                    
                }
            }

            // Handle API errors or no results found
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred while searching for books.");
        }
    }

    public async Task<int> GetUserBookCount(string username)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var userBooks = await _context.Books
                .Include(b => b.Author)
                .Where(b => b.User == user)
                .ToListAsync();

            if (userBooks == null || !userBooks.Any())
            {
                return 0;
            }

            return userBooks.Count;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private BookDto MapBookToDto(Book book)
    {
        return new BookDto
        {
            
            Title = book.Title,
            Author = book.Author.Name,
            Genre = book.Genre,
            Description = book.Description,
            PublishDate = book.PublishDate,
            SmallCoverImage = book.SmallCoverImage
        };
    }
    public async Task<DeleteBookResult> DeleteAllBooksAsync(string username)
    {
        try
        {

            var user = await _userManager.FindByNameAsync(username);
            var allBooksToDelete = await _context.Books
                .Include(b => b.Author)
                .Where(u => u.User == user)
                .ToListAsync();

            if (allBooksToDelete == null)
            {
                return DeleteBookResult.BookNotFound();
            }

            _context.Books.RemoveRange(allBooksToDelete);
                await _context.SaveChangesAsync();
                return DeleteBookResult.Success();
            

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred on the server");
        }
    }

    public async Task<ReviewResult> AddReviewToBookAsync(BookReviewDto bookReviewDto, string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == bookReviewDto.Id && b.User == user);

        if (book == null)
        {
            return new ReviewResult() {Succeeded = false, Response = new Response() {Message = "Book not found"}};
        }
            book.Rating = bookReviewDto.Rating;
            book.Review = bookReviewDto.Review;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return ReviewResult.Succeed("Review added successfully");
    }

    public async Task<List<BookReviewPrewDto>> GetAllReviewsAsync()
    {
        try
        {
            var reviews = await _context.Books
                .Include(b => b.Author)
                .Where(b => b.Review != null)
                .ToListAsync();
            
            var bookReviewPrewDtos = reviews.Select(b => MapBookReviewToDto(b)).ToList();
            
            return bookReviewPrewDtos;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private BookReviewPrewDto MapBookReviewToDto(Book book)
    {
        return new BookReviewPrewDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author?.Name,
            SmallCoverImage = book.SmallCoverImage,
            Genre = book.Genre,
            Review = book.Review,
            Rating = book.Rating
        };
    }
}