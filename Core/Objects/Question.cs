
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Objects
{
    public class Question
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public string Answer { get; set; }
        public string Content { get; set; }
        public QuestionContentType Type {get;set;}

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public Question(int points, string answer, string content, QuestionContentType type, Category category)
        {
            Points = points;
            Answer = answer;
            Content = content;
            Type = type;
            Category = category;
        }

        public Question()
        {
        }
    }
}
