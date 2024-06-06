
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Models;
using BlogingAPI.DTO.BlogPostDtos;
using System.Collections.Generic;
using BlogingAPI.DTO.CommentsDto;
using BlogingAPI.DTO.AuthorsDtos;

namespace BlogingAPI.Controllers
{
    [Route("api/BlogPosts")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogPostsController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPost()
        {
            try
            {
                var posts = await _context.BlogPosts.ToListAsync();

                var listdto = new List<BlogPostDto>();
                foreach (var post in posts)
                {
                    // Get the author of the blog post
                    var author = await _context.Authors.FindAsync(post.AuthorId);

                    // Get the comments for the blog post
                    var comments = await _context.CommentOnPosts
                        .Where(c => c.BlogPostId == post.Id)
                        .ToListAsync();

                    var listcomments = new List<CommentForPostDto>();
                    foreach (var comment in comments)
                    {
                        // Get the author of the comment
                        //var commentAuthor = await _context.Authors.FindAsync(comment.AuthorId);

                        listcomments.Add(new CommentForPostDto()
                        {
                            Id = comment.Id,
                            Content = comment.Content,
                            //BlogPostId = post.Id,
                            //Author = commentAuthor != null ? new AuthorsDto
                            //{
                            //    Id = commentAuthor.Id,
                            //    Name = commentAuthor.Name
                            //} : null
                        });
                    }

                    listdto.Add(new BlogPostDto()
                    {
                        Id = post.Id,
                        BlogTitle = post.BlogTitle,
                        BlogContent = post.BlogContent,
                        //AuthorId = post.AuthorId,
                        Author = author != null ? new AuthorsDto
                        {
                            Id = author.Id,
                            Name = author.Name
                        } : null,
                        CommentOnPosts = listcomments
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Data retrieved successfully.",
                    Data = listdto
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all posts: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }




        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _context.BlogPosts
                    .Select(p => new BlogPostDto { Id = p.Id, BlogTitle = p.BlogTitle, BlogContent = p.BlogContent, AuthorId = p.AuthorId })
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (post == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Post not found"
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Data retrieved successfully.",
                    Data = post
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting post with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<ActionResult> CreatePost(BlogPostCreateDto postDto)
        {
            try
            {
                var blogPost = new BlogPost { BlogTitle = postDto.BlogTitle, BlogContent = postDto.BlogContent, AuthorId = postDto.AuthorId };

                _context.BlogPosts.Add(blogPost);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPostById), new { id = blogPost.Id }, new { Success = true, Message = "Post created successfully.", Data = blogPost });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating post: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, BlogPostDto postDto)
        {
            if (postDto.Id != id)
            {
                return BadRequest(new { Success = false, Message = "ID mismatch" });
            }

            var existingPost = await _context.BlogPosts
                .Include(bp => bp.CommentOnPosts)
                .FirstOrDefaultAsync(bp => bp.Id == id);

            if (existingPost == null)
            {
                return NotFound(new { Success = false, Message = "Post not found" });
            }

            existingPost.BlogTitle = postDto.BlogTitle;
            existingPost.BlogContent = postDto.BlogContent;
            existingPost.AuthorId = postDto.AuthorId;

            // Check for null CommentOnPosts and provide a default empty list if necessary
            var updatedComments = (postDto.CommentOnPosts ?? new List<CommentForPostDto>()).Select(dto => new CommentOnPost
            {
                Id = dto.Id,
                Content = dto.Content,
                //BlogPostId = dto.BlogPostId,
               // AuthorId = dto.Author.Id
            }).ToList();

            // Replace existing comments with the updated ones
            existingPost.CommentOnPosts.Clear();
            foreach (var comment in updatedComments)
            {
                existingPost.CommentOnPosts.Add(comment);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Success = true, Message = "Record updated successfully.", Data = existingPost });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency error updating post with ID {id}: {ex.Message}");
                if (!BlogPostExists(id))
                {
                    return NotFound(new { Success = false, Message = "Post not found" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating post with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

       


        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var deletePost = await _context.BlogPosts.FindAsync(id);
                if (deletePost == null)
                {
                    return NotFound(new { Success = false, Message = "Post not found" });
                }

                _context.BlogPosts.Remove(deletePost);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Success = true,
                    Message = $"Post with Id {id} is deleted successfully ",

                });


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting post with ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "An error occurred" });
            }
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
       
    }
}
