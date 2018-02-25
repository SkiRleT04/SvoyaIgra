using Core.Objects;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server.Objects
{
    class GameObject
    {
        public Dictionary<string, IEnumerable<Question>> tabeleQuestions;
        public ReadOnlyDictionary<string, IEnumerable<Question>> TableQuestions { get; private set; }
        public ClientObject Respondent { get; set; }
        //public Timer Timer { get; private set; }


        public GameObject()
        {
            tabeleQuestions = DB.GetQuestionsTable();
            TableQuestions = new ReadOnlyDictionary<string, IEnumerable<Question>>(tabeleQuestions);
            Respondent = null;
            //Timer.
        }


    }
}
