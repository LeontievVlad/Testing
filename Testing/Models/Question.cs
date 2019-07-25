using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string NameQuestion { get; set; }
        [Required]
        public string AnswerFirst { get; set; }
        [Required]
        public string AnswerSecond { get; set; }
        [Required]
        public string AnswerThird { get; set; }
        [Required]
        public string AnswerForty { get; set; }
        [Required]
        public string IsTrue { get; set; }

        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public int? ThemeId { get; set; }
        public Theme Theme { get; set; }

        
    }
}