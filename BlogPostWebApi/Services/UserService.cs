﻿using AutoMapper;
using BlogPostWebApi.Common.Exceptions;
using BlogPostWebApi.Common.Helpers;
using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces;
using BlogPostWebApi.Interfaces.Repositories;
using System.Net;

namespace BlogPostWebApi.Services;

public class UserService(IUnitOfWork ofWork,
                         IMapper mapper) : IUserService
{
    private readonly IUnitOfWork _ofWork = ofWork;
    private readonly IMapper _mapper = mapper;

    public async Task DeleteAsync(int id)
    {
        var user = await _ofWork.Users.GetByIdAsync(id);
        if (user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
        }

        user.IsActive = false;
        await _ofWork.Users.UpdateAsync(user);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _ofWork.Users.GetAllAsync(x => x.IsActive);
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _ofWork.Users.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var model = await _ofWork.Users.GetByIdAsync(id);
        if (model is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

        var user = (User)dto;
        user.Id = id;
        user.CreatedAt = TimeHelper.GetCurrentTime();
        user.Password = model.Password;
        user.Email = model.Email;
        user.Biography = model.Biography;
        user.UserName = model.UserName;

        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.Gender = dto.Gender;

        user.Role = model.Role;

        await _ofWork.Users.UpdateAsync(user);
        throw new StatusCodeException(HttpStatusCode.OK, "User has been updated sucessfully");
    }
}