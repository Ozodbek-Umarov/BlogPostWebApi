﻿using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Interfaces;
using BlogPostWebApi.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController(IAdminService adminService,
                              IUserService userService) : ControllerBase
{
    private readonly IAdminService _adminService = adminService;
    private readonly IUserService _userService = userService;

    [HttpPost("id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeUserRoleAsync(int id)
    {
        await _adminService.ChangeUserRoleAsync(id);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromForm] UpdateUserDto dto)
    {
        await _userService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpGet("admins")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAdminAsync()
        => Ok(await _adminService.GetAllAdminAsync());
}
