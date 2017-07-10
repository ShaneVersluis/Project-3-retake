using System;
using MySql.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;
using OxyPlot;
using OxyPlot.Series;


namespace WpfApp1
{
    public class DBconnection : IConnection
    {
        //making a new MySqlConnection varible called conn to use to interact with the database
        public MySqlConnection conn = new MySqlConnection();

        List<string> district = new List<string>();
        List<string> neighborhood = new List<string>();

        public void CreateDB()
        {
            //This first checks if the table already exists if it does drop it.
            //If it's dropped or not present it will create a new table
            string sqlQuery = @"DROP TABLE IF EXISTS parking;
                                DROP TABLE IF EXISTS public_transport;
                                DROP TABLE IF EXISTS car_possession;
                                DROP TABLE IF EXISTS criminality;
                                DROP TABLE IF EXISTS people;
                                

                                CREATE TABLE parking 
                                ( 
                                    latitude float(32), 
                                    longitude float(32), 
                                    name varchar(32),
                                    type varchar(32),
                                    district varchar(32)
                                );
                                 CREATE TABLE car_possession
                                (
                                    neighborhood varchar(69),
                                    total int
                                );
                                CREATE TABLE criminality
                                (
                                    neighborhood varchar(69),
                                    year_2006 int,
                                    year_2007 int,
                                    year_2008 int,
                                    year_2009 int,
                                    year_2011 int
                                );

                                CREATE TABLE people
                                (
                                    neighborhood varchar(69),
                                    total int
                                );";
            this.dbCommand(sqlQuery);
        }
        // This inserts data into the Database
        public void InsertIntoDB()
        {
            //query to be inserted into the Database
            string sqlQuery = @"INSERT INTO car_possession
                                VALUES ('Stadscentrum', 40),
		                                ('Delfshaven', 30),
		                                ('Overschie', 58),
		                                ('Kralingen_Crooswijk', 42),
		                                ('Noord', 38),
		                                ('Hillegersberg_Schiebroek', 56),
		                                ('Prins_Alexander', 48),
		                                ('Feijenoord', 32),
		                                ('IJsselmonde', 46),
		                                ('Charlois', 43),
		                                ('Hoogvliet_Pernis', 42),
		                                ('Hoek_van_Holland', 49);
                                
                                INSERT INTO criminality
                                VALUES  ('Charlois', 330, 318, 303, 298, 343),
                                        ('Delfshaven', 323, 335, 386, 312, 411),
                                        ('Feijenoord', 259, 275, 337, 294, 303),
                                        ('Hillegersberg_Schiebroek', 295, 201, 222, 295, 286),
                                        ('Hoek_van_Holland', 74, 59, 84, 110, 95),
                                        ('IJsselmonde', 334, 362, 340, 343, 316),
                                        ('Kralingen_Crooswijk', 429, 470, 343, 341, 431),
                                        ('Noord', 448, 500, 463, 401, 466),
                                        ('Overschie', 85, 55, 63, 51, 82),
                                        ('Hoogvliet_Pernis', 22, 22, 31, 22, 28),
                                        ('Prins_Alexander', 588, 527, 838, 759, 611),
                                        ('Rozenburg', -1, -1, -1, 84, 38),
                                        ('Stadscentrum', 563, 628, 1179, 571, 721);



                                INSERT INTO people
                                VALUES  ('Charlois', 66180),
                                        ('Delfshaven', 75445),
                                        ('Feijenoord', 73490),
                                        ('Hillegersberg_Schiebroek', 43580),
                                        ('Hoek_van_Holland', 10155),
                                        ('IJsselmonde', 59630),
                                        ('Kralingen_Crooswijk', 53165),
                                        ('Noord', 51690),
                                        ('Overschie', 6710),
                                        ('Hoogvliet_Pernis', 4785),
                                        ('Prins_Alexander', 94600),
                                        ('Rozenburg', 12420),
                                        ('Stadscentrum', 32925);";
            this.dbCommand(sqlQuery);
        }

        // This opens the Connection to the Database
        public void OpenConnection()
        {
            //this is the connection data to make a connection to the Database
            conn.ConnectionString = "SERVER = 127.0.0.1;" +
                                    "Database = dbproject3retake;" +
                                    "User ID = root;" +
                                    "Password = root;";
            //opens the connection
            conn.Open();
        }
        // this closes the connection to the Database
        public void CloseConnection()
        {
            conn.Close();
        }
        // this sends the Query tot the database
        // through the use of the parameter name = sqlQuery
        public void dbCommand(string sqlQuery)
        {
            this.OpenConnection();
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            command.ExecuteNonQuery();
            this.CloseConnection();
        }

        public List<string> getdistrictFromDB()
        {
            string sqlQuery = "SELECT DISTINCT(district) FROM parking;";

            this.OpenConnection();
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                district.Add(reader.GetString(0));
            }

            district.Sort();

            this.CloseConnection();

            return district;
        }

        public List<string> getneighboorhoodFromDB()
        {
            string sqlQuery = "SELECT DISTINCT(neighborhood) FROM car_possession;";

            this.OpenConnection();
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                neighborhood.Add(reader.GetString(0));
            }

            neighborhood.Sort();

            this.CloseConnection();

            return neighborhood;
        }

        public List<BarItem> getPeopleData()
        {
            List<BarItem> peopleData = new List<BarItem>();
            string sqlQuery = @"SELECT car_possession.neighborhood, ROUND((people.total + car_possession.total)/ COUNT(car_possession.neighborhood)) AS 'people_w/_cars'
                                FROM car_possession, people
                                WHERE car_possession.neighborhood = people.neighborhood
                                GROUP BY car_possession.neighborhood;";
            this.OpenConnection();
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                peopleData.Add(new BarItem { Value = reader.GetDouble(1) });
            }

            this.CloseConnection();

            return peopleData;
        }
        // gets a list of people with a car per neighborhood.
        // this gets stored in a list called carData
        public List<BarItem> getCarData()
        {
            //creates a new list called carData
            List<BarItem> carData = new List<BarItem>();
            string sqlQuery = @"SELECT car_possession.neighborhood, ROUND((people.total / car_possession.total)* COUNT(car_possession.neighborhood)) AS 'people_w/_cars_per_district'
                                FROM car_possession, people,parking
                                WHERE car_possession.neighborhood = people.neighborhood
                                GROUP BY parking.district;";
            //opens connection to DB
            this.OpenConnection();
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            var reader = command.ExecuteReader();

            //while reader.read add data to the list
            while (reader.Read())
            {
                carData.Add(new BarItem { Value = reader.GetDouble(1) });
            }
            //close connection to the DB
            this.CloseConnection();
            //return the collected data
            return carData;
        }

    }
}
