using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class Theme
    {
        public int Id { get; set; }

        [Required]
        public string ThemeName { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string Author { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}