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
    public partial class Test : Window
    {
        List<Question> data = new List<Question>();
        List<Question> testQuestions = new List<Question>();
        public Test()
        {
            InitializeComponent();

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

            //do zmiany. wybrać 10 losowych elementów z listy data
            testQuestions.Add(data[1]);
            //koniec losowania

            //wyświetlanie pytania
            Text_TextBlock.Text = testQuestions[0].Text;

            //do zrobienia losowanie odpowiedzi

            //wyświetlanie odpowiedzi
            AnswerA.Content = testQuestions[0].Answer1;
            AnswerB.Content = testQuestions[0].Answer2;
            AnswerC.Content = testQuestions[0].Answer3;
            AnswerD.Content = testQuestions[0].Answer4;
        }
    }
}
