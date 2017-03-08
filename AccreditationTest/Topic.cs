using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccreditationTest
{
    class Topic
    {
        public string topicName;
        public List<int> used; // Индексы вопросов, которые уже использовались в этом тесте

        private List<Question> questions; //Все вопросы темы

        public Topic(string a)
        {
            topicName = a;
            questions = new List<Question>();
            used = new List<int>();
        }

        public void AddQst(Question q)
        {
            questions.Add(q);
        }

        public Question GetRandQst()
        {
            Random rnd = new Random();
            int i = rnd.Next(questions.Count);
            if (used.Contains(i))
                return null;
            else
            {
                used.Add(i);
                return questions[i];
            }
        }
    }
}
