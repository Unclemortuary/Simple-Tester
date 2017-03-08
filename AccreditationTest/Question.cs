using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccreditationTest
{
    class Question
    {
        public string topic; //Тема вопроса
        public string question; //Текст вопроса

        private string[] Answers; //Ответы

        public Question(string t)
        {
            topic = t;
            Answers = new string[4];
        }

        public void TakeAnsw(string[] ans)
        {
            for (int i = 0; i < Answers.Length; i++)
                Answers[i] = ans[i];
        }

        public string[] GiveAnswers()
        {
            return (string[]) Answers.Clone();
        }

        public bool CheckAnsw(string a)
        {
            if (String.Equals(a, Answers[0]))
                return true;
            else
                return false;
        }

    }
}
