using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PressfordConsultingNews.Models
{
    public class Author
    {
        public Author() => Articles = new HashSet<Article>();

        public int AuthorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}