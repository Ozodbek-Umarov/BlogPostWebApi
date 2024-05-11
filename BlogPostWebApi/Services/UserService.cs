using AutoMapper;
using BlogPostWebApi.Common.Exceptions;
using BlogPostWebApi.Common.Helpers;
using BlogPostWebApi.Common.Utils;
using BlogPostWebApi.DTOs.Users;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces;
using BlogPostWebApi.Interfaces.Repositories;
using Newtonsoft.Json;
using System.Net;

namespace BlogPostWebApi.Services;

public class UserService(IUnitOfWork ofWork,
                         IMapper mapper,
                         IHttpContextAccessor accessor) : IUserService
{
    private readonly IUnitOfWork _ofWork = ofWork;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _accessor = accessor;

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

    public async Task<IEnumerable<UserDto>> GetAllAsync(PaginationParams @params)
    {
        var users = await _ofWork.Users.GetAllAsync(x => x.IsActive);
        var metadata = new PaginationMetaData(users.Count(), @params.PageIndex, @params.PageSize);

        _accessor.HttpContext?.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return (IEnumerable<UserDto>)_mapper.Map<List<User>>(users.Skip(@params.SkipCount()).Take(@params.PageSize));
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
