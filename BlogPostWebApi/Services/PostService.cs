using AutoMapper;
using BlogPostWebApi.Common.Exceptions;
using BlogPostWebApi.DTOs.Posts;
using BlogPostWebApi.Entities;
using BlogPostWebApi.Interfaces.Repositories;
using BlogPostWebApi.Interfaces.Services;
using System.Net;

namespace BlogPostWebApi.Services;

public class PostService(IUnitOfWork ofWork,
                         IMapper mapper) : IPostService
{
    private readonly IUnitOfWork _ofWork = ofWork;
    private readonly IMapper _mapper = mapper;

    public async Task CreateAsync(AddPostDto dto)
    {
        var post = _mapper.Map<Post>(dto);
        await _ofWork.Posts.CreateAsync(post);
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _ofWork.Posts.GetByIdAsync(id);
        if (post is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Post not found");
        }
        post.IsActive = false;
        await _ofWork.Posts.UpdateAsync(post);
    }

    public async Task<List<PostDto>> GetAllAsync()
    {
        var posts = await _ofWork.Posts.GetAllAsync(x => x.IsActive);
        return _mapper.Map<List<PostDto>>(posts.ToList());
    }

    public async Task<PostDto> GetByIdAsync(int id)
    {
        var post = await _ofWork.Posts.GetByIdAsync(id);
        if (post is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Post not found");
        }
        return _mapper.Map<PostDto>(post);
    }

    public async Task<List<PostDto>> GetByTitleAsync(string title)
    {
        var posts = await _ofWork.Posts.GetByNameAsync(x => x.Title.Contains(title));
        return _mapper.Map<List<PostDto>>(posts.ToList());
    }

    public async Task UpdateAsync(PostDto dto)
    {
        var existingPost = await _ofWork.Posts.GetByIdAsync(dto.Id);
        if (existingPost is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Post not found");
        }

        _mapper.Map(dto, existingPost);
        existingPost.CreatedAt = existingPost.CreatedAt;

        await _ofWork.Posts.UpdateAsync(existingPost);

    }
}
