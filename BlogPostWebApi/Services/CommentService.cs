using AutoMapper;
using BlogPostWebApi.Common.Exceptions;
using BlogPostWebApi.DTOs.Comments;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Repositories;
using BlogPostWebApi.Interfaces.Services;
using System.Net;

namespace BlogPostWebApi.Services;

public class CommentService(IUnitOfWork ofWork,
                            IMapper mapper) : ICommentService
{
    private readonly IUnitOfWork _ofWork = ofWork;
    private readonly IMapper _mapper = mapper;

    public async Task CreateAsync(AddCommentDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "Comment data is required.");
        }

        var comment = _mapper.Map<Comment>(dto);

        if (comment.PostId <= 0)
        {
            throw new ArgumentException("Invalid Post ID.");
        }
        if (comment.UserId <= 0)
        {
            throw new ArgumentException("Invalid User ID.");
        }

        try
        {
            await _ofWork.Comments.CreateAsync(comment);
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, "Failed to create comment.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _ofWork.Comments.GetByIdAsync(id);
        if (comment is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Comment not found");
        }

        try
        {
            await _ofWork.Comments.DeleteAsync(comment);
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, "Failed to delete comment.");
        }
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        var comments = await _ofWork.Comments.GetAllAsync();
        return _mapper.Map<List<CommentDto>>(comments.ToList());
    }

    public async Task<CommentDto> GetByIdAsync(int id)
    {
        var comment = await _ofWork.Comments.GetByIdAsync(id);
        if (comment is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Comment not found");
        }
        return _mapper.Map<CommentDto>(comment);
    }
}
