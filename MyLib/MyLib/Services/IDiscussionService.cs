﻿using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public interface IDiscussionService

{
 Task<ForumActionResult> CreatePostAsync(CreatePostDto createPostDto, string username);
 Task<IEnumerable<ForumPost>>GetAllPostsAsync(string username);
 Task<ForumActionResult> CreateReplyAsync(CreateReplyDto createReplyDto, Guid id, string username);
 Task<PostDto> GetPostByIdAsync(string username, Guid id);
 Task<List<ReplyDto>> GetAllReplyToPostById(string username, Guid id);
 Task<UpdatePostResult> UpdatePostAsync(UpdatePostDto updatePostDto, Guid id, string username);
 Task<ForumActionResult> DeletePostAsync(Guid id, string username);
}