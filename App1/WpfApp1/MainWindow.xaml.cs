﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using OxyPlot;
using OxyPlot.Series;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //do a test on the database
            DBconnection test = new DBconnection();
            test.CreateDB();
            
            //Do a test on the parser
            IParse db = new ParkingParser();
            db.ReadData();
            db.WriteToDB();
            test.InsertIntoDB();

            InitializeComponent();
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

        private void Criminality_Click(object sender, RoutedEventArgs e)
        {
            Criminality CCriminality = new Criminality();
            CCriminality.Show();
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
