﻿namespace MyLib.Models.ResponseDto;

public class UserProfileDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int BooksCount { get; set; }
    public DateTime ProfileCreationDate { get; set; }
   
}