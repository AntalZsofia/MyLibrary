﻿using MyLib.Models.Entities;
using MyLib.Models.Enums;
using MyLib.Models.RequestDto;
using MyLib.Models.ResponseDto;
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
    Task<BookDto> GetBookByIdAsync(string username, Guid id);

    Task<List<Book>> SearchBookAsync(string? query, string? username);
    Task<int> GetUserBookCount(string username);
    Task<DeleteBookResult> DeleteAllBooksAsync(string username);
    Task<ReadingStatusResult>UpdateReadingStatusAsync(ReadingStatusDto readingStatusDto, string? username);
    Task<IEnumerable<Book>> GetReadingStatusAsync(string username, ReadingStatus readingStatus);
    Task<ReviewResult>AddReviewToBookAsync(BookReviewDto bookReviewDto, string username);
    Task<List<BookReviewPrewDto>> GetAllReviewsAsync();
    Task<List<BookSearchResultDto>> GetBooksBySameAuthorAsync(string author, string title, string username,
        IConfiguration configuration);
}