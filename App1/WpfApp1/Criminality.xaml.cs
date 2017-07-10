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
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Criminality.xaml
    /// </summary>
    public partial class Criminality : Window
    {
        public Criminality()
        {
            InitializeComponent();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Home = new MainWindow();
            Home.Show();
            this.Close();
        }
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            Info CInfo = new Info();
            CInfo.Show();
            this.Close();
        }

        private void PwCperdistrict_Click(object sender, RoutedEventArgs e)
        {
            PwC CPwc = new PwC();
            CPwc.Show();
            this.Close();
        }

        private void Parkinglot_click(object sender, RoutedEventArgs e)
        {
            Parkinglot CParkinglot = new Parkinglot();
            CParkinglot.Show();
            this.Close();

        }
    }
}
