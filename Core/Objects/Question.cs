
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
        public string Category { get; set; }
        public int Points { get; set; }
        public int Id { get; set; }
        public string Answer { get; set; }
        public string Content { get; set; }
        public QuestionContentType Type {get;set;}
    }
}
