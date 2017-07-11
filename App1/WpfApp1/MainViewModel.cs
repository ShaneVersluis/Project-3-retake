using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace WpfApp1
{
    class MainViewModel
    {
        public MainViewModel()
        {

            // setup database connection
            DBconnection dbConn = new DBconnection();

            //get data from the database defined in DBconnection
            var district = dbConn.getdistrictFromDB();
            var neighborhood = dbConn.getneighboorhoodFromDB();

            //make a new plotmodel to be used to show a linegraph

            this.CriminalityModel = new PlotModel { Title = "Criminality per year" };

            //all the vars: "" are lines on the graph
            var CrimiCharlois = new LineSeries
            {
                Title = "Charlois",
                Color = OxyColors.Black
            };
            CrimiCharlois.Points.Add(new DataPoint(2006, 330));
            CrimiCharlois.Points.Add(new DataPoint(2007, 318));
            CrimiCharlois.Points.Add(new DataPoint(2008, 303));
            CrimiCharlois.Points.Add(new DataPoint(2009, 298));
            CrimiCharlois.Points.Add(new DataPoint(2011, 343));
            CriminalityModel.Series.Add(CrimiCharlois);

            var CrimiDelfshaven = new LineSeries
            {
                Title = "Delfshaven",
                Color = OxyColors.Blue
            };
            CrimiDelfshaven.Points.Add(new DataPoint(2006, 323));
            CrimiDelfshaven.Points.Add(new DataPoint(2007, 335));
            CrimiDelfshaven.Points.Add(new DataPoint(2008, 368));
            CrimiDelfshaven.Points.Add(new DataPoint(2009, 312));
            CrimiDelfshaven.Points.Add(new DataPoint(2011, 411));
            CriminalityModel.Series.Add(CrimiDelfshaven);

            var CrimiFeijenoord = new LineSeries
            {
                Title = "Feijenoord",
                Color = OxyColors.Green
            };
            CrimiFeijenoord.Points.Add(new DataPoint(2006, 259));
            CrimiFeijenoord.Points.Add(new DataPoint(2007, 275));
            CrimiFeijenoord.Points.Add(new DataPoint(2008, 337));
            CrimiFeijenoord.Points.Add(new DataPoint(2009, 294));
            CrimiFeijenoord.Points.Add(new DataPoint(2011, 303));
            CriminalityModel.Series.Add(CrimiFeijenoord);

            var CrimiHillegersberg_Schiebroek = new LineSeries
            {
                Title = "Hillegersberg_Schiebroek",
                Color = OxyColors.Yellow
            };
            CrimiHillegersberg_Schiebroek.Points.Add(new DataPoint(2006, 295));
            CrimiHillegersberg_Schiebroek.Points.Add(new DataPoint(2007, 201));
            CrimiHillegersberg_Schiebroek.Points.Add(new DataPoint(2008, 222));
            CrimiHillegersberg_Schiebroek.Points.Add(new DataPoint(2009, 295));
            CrimiHillegersberg_Schiebroek.Points.Add(new DataPoint(2011, 286));
            CriminalityModel.Series.Add(CrimiHillegersberg_Schiebroek);


            var CrimiHoek_van_Holland = new LineSeries
            {
                Title = "Hoek_van_Holland",
                Color = OxyColors.Pink
            };
            CrimiHoek_van_Holland.Points.Add(new DataPoint(2006, 74));
            CrimiHoek_van_Holland.Points.Add(new DataPoint(2007, 59));
            CrimiHoek_van_Holland.Points.Add(new DataPoint(2008, 84));
            CrimiHoek_van_Holland.Points.Add(new DataPoint(2009, 110));
            CrimiHoek_van_Holland.Points.Add(new DataPoint(2011, 95));
            CriminalityModel.Series.Add(CrimiHoek_van_Holland);


            var CrimiIJsselmonde = new LineSeries
            {
                Title = "IJsselmonde",
                Color = OxyColors.Purple
            };
            CrimiIJsselmonde.Points.Add(new DataPoint(2006, 334));
            CrimiIJsselmonde.Points.Add(new DataPoint(2007, 362));
            CrimiIJsselmonde.Points.Add(new DataPoint(2008, 340));
            CrimiIJsselmonde.Points.Add(new DataPoint(2009, 343));
            CrimiIJsselmonde.Points.Add(new DataPoint(2011, 316));
            CriminalityModel.Series.Add(CrimiIJsselmonde);


            var CrimiKralingen_Crooswijk = new LineSeries
            {
                Title = "Kralingen_Crooswijk",
                Color = OxyColors.Orange
            };
            CrimiKralingen_Crooswijk.Points.Add(new DataPoint(2006, 429));
            CrimiKralingen_Crooswijk.Points.Add(new DataPoint(2007, 470));
            CrimiKralingen_Crooswijk.Points.Add(new DataPoint(2008, 343));
            CrimiKralingen_Crooswijk.Points.Add(new DataPoint(2009, 341));
            CrimiKralingen_Crooswijk.Points.Add(new DataPoint(2011, 431));
            CriminalityModel.Series.Add(CrimiKralingen_Crooswijk);

            var CrimiNoord = new LineSeries
            {
                Title = "Noord",
                Color = OxyColors.Turquoise
            };
            CrimiNoord.Points.Add(new DataPoint(2006, 448));
            CrimiNoord.Points.Add(new DataPoint(2007, 500));
            CrimiNoord.Points.Add(new DataPoint(2008, 463));
            CrimiNoord.Points.Add(new DataPoint(2009, 401));
            CrimiNoord.Points.Add(new DataPoint(2011, 466));
            CriminalityModel.Series.Add(CrimiNoord);

            var CrimiOverschie = new LineSeries
            {
                Title = "Overschie",
                Color = OxyColors.Magenta
            };
            CrimiOverschie.Points.Add(new DataPoint(2006, 85));
            CrimiOverschie.Points.Add(new DataPoint(2007, 55));
            CrimiOverschie.Points.Add(new DataPoint(2008, 63));
            CrimiOverschie.Points.Add(new DataPoint(2009, 51));
            CrimiOverschie.Points.Add(new DataPoint(2011, 82));
            CriminalityModel.Series.Add(CrimiOverschie);

            var CrimiHoogvliet_Pernis = new LineSeries
            {
                Title = "Hoogvliet_Pernis",
                Color = OxyColors.SaddleBrown
            };
            CrimiHoogvliet_Pernis.Points.Add(new DataPoint(2006, 22));
            CrimiHoogvliet_Pernis.Points.Add(new DataPoint(2007, 22));
            CrimiHoogvliet_Pernis.Points.Add(new DataPoint(2008, 31));
            CrimiHoogvliet_Pernis.Points.Add(new DataPoint(2009, 22));
            CrimiHoogvliet_Pernis.Points.Add(new DataPoint(2011, 28));
            CriminalityModel.Series.Add(CrimiHoogvliet_Pernis);

            var CrimiPrins_Alexander = new LineSeries
            {
                Title = "Prins_Alexander",
                Color = OxyColors.Gray
            };
            CrimiPrins_Alexander.Points.Add(new DataPoint(2006, 588));
            CrimiPrins_Alexander.Points.Add(new DataPoint(2007, 527));
            CrimiPrins_Alexander.Points.Add(new DataPoint(2008, 838));
            CrimiPrins_Alexander.Points.Add(new DataPoint(2009, 759));
            CrimiPrins_Alexander.Points.Add(new DataPoint(2011, 611));
            CriminalityModel.Series.Add(CrimiPrins_Alexander);

            var CrimiRozenburg = new LineSeries
            {
                Title = "Rozenburg",
                Color = OxyColors.Cyan
            };
            CrimiRozenburg.Points.Add(new DataPoint(2006, 0));
            CrimiRozenburg.Points.Add(new DataPoint(2007, 0));
            CrimiRozenburg.Points.Add(new DataPoint(2008, 0));
            CrimiRozenburg.Points.Add(new DataPoint(2009, 84));
            CrimiRozenburg.Points.Add(new DataPoint(2011, 38));
            CriminalityModel.Series.Add(CrimiRozenburg);

            var CrimiStadscentrum = new LineSeries
            {
                Title = "Stadscentrum",
                Color = OxyColors.Red
            };
            CrimiStadscentrum.Points.Add(new DataPoint(2006, 563));
            CrimiStadscentrum.Points.Add(new DataPoint(2007, 628));
            CrimiStadscentrum.Points.Add(new DataPoint(2008, 1179));
            CrimiStadscentrum.Points.Add(new DataPoint(2009, 571));
            CrimiStadscentrum.Points.Add(new DataPoint(2011, 721));
            CriminalityModel.Series.Add(CrimiStadscentrum);


            //make the axes and the minimum and maximum for the linegraph
            CriminalityModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 2005, Maximum = 2012 });
            CriminalityModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 1200 });


            // Create new model
            // With data gotten from the database
            this.CarModel = new PlotModel { Title = "Number of people with a car per district that has a parking garage" };
            var carBarSeries = new BarSeries
            {
                ItemsSource = dbConn.getCarData(),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}"
            };
            CarModel.Series.Add(carBarSeries);
            //make axes through the CarModel data
            CarModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "",
                ItemsSource = district
            }); ;
            
            
            // Create new model
            // With data gotten from the database
            this.PeopleModel = new PlotModel { Title = "Number of persons with a car per district" };
            var PeopleBarSeries = new BarSeries
            {
                ItemsSource = dbConn.getPeopleData(),
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}"
            };
            PeopleModel.Series.Add(PeopleBarSeries);
            
            //make axes through the PeopleModel
            PeopleModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "",
                ItemsSource = neighborhood
            }); ;
        }
        //create the plot models to be used in the WPF files
        public PlotModel CriminalityModel { get; private set; }
        public PlotModel CarModel { get; private set; }
        public PlotModel PeopleModel { get; private set; }
    }
}
