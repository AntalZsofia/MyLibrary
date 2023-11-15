using MyLib.Models.Entities;

namespace MyLib.Models.ResponseDto;

public class UsersResponseDto
{
    public DateTime ProfileCreationDate { get; set; }
    
    public ApplicationUser User { get; set; }
    public string Email { get; set; }
    

}