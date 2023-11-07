using MyLib.Models.Entities;

namespace MyLib.Models.ResponseDto;

public class PostListResponseDto
{
    public List<ForumPost>Posts { get; set; }
}