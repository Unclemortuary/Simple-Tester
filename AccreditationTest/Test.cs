using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccreditationTest
{
    //Класс для конкретного теста
    class Test
    {
        public const int Count = 50; //Кол-во вопросов
        public int count 
        {
            get
            {
                return Count;
            }
        }

        private int counter, score; //Порядковый счетчик вопросов и счет пользователя
        private Question[] Q;

        public Test()
        {
            counter = 0; score = 0;
            Q = new Question[Count];
        }

        public void Generate(List<Topic> t)
        {
            //Берем по очереди из списка тем t по одному рандомному вопросу, формируя тем самым тест
            for(int i = 0, k = 0; i < Count; i++, k++)
            {
              A:  if(k == t.Count)
                {
                    k = 0;
                    Q[i] = t[k].GetRandQst();
                }
                else
                {
                    Q[i] = t[k].GetRandQst();
                }
                if (Q[i] == null) //0 возварщается, если такой вопрос уже использовался
                    goto A; // По новой
            }
        }

        public Question NextQ() //Даём следующий вопрос 
        {
            if (counter < Count)
            {
                counter++;
                return Q[counter - 1];
            }
            else
                return null;
        }

        public int GetCounter()
        {
            return counter;
        }

        public int GetScore()
        {
            return score;
        }

        public void IncreaseScore()
        {
            score++;
        }

        public Question GetQ(int i)
        {
            return Q[i];
        }
    }
}
