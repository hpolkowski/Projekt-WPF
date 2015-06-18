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
    }
}
