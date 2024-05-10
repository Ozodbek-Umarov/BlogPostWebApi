using AutoMapper;
using BlogPostWebApi.Common.Exceptions;
using BlogPostWebApi.Common.Helpers;
using BlogPostWebApi.Common.Security;
using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Common;
using BlogPostWebApi.Interfaces.Repositories;
using BlogPostWebApi.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace BlogPostWebApi.Services;

public class AccountService(IUnitOfWork unit,
                            IFileService fileService,
                            IAuthManager auth,
                            IMemoryCache cache,
                            IEmailService email,
                            IMapper mapper) : IAccountService
{
    private readonly IUnitOfWork _unit = unit;
    private readonly IFileService _fileService = fileService;
    private readonly IAuthManager _auth = auth;
    private readonly IMemoryCache _cache = cache;
    private readonly IEmailService _email = email;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> CheckCodeAsync(string email, string code)
    {
        var user = await _unit.Users.GetByEmailAsync(x => x.Email == email);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");
        if (_cache.TryGetValue(email, out var result))
        {
            if (code.Equals(result))
            {
                user.IsVerified = true;
                await _unit.Users.UpdateAsync(user);
                return true;
            }
            else
                throw new StatusCodeException(HttpStatusCode.Conflict, "Code is incorrect!");
        }
        else
            throw new StatusCodeException(HttpStatusCode.BadRequest, "Code expired!");
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _unit.Users.GetByEmailAsync(x => x.Email == dto.Email);

        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

        var storedHash = user.Password;
        var storedSalt = user.Salt;
        if (!PasswordHasher.IsEqual(storedHash, dto.Password, storedSalt))
            throw new StatusCodeException(HttpStatusCode.Conflict, "Password incorrect!");

        if (!user.IsVerified)
            throw new StatusCodeException(HttpStatusCode.BadRequest, "User is not verified!");

        return _auth.GeneratedToken(user);
    }

    public async Task RegisterAsync(AddUserDto dto)
    {
        var user = _mapper.Map<User>(dto);

        if (dto.Image is null)
            user.ImagePath = FileHelper.GetDefaultImagePath();
        else
            user.ImagePath = await _fileService.SaveImageAsync(dto.Image!);

        string salt;
        user.Password = PasswordHasher.GetHash(user.Password, out salt);
        user.Salt = salt;

        await _unit.Users.CreateAsync(user);
    }

    public async Task SendCodeAsync(string email)
    {
        var user = await _unit.Users.GetByEmailAsync(x => x.Email == email);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");
        var code = GeneratedCode();
        _cache.Set(email, code, TimeSpan.FromSeconds(60));
        await _email.SendMessageAsync(email, "Verification code!", code);
    }
    private string GeneratedCode()
        => (new Random().Next(10000, 99999)).ToString();
}
