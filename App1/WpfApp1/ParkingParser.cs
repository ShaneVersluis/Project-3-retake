using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using OxyPlot;
using OxyPlot.Series;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;


namespace WpfApp1
{
    class ParkingParser : IParse
    {
        // create a new datatable for easy transfer to db
        DataTable dt = new DataTable();
        DataRow row;
        
       

        public void ReadData()
        {
            // reads the csv file and splits it up at a comma to create a string[]
            string filepath = @"C:\Users\Shane versluis\Desktop\Project-3-retake\App1\WpfApp1\parking.csv";
            StreamReader sr = new StreamReader(filepath);
            string line = sr.ReadLine();
            line = line.Replace("\"", "");
            string[] value = line.Split(',');
           
            // creates datacolumns for the required data
            foreach (string datacolumn in value)
            {
                Console.WriteLine(value);
                if (datacolumn == "LATITUDE" || datacolumn == "LONGITUDE" || datacolumn == "NAME" || datacolumn == "TYPE" || datacolumn == "DISTRICT")
                {
                    dt.Columns.Add(new DataColumn(datacolumn));
                    
                }
            }

            //reads over the entire csv file and adds the data if it's in correct format
            while (!sr.EndOfStream)
            {

                // read the next line and split it up at a comma
                string lines = sr.ReadLine();
                value = lines.Split(',');

                if (value.Length > 6)
                {
                    //gets these set colomns from csv file
                    string[] values = { value[1], value[2], value[3], value[5], value[6]};

                    row = dt.NewRow();
                    row.ItemArray = values;
                    dt.Rows.Add(row);

                }
            }
        }


        // This builds a string from the data we gathered from parking.csv and put it into a new csv file called Test.csv
        public void datatableToCSV()
        {
            StringBuilder sb = new StringBuilder();

            // transform DataColumns to columns in csv
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);

            // append a , to the string to split it up
            sb.AppendLine(string.Join(",", columnNames));

            // iterative over all the DataRows and append them to the string stored in memory
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            // write the string to a .csv file
            File.WriteAllText("test.csv", sb.ToString());
        }
        //This is where i write the data to the database
        public void WriteToDB()
        {
            datatableToCSV();
            //making a connection with the database
            MySqlConnection connection = new MySqlConnection("SERVER = 127.0.0.1;" +
                                                             "Database = dbproject3retake;" +
                                                             "User ID = root;" +
                                                             "Password = root;");

            //bulk load the data to the database
            MySqlBulkLoader bl = new MySqlBulkLoader(connection);
            bl.TableName = "parking";
            bl.FieldTerminator = ",";
            bl.LineTerminator = "\r\n";
            bl.FileName = "test.csv";
            bl.NumberOfLinesToSkip = 1;
            int inserted = bl.Load();
        }
    }
}
