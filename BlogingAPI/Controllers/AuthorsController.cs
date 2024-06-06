using BlogApp.Data;
using BlogApp.Models;
using BlogingAPI.DTO.AuthorsDtos;
using BlogingAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogingAPI.Controllers
{
    [Route("api/Authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BlogContext _blogContext;

        public AuthorsController(BlogContext blogContext)
        {
           _blogContext = blogContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorsDto>>> GetAuthors()
        {
            try
            {
                var authors = await _blogContext.Authors
                    .Select(a => new AuthorsDto { Id = a.Id, Name = a.Name })
                    .ToListAsync();

                return Ok(new
                {
                    Success = true,
                    Message = "Data retrieved successfully.",
                    Data = authors
                });
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error getting authors: {ex.Message}");

                // Return a 500 Internal Server Error response with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Success = false, Message = "An error occurred while retrieving authors." });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorsDto>> GetAuthorById(int id)
        {
            try
            {
                var author = await _blogContext.Authors
                                               .Select(a => new AuthorsDto { Id = a.Id, Name = a.Name })
                                               .FirstOrDefaultAsync(a => a.Id == id);

                if (author == null)
                {
                    return NotFound(new
                    {
                        Success = false,
                        Message = "Author not found"
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Data retrieved successfully.",
                    Data = author
                });
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine($"Error getting author with ID {id}: {ex.Message}");

                // Return a 500 Internal Server Error response with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Success = false,
                        Message = "An error occurred while retrieving the author."
                    });
            }
        }

        [HttpPost]
        public async Task<ActionResult<AuthorsCreateDto>> PostAuthor(AuthorsCreateDto authorDto)
        {
            try
            {
                var author = new Author { Name = authorDto.Name };
                _blogContext.Authors.Add(author);
                await _blogContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, new { Success = true, Message = "Author created successfully.", Data = author });
               
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine($"Error creating author: {ex.Message}");

                // Return a 500 Internal Server Error response with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Success = false, Message = "An error occurred while creating the author." });
            }
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorsDto authorDto)
        {
            if (authorDto.Id != id)
            {
                return BadRequest(new { Success = false, Message = "ID mismatch" });
            }

            var updateRecord = await _blogContext.Authors.FindAsync(id);
            if (updateRecord == null)
            {
                return NotFound(new { Success = false, Message = "Author not found" });
            }

            updateRecord.Name = authorDto.Name;

            try
            {
                await _blogContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsRecordExist(id))
                {
                    return NotFound(new { Success = false, Message = "Author not found during update" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine($"Error updating author with ID {id}: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Success = false, Message = "An error occurred while updating the author" });
            }

            return Ok(new
            {
                Success = true,
                Message = "Author updated successfully",
                Data = updateRecord
            });
        }

        private bool IsRecordExist(int id)
        {
            return _blogContext.Authors.Any(e => e.Id == id);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleteAuthor = await _blogContext.Authors.FindAsync(id);
            if (deleteAuthor == null)
            {
                return NotFound(new { Success = false, Message = "Author not found" });
            }

            _blogContext.Authors.Remove(deleteAuthor);

            try
            {
                await _blogContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine($"Error deleting author with ID {id}: {ex.Message}");

                // Return a 500 Internal Server Error response with a custom error message
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Success = false, Message = "An error occurred while deleting the author" });
            }

            return Ok(new
            {
                Success = true,
                Message = $"Author Id {id} deleted successfully ",
               
            });
        }


    }
}
