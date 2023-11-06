using MyLib.Models.RequestDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public interface IDiscussionService

{
 Task<ForumActionResult> CreatePostAsync(CreatePostDto createPostDto, string username);
 
}