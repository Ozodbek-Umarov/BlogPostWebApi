using BlogPostWebApi.Common.Utils;
using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Interfaces;
using BlogPostWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{id}")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetAsync(int id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }

    [HttpGet("users")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var users = await _userService.GetAllAsync(@params);
        return Ok(users);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromForm] UpdateUserDto dto)
    {
        var id = int.Parse(HttpContext.User.FindFirst("Id")!.Value);

        await _userService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("id")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok();
    }
}
