using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Testy
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    /// 
    public partial class Test : Window
    {
        List<Question> testQuestions = new List<Question>();
        int[] userAnswers = Enumerable.Repeat(0, N).ToArray();
        int[] userPoints = Enumerable.Repeat(0, N).ToArray();
        int QuestionNumber = 0;
        public const int N = 5;
        int topScore = 0;

        public Test(int topScore)
        {
            InitializeComponent();

            this.topScore = topScore;
            List<Question> data = new List<Question>();

            XDocument xml = XDocument.Load("Questions.xml");
            foreach (XElement element in xml.Root.Elements("Question"))
            {
                data.Add(new Question(
                    int.Parse(element.Attribute("ID").Value.ToString()),
                    element.Element("Text").Value.ToString(),
                    element.Element("Answer1").Value.ToString(),
                    element.Element("Answer2").Value.ToString(),
                    element.Element("Answer3").Value.ToString(),
                    element.Element("Answer4").Value.ToString()
                    ));
            }

            Random rand = new Random();
            //wybrać 5 losowych elementów z listy data
            for (int i = 0; i < N; i++)
            {
                int k = rand.Next(0,data.Count);
                testQuestions.Add(data[k]);
                data.RemoveAt(k);

            }
            //koniec losowania
            showQuestion();
        }

        public void showQuestion()
        {
            Button_Poprzednie.IsEnabled = QuestionNumber != 0;
            Button_Nastepne.IsEnabled = QuestionNumber != testQuestions.Count-1;

            //wyświetlanie pytania
            Text_TextBlock.Text = "Pytanie " + (QuestionNumber + 1).ToString() + ":\n" + testQuestions[QuestionNumber].Text;

            //wyświetlanie odpowiedzi
            List<String> Answers = new List<String>();
            Random rand = new Random();
            Answers.Add(testQuestions[QuestionNumber].Answer1);
            Answers.Add(testQuestions[QuestionNumber].Answer2);
            Answers.Add(testQuestions[QuestionNumber].Answer3);
            Answers.Add(testQuestions[QuestionNumber].Answer4);
            int i = rand.Next(0, Answers.Count);
            AnswerA.Content = Answers[i];
            Answers.RemoveAt(i);
            i = rand.Next(0, Answers.Count);
            AnswerB.Content = Answers[i];
            Answers.RemoveAt(i);
            i = rand.Next(0, Answers.Count);
            AnswerC.Content = Answers[i];
            Answers.RemoveAt(i);
            i = rand.Next(0, Answers.Count);
            AnswerD.Content = Answers[i];
            Answers.RemoveAt(i);
        }

        private void Button_Zakoncz_Click(object sender, RoutedEventArgs e)
        {
            saveAnswer();
            Results window = new Results(userPoints.Sum(), topScore);
            Close();
            window.ShowDialog();
        }

        private void Button_Poprzednie_Click(object sender, RoutedEventArgs e)
        {
            saveAnswer();
            QuestionNumber--;
            showQuestion();
            retriveAnswer();
        }

        private void Button_Nastepne_Click(object sender, RoutedEventArgs e)
        {
            saveAnswer();
            QuestionNumber++;
            showQuestion();
            retriveAnswer();
        }

        private void saveAnswer()
        {
            string checkedAnswer = "";
            if (AnswerA.IsChecked.Value)
            {
                userAnswers[QuestionNumber] = 1;
                checkedAnswer = AnswerA.Content.ToString();
            }
            else if (AnswerB.IsChecked.Value)
            {
                userAnswers[QuestionNumber] = 2;
                checkedAnswer = AnswerB.Content.ToString();
            }
            else if (AnswerC.IsChecked.Value)
            {
                userAnswers[QuestionNumber] = 3;
                checkedAnswer = AnswerC.Content.ToString();
            }
            else if (AnswerD.IsChecked.Value)
            {
                userAnswers[QuestionNumber] = 4;
                checkedAnswer = AnswerD.Content.ToString();
            }
            else
                userAnswers[QuestionNumber] = 0;

            if (checkedAnswer.Equals(testQuestions[QuestionNumber].Answer1))
                userPoints[QuestionNumber] = 1;
            else
                userPoints[QuestionNumber] = 0;
        }

        private void retriveAnswer()
        {
            if (userAnswers[QuestionNumber] == 1)
                AnswerA.IsChecked = true;
            else if (userAnswers[QuestionNumber] == 2)
                AnswerB.IsChecked = true;
            else if (userAnswers[QuestionNumber] == 3)
                AnswerC.IsChecked = true;
            else if (userAnswers[QuestionNumber] == 4)
                AnswerD.IsChecked = true;
            else
            {
                AnswerA.IsChecked = false;
                AnswerB.IsChecked = false;
                AnswerC.IsChecked = false;
                AnswerD.IsChecked = false;
            }
        }
    }
}
