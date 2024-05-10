using BlogPostWebApi.DTOs.Comments;
using BlogPostWebApi.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPostWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(ICommentService commentService) : ControllerBase
{
    private readonly ICommentService _commentService = commentService;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddCommentDto dto)
    {
        await _commentService.CreateAsync(dto);
        return Ok(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _commentService.DeleteAsync(id);
        return NoContent(); 
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var comments = await _commentService.GetAllAsync();
        return Ok(comments);
    }
}
