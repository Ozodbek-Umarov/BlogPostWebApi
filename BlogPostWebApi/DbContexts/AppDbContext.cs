﻿using BlogPostWebApi.Entities;
using BlogPostWebApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace BlogPostWebApi.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasData(new User
            {
                Id = 1,
                FullName = "Ozodbek Umarov",
                Email = "ozodchik.krasavchik@gmail.com",
                UserName = "wikko",
                Biography = "wikko",
                IsVerified = true,
                Gender = Gender.Male,
                Role = Role.Admin,
                Salt = "aa27789b-72ac-444f-991f-ebd03cc0bd65",
                Password = "908ff1b8d4c1232ab41962217563de90f3d1de262d267f0d606737905443a97a",
                ImagePath = @"..\..\wwwroot\userImages\admin.png"
            });

        modelBuilder.Entity<User>()
            .HasIndex(p => new { p.Email, p.ImagePath, p.UserName})
            .IsUnique();
    }
}
