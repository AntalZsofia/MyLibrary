using MyLib.Models.Entities;
using MyLib.Models.RequestDto;
using MyLib.Models.Result;

namespace MyLib.Services;

public interface IDiscussionService

{
 Task<ForumActionResult> CreatePostAsync(CreatePostDto createPostDto, string username);
 Task<IEnumerable<ForumPost>>GetAllPostsAsync(string username);

}