using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string NameCategory { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public ICollection<Theme> Themes{ get; set; }

    }
}