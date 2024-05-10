using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Interfaces.Services;
using BlogPostWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountService service) : ControllerBase
{
    private readonly IAccountService _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromForm] AddUserDto dto)
    {
        await _service.RegisterAsync(dto);
        return Ok();
    }
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromForm] LoginDto dto)
    {
        var result = await _service.LoginAsync(dto);
        return Ok($"Token : {result}");
    }
    [HttpPost("sendcode")]
    public async Task<IActionResult> SendCodeAsync([FromForm] string email)
    {
        await _service.SendCodeAsync(email);
        return Ok();
    }
    [HttpPost("check")]
    public async Task<IActionResult> CheckCodeAsync([FromForm] string email, [FromForm] string code)
        => Ok(await _service.CheckCodeAsync(email, code));
}
