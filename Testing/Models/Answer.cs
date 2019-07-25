using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public string PersonName { get; set; }

        public string AnswerName { get; set; }

        public int? QuestionId { get; set; }

        public int? ThemeId { get; set; }

        public int? CategoryId { get; set; }
    }
}