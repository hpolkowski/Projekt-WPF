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
    /// Interaction logic for NewQuestion.xaml
    /// </summary>
    public partial class NewQuestion : Window
    {
        public NewQuestion()
        {
            InitializeComponent();
        }

        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {
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

            data.Add(new Question(
                data.Count, 
                Text_TextBox.Text.ToString(), 
                AnswerA_TextBox.Text.ToString(), 
                AnswerB_TextBox.Text.ToString(), 
                AnswerC_TextBox.Text.ToString(),
                AnswerD_TextBox.Text.ToString()));

            XDocument xml2 = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Plik testowy"),
                new XElement("Questions",
                    new XAttribute("count", data.Count),
                    from Q in data
                    select new XElement("Question",
                        new XAttribute("ID", Q.ID),
                        new XElement("Text", Q.Text),
                        new XElement("Answer1", Q.Answer1),
                        new XElement("Answer2", Q.Answer2),
                        new XElement("Answer3", Q.Answer3),
                        new XElement("Answer4", Q.Answer4)
                        )
                    )
                );

            xml2.Save("Questions.xml");
            Close();
        }

        private void Button_Zakoncz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Close();
            window.ShowDialog();
        }
    }
}
