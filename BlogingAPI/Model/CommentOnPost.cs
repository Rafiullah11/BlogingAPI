using System.ComponentModel.DataAnnotations;
using BlogApp.Data;
using BlogingAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models
{
    public class CommentOnPost
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        // Foreign key to BlogPost
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        // Foreign key to BlogPost
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }

}
