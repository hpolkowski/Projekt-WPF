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

namespace Testy
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        int topScore = 0;
        public List<Question> testQuestions = new List<Question>();

        public Results(int result, int topScore)
        {
            InitializeComponent();

            if (topScore < result)
                this.topScore = result;
            else
                this.topScore = topScore;
            Label_Result.Content = result;
        }

        private void Button_Rozwiaz_Click(object sender, RoutedEventArgs e)
        {
            Test window = new Test(topScore);
            Close();
            window.ShowDialog();
        }

        private void Button_Zakoncz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.topScore = topScore;
            window.Label_TopScore.Content = topScore;
            Close();
            window.ShowDialog();
        }

        private void Button_Drukuj_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            FlowDocument doc = new FlowDocument(new Paragraph(new Run("Test wyboru")));
            for (int i = 0; i < testQuestions.Count; i++ )
            {
                List<String> Answers = get_Answers(i);
                doc.Blocks.Add(new Paragraph(new Run((i+1).ToString() + ". " + testQuestions[i].Text)));
                doc.Blocks.Add(new Paragraph(new Run("\t A. " + Answers[0])));
                doc.Blocks.Add(new Paragraph(new Run("\t B. " + Answers[1])));
                doc.Blocks.Add(new Paragraph(new Run("\t C. " + Answers[2])));
                doc.Blocks.Add(new Paragraph(new Run("\t D. " + Answers[3])));
            }
            doc.Name = "Test_wyboru";
            doc.FontSize = 14;
            doc.PagePadding = new Thickness(100);
            doc.ColumnGap = 0;
            doc.ColumnWidth = 500;
            IDocumentPaginatorSource idpSource = doc;
            Nullable<Boolean> print = printDialog.ShowDialog();
            if (print == true)
            {
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Drukowanie testu");
            }
        }

        private List<String> get_Answers(int QuestionNumber)
        {
            //wyświetlanie odpowiedzi
            List<String> Answers = new List<String>();
            List<String> tmpAnswers = new List<String>();
            Random rand = new Random();
            Answers.Add(testQuestions[QuestionNumber].Answer1);
            Answers.Add(testQuestions[QuestionNumber].Answer2);
            Answers.Add(testQuestions[QuestionNumber].Answer3);
            Answers.Add(testQuestions[QuestionNumber].Answer4);
            for (int l = 0; l < 4; l++)
            {
            int i = rand.Next(0, Answers.Count);
            tmpAnswers.Add(Answers[i]);
            Answers.RemoveAt(i);
            }
            return tmpAnswers;
        }
    }
}
