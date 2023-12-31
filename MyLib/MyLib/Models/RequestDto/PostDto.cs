﻿using MyLib.Models.Entities;

namespace MyLib.Models.RequestDto;

public class PostDto
{
 public Guid Id { get; set; }
    public string? Content { get; set; }
    public ApplicationUser User { get; set; }
    public string? DiscussionThread { get; set; }
    public DateTime PostCreationDate { get; set; }
    public int Likes { get; set;  }

}