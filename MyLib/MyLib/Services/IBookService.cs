﻿using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public interface IBookService
{
    Task<BookActionResult> CreateBookAsync(CreateBookDto createBookDto, string username);
    Task<UpdateBookResult> UpdateBookAsync(UpdateBookDto updateBookDto, Guid id, string username);
    Task<DeleteBookResult> DeleteBookAsync(Guid id, string username);

    Task<IEnumerable<Book>> GetAllBooksAsync(string username);
    Task<IEnumerable<BookSearchResultDto>> SearchGoogleBooksAsync(string query, IConfiguration configuration);
    Task<AddToCollectionResult> AddToUserCollectionAsync(BookDto bookDto, string? username);
}