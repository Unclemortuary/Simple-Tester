using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AccreditationTest
{
    public partial class Form1 : Form
    {
        Test currentTest;
        
        public List<string> files = new List<string>();
        public List<string> names = new List<string>();

        //Списки для вопросов, на которые ответили неправилньо
        public List<string> wTopics;
        public List<string> wQuestions;
        public List<string> wAnswers;

        private List<Topic> topics; //Список тем


        public Form1()
        {
            InitializeComponent();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files(*.txt)|*txt";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    files.Add(dialog.FileNames[i]);
                    names.Add(dialog.SafeFileNames[i]);
                }
            }
            else
                Environment.Exit(0);
            topics = new List<Topic>(files.Count);
            textBox1.ScrollBars = ScrollBars.Vertical;
            wTopics = new List<string>();
            wQuestions = new List<string>();
            wAnswers = new List<string>();
            currentTest = new Test();          
        }

        public void ReadFromFile(int i)
        {
            Topic t = new Topic(names[i].Replace(".txt.", ""));
            Question q;
            string line = null;
            char[] aChecker = new char[2];
            //Пробуем открыть файл
           try
           {
                //// Открываем поток для чтения
                StreamReader reader = new StreamReader(files[i]);
                StreamReader peeper = reader; //Нужен для проверки методом Peek на начало строки с ответом
                do
                {
                    //Читаем в цикле по строчкам.
                    if(peeper.Peek() == 10 || peeper.Peek() == 13) //Если enter
                    {
                        reader.ReadLine();
                    }
                    if(peeper.Peek() > 47 && peeper.Peek() < 59) //Если цифра
                    {
                        line += reader.ReadLine(); //Читаем в line
                    }
                    else
                    {
                        reader.Read(aChecker, 0, aChecker.Length); 
                        if (aChecker[0] == 'А' && aChecker[1] == ')') //Если А)
                        {
                            string[] answ = new string[4];
                            answ[0] = reader.ReadLine(); //Первый ответ правильный
                            for (int j = 1; j < answ.Length; j++)
                            {
                                //Заносим остальные ответы
                                answ[j] = reader.ReadLine();
                                answ[j] = answ[j].Remove(0, 2);
                            }
                            q = new Question(names[i].Replace(".txt", ""));
                            q.question = line; //line заносив в поле question вопроса
                            line = null;
                            q.TakeAnsw(answ); //Передаем ответы
                            t.AddQst(q); //Добавляем вопрос в список тем
                            Array.Clear(aChecker, 0, aChecker.Length);
                        }
                    }
                } while (!reader.EndOfStream);
                reader.Close();
                topics.Add(t);
            }
            catch(Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Activate();
            //Тут цикл по всем файлам
            for (int i = 0; i < files.Count; i++)
                ReadFromFile(i);
            currentTest.Generate(topics);
            GetOne();
        }

        private void GetOne() //Берем следующий вопрос из списка вопросов экземпляра Test
        {
            Random rand = new Random();
            var one = currentTest.NextQ();
            if(one != null)
            {
                l_counter.Text = currentTest.GetCounter().ToString() + "/" + currentTest.count.ToString();
                l_topic.Text = "Тема : " + one.topic;
                textBox1.Text = one.question;
                textBox1.SelectionStart = 0;
                var currAnsw = one.GiveAnswers();
                for (int i = currAnsw.Length - 1; i >= 0; i--)
                {
                    //Тут перемешиваем варианты ответа...
                    int j = rand.Next(i);
                    var temp = currAnsw[i];
                    currAnsw[i] = currAnsw[j];
                    currAnsw[j] = temp;
                }
                // ...и в отдаем в radiobutton
                radioButton1.Text = currAnsw[0];
                radioButton2.Text = currAnsw[1];
                radioButton3.Text = currAnsw[2];
                radioButton4.Text = currAnsw[3];
            }
            else
            {
                //Если вернули 0, то вопросов больше нет, показываем результаты
                ShowResults();
                button2.Enabled = true; // Кнопка "Заново"
            }
        }

        private void button1_Click(object sender, EventArgs e) //Обработчик кнопки "Ответить"
        {
            string answ = null; //Наш ответ
            int number = currentTest.GetCounter() - 1;
            //Тут берем ответ из radiobutton
            if (radioButton1.Checked)
                answ = radioButton1.Text;
            else if(radioButton2.Checked)
                answ = radioButton2.Text;
            else if(radioButton3.Checked)
                answ = radioButton3.Text;
            else if(radioButton4.Checked)
                answ = radioButton4.Text;

            if (currentTest.GetQ(number).CheckAnsw(answ)) //Сверяем с правильным
                currentTest.IncreaseScore(); //Увеличиваем счет
            else
            {
                //Если неправильный - запоминаем вопрос, чтобы в конце теста показать правильный ответ
                wTopics.Add(currentTest.GetQ(number).topic);
                wQuestions.Add(currentTest.GetQ(number).question);
                wAnswers.Add(currentTest.GetQ(number).GiveAnswers()[0]);
            }

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            button1.Enabled = false;
            GetOne();
        }

        private void ShowResults()
        {
            MessageBox.Show("Вы ответили правильно на " + currentTest.GetScore() + " из " + currentTest.count + " вопросов");
            if(wAnswers.Count > 0)
            {
                //Новая форма с "неправильными" вопросами
                Form2 results = new Form2(this);
                results.Show();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
