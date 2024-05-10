using BlogPostWebApi.Common.Mappers;
using BlogPostWebApi.DbContexts;
using BlogPostWebApi.Interfaces;
using BlogPostWebApi.Interfaces.Repositories;
using BlogPostWebApi.Interfaces.Services;
using BlogPostWebApi.Repositories;
using BlogPostWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDb"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// --> UnitOfWork
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// --> Service
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IUserService, UserService>();

//cashe
builder.Services.AddMemoryCache();

//Mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();