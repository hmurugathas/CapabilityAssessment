using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PressfordConsultingNews.Models
{
    public class Article
    {
        public Article()
        {

        }
        
        public int ArticleId { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Your article title must not exceed the 200 character limit.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(5000, ErrorMessage = "Your article content must not exceed the 5000 character limit.")]
        public string Content { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }
    }
}