using BlogPostWebApi.Common.Exceptions;
using BlogPostWebApi.Common.Utils;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Enums;
using BlogPostWebApi.Interfaces.Repositories;
using BlogPostWebApi.Interfaces.Services;
using Newtonsoft.Json;
using System.Net;

namespace BlogPostWebApi.Services;

public class AdminService(IUnitOfWork ofWork,
                          IHttpContextAccessor accessor) : IAdminService
{
    private readonly IUnitOfWork _ofWork = ofWork;
    private readonly IHttpContextAccessor _accessor = accessor;

    public async Task ChangeUserRoleAsync(int id)
    {
        var user = await _ofWork.Users.GetByIdAsync(id);
        if (user is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

        user.Role = user.Role == Role.Admin ? Role.User : Role.Admin;

        await _ofWork.Users.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _ofWork.Users.GetByIdAsync(userId);
        if (user is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
        }

        user.IsActive = false;
        await _ofWork.Users.UpdateAsync(user);
    }

    public async Task<IEnumerable<User>> GetAllAdminAsync(PaginationParams @params)
    {
        var users = await _ofWork.Users.GetAllAsync(u => u.Role == Role.Admin && u.IsActive);
        var metadata = new PaginationMetaData(users.Count(), @params.PageIndex, @params.PageSize);
        _accessor.HttpContext?.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return users.Skip(@params.SkipCount()).Take(@params.PageSize).ToList();
    }
}
