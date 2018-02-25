using Core.Objects;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Server.Objects
{
    class GameObject
    {
        public Dictionary<string, IEnumerable<Question>> tabeleQuestions;
        public ReadOnlyDictionary<string, IEnumerable<Question>> TableQuestions { get; private set; }
        public Timer AnswerTimer { get; private set; }
        private int secondAnswer = 0;
        private static int ANSWER_TIMER = 10;


        public GameObject()
        {
            tabeleQuestions = DB.GetQuestionsTable();
            TableQuestions = new ReadOnlyDictionary<string, IEnumerable<Question>>(tabeleQuestions);
            Respondent = null;
            AnswerTimer.Interval = 1000;
            AnswerTimer.Elapsed += AnswerTimer_Elapsed;
        }

        public void StartAnswerTimer()
        {
            secondAnswer = 0;
            AnswerTimer.Start();
        }

        private void AnswerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            secondAnswer++;
            if (secondAnswer >= ANSWER_TIMER)
            {
                AnswerTimer.Stop();
            }
        }


    }
}
