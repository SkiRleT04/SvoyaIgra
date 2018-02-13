using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Objects
{
    public class Category
    {
        public Category()
        {
            Questions = new List<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}
