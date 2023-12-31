﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLib.Models.Entities;
using MyLib.Models.Enums;

namespace MyLib.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ForumPost> ForumPosts { get; set; }
    public DbSet<ForumReply> ForumReplies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.AuthorId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.MyBooks)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<ForumPost>()
            .HasOne(fp => fp.User)
            .WithMany(u => u.ForumPosts)
            .HasForeignKey(fp => fp.UserId);

        modelBuilder.Entity<ForumPost>()
            .HasIndex(fp => fp.DiscussionThread);

        modelBuilder.Entity<ForumReply>()
            .HasOne(fr => fr.User)
            .WithMany(u => u.ForumReplies)
            .HasForeignKey(fr => fr.UserId);

    }

    public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        // Seed roles
        if (!roleManager.Roles.Any())
        {
            var roles = new List<IdentityRole<Guid>>()
            {
                new() { Name = "User" },
                new() { Name = "Admin"}
            };

            foreach (var identityRole in roles)
            {
                await roleManager.CreateAsync(identityRole);
            }
        }

        // Seed users
        if (!userManager.Users.Any())
        {
            var newUsers = new List<ApplicationUser>()
            {
                new() { UserName = "Loci", Email = "loci@gmail.com", ProfileCreationDate = DateTime.UtcNow },
                new() { UserName = "Zsofi", Email = "zsofi@gmail.com", ProfileCreationDate = DateTime.UtcNow },
                new() { UserName = "Bianka", Email = "bianka@gmail.com", ProfileCreationDate = DateTime.UtcNow }
            };

            foreach (var newUser in newUsers)
            {
                await userManager.CreateAsync(newUser, "Abcd@1234");
                await userManager.AddToRolesAsync(newUser, new string[] { "User", "Admin" });
            }
        }
        //Seed Authors

        if (!context.Authors.Any())
        {
            var defaultAuthor = new Author()
            {
                Name = "John Doe"

            };
            context.Authors.Add(defaultAuthor);
            await context.SaveChangesAsync();
        }

        //Seed books
        if (!context.Books.Any())
        {
            var author = await context.Authors.FirstOrDefaultAsync();
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Zsofi");

            //int usersNumber = users.Count;

            var random = new Random();

            var books = new List<Book>()
            {
                new Book()
                {
                    Title = "Meet me in Milan",
                    Author = author,
                    Genre = "Mystery",
                    PublishDate = "2023-10-12",
                    User = user,
                    Description = "Test description",
                    SmallCoverImage = "link to cover image"
                }
            };
            context.Books.AddRange(books);
            await context.SaveChangesAsync();
        }

        //Seed forum posts
        if (!context.ForumPosts.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Zsofi");
            var posts = new List<ForumPost>()
            {
                new ForumPost()
                {
                    Content = "This is a post in the Questions thread.",
                    User = user,
                    DiscussionThread = "Question",
                    PostCreationDate = DateTime.UtcNow,
                    Likes = 0
                },

            };

            
            context.ForumPosts.AddRange(posts);
            await context.SaveChangesAsync();
        }

        //Seed forum replies
        if (!context.ForumReplies.Any())
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Zsofi");
            var post = await context.ForumPosts.FirstOrDefaultAsync(); 

            var replies = new List<ForumReply>()
            {
                new ForumReply()
                {
                    Reply = "This is a test reply",
                    User = user,
                    PostId = post.Id,
                    ReplyCreationDate = DateTime.UtcNow,
                    Likes = 0
                },

            };

            context.ForumReplies.AddRange(replies);
            await context.SaveChangesAsync();
        }

    }
}

