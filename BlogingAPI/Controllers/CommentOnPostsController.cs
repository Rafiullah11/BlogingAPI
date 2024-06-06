using BlogApp.Data;
using BlogApp.Models;
using BlogingAPI.DTO.BlogPostDtos;
using BlogingAPI.DTO.CommentsDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogingAPI.Controllers
{
    [Route("api/CommentOnPosts")]
    [ApiController]
    public class CommentOnPostsController : ControllerBase
    {
        private readonly BlogContext _blogContext;

        public CommentOnPostsController(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllComment()
        {
            try
            {
                var commentPost = await _blogContext.CommentOnPosts.ToListAsync();
                var commentListDto = new List<CommentOnPostDto>();
                foreach (var comment in commentPost)
                {
                    var blogPosts = _blogContext.BlogPosts.Where(x => x.Id == comment.Id).ToList();

                    var listOfBlogDto = new List<BlogPostDto>();
                    foreach (var blog in blogPosts)
                    {
                        listOfBlogDto.Add(new BlogPostDto()
                        {
                            Id = blog.Id,
                            AuthorId = blog.AuthorId,
                            BlogTitle = blog.BlogTitle,
                            BlogContent = blog.BlogContent
                        });
                    }

                    commentListDto.Add(new CommentOnPostDto()
                    {
                        //AuthorId = comment.AuthorId,
                        Content = comment.Content,
                        BlogPostId = comment.BlogPostId,
                        Id = comment.Id,
                        BlogPost = listOfBlogDto,
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Data retrieved successfully.",
                    Data = commentListDto
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all comments: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult> GetCommentById(int id)
        {
            try
            {
                var commentDto = await _blogContext.CommentOnPosts
                    .Where(c => c.Id == id)
                    .Select(c => new CommentOnPostDto
                    {
                        Id = c.Id,
                        Content = c.Content,
                        //AuthorId = c.AuthorId,
                        BlogPostId = c.BlogPostId
                    })
                    .FirstOrDefaultAsync();

                if (commentDto == null)
                {
                    return NotFound(new { Success = false, Message = "Comment not found" });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Data retrieved successfully.",
                    Data = commentDto
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting comment with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentOnPostCreateDto>> CreateComment(CommentOnPostCreateDto commentCommentDto)
        {
            try
            {
                var comments = new CommentOnPost
                {
                    Content = commentCommentDto.Content,
                    AuthorId = commentCommentDto.AuthorId,
                    BlogPostId = commentCommentDto.BlogPostId
                };
                _blogContext.CommentOnPosts.Add(comments);
                await _blogContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCommentById), new { id = comments.Id }, new { Success = true, Message = "Comment created successfully.", Data = comments });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating comment: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, CommentOnPost comment)
        {
            if (comment.Id != id)
            {
                return BadRequest(new { Success = false, Message = "ID mismatch" });
            }

            try
            {
                var updateComment = await _blogContext.CommentOnPosts.FindAsync(id);
                if (updateComment == null)
                {
                    return NotFound(new { Success = false, Message = "Comment not found" });
                }

                updateComment.Content = comment.Content;

                try
                {
                    await _blogContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine($"Concurrency error updating comment with ID {id}: {ex.Message}");
                    if (!_blogContext.CommentOnPosts.Any(e => e.Id == id))
                    {
                        return NotFound(new { Success = false, Message = "Comment not found" });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
                    }
                }

                return Ok(new { Success = true, Message = "Comment updated successfully.", Data = updateComment });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating comment with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var deleteComment = await _blogContext.CommentOnPosts.FindAsync(id);
                if (deleteComment == null)
                {
                    return NotFound(new { Success = false, Message = "Comment not found" });
                }

                _blogContext.CommentOnPosts.Remove(deleteComment);
                await _blogContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting comment with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }
    }
}
