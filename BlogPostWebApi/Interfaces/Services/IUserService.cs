﻿using BlogPostWebApi.Common.Utils;
using BlogPostWebApi.DTOs.Users;

namespace BlogPostWebApi.Interfaces;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllAsync(PaginationParams @params);
    Task UpdateAsync(int id, UpdateUserDto dto);
    Task DeleteAsync(int id);
}
