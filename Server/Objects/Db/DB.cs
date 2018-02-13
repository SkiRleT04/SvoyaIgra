using Core.Enums;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Db
{
    public static class DB
    {
        public static ResponseStatus RegisterUser(User user)
        {
            using (var db = new ApplicationContext())
            {
                //if user exists
                if (db.Users.FirstOrDefault(u => u.Login == user.Login) != null)
                    return ResponseStatus.LoginIsTaken;
                db.Users.Add(user);
                if (db.SaveChanges() > 0)
                    return ResponseStatus.Ok;
                else
                    return ResponseStatus.Bad;
            }
        }

        public static ResponseStatus AuthUser(User user)
        {
            using (var db = new ApplicationContext())
            {
                if (db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password) != null)
                    return ResponseStatus.Ok;
                if (db.Users.FirstOrDefault(u => u.Login == user.Login) == null)
                    return ResponseStatus.UserDoesntExist;
                if (db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password != user.Password) != null)
                    return ResponseStatus.WrongPassword;
            }
            return ResponseStatus.Bad;
        }


        public static Dictionary<string, IEnumerable<Question>> GetQuestionsTable()
        {
            using (var db = new ApplicationContext())
            {

                List<Category> categories = db.Categories.ToList();

                for (int i = 1; i < 50; i++)
                {
                    for (int j = 1; j < 6; j++)
                    {
                        QuestionContentType t = (i * j % 2 == 0) ? QuestionContentType.Img : QuestionContentType.Text;

                        int r = new Random().Next(categories.Count);
                        db.Questions.Add(new Question(j * 300, $"Answer{i * j}", $"Content{i * j}", t, categories[r]));
                        db.SaveChanges();
                    }

                }


                var table = new Dictionary<string, IEnumerable<Question>>();
                //get 6 random category 
                /*var categories = db.Categories
                    .OrderBy(x => Guid.NewGuid())
                    .Take(6);

                //fill table questions
                foreach (var category in categories)
                {
                    List<Question> questions = new List<Question>();
                    for (int i = 1; i < 6; i++)
                    {
                        Question current = category.Questions
                       .Where(q => q.Points == (i * 300))
                       .OrderBy(x => Guid.NewGuid())
                       .FirstOrDefault();
                        questions.Add(current);
                    }
                    table.Add(category.Name, questions.ToArray());
                }
              
                return table;
                  */
                return table;
    
            }     
        }
        
    }
}
