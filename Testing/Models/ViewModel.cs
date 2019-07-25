using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class ViewModel
    {
        public IEnumerable<Category> Categories{ get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Theme> Themes { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

    }
}