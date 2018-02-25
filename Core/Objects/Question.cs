
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
        public string Variant1 { get; set; }
        public string Variant2 { get; set; }
        public string Variant3 { get; set; }
        public string Variant4 { get; set; }
        public QuestionContentType Type {get;set;}
            

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public Question(int points, string answer, string content, QuestionContentType type, Category category, string variant1, string variant2, string variant3, string variant4)
        {
            Points = points;
            Answer = answer;
            Content = content;
            Type = type;
            Category = category;
            Variant1 = variant1;
            Variant2 = variant2;
            Variant3 = variant3;
            Variant4 = variant4;
        }

        public Question()
        {
        }
    }
}
