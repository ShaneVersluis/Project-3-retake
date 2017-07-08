using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string filepath = @"..\\..\\lights.csv";
            StreamReader sr = new StreamReader(filepath);
            string line = sr.ReadLine();
            line = line.Replace("\"", "");
            string[] value = line.Split(',');

            // creates datacolumns for the required data
            foreach (string datacolumn in value)
            {
                if (datacolumn == "longitude" || datacolumn == "latitude" || datacolumn == "WIJKNAAM" || datacolumn == "ID_43")
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

                if (value.Length > 5)
                {
                    string[] values = { value[0], value[1], value[5], value[48] };

                    // regular expression to check if input is in ddmmyyyy format
                    Regex correctFormat = new Regex(@"^\d{1,2}\/\d{1,2}\/\d{4}$");

                    // checks if value[48] matches with the regex, ortherwise it insert an empty string
                    Match correctFormatMatch = correctFormat.Match(values[3]);

                    // check with regex and remove invalid dates
                    if (correctFormatMatch.Success && values[3] != "" && values[3] != "0000")
                    {
                        string dateOnly = values[3];
                        string yearOnly;

                        if (dateOnly.Length == 9)
                        {
                            yearOnly = dateOnly.Remove(0, 5);
                        }
                        else
                        {
                            yearOnly = dateOnly.Remove(0, 4);
                        }
                        values[3] = yearOnly;

                        // rename columns so they match the other data
                        if (values[2] == "CHARLOIS")
                        {
                            values[2] = "Charlois";
                        }
                        else if (values[2] == "DELFSHAVEN")
                        {
                            values[2] = "Delfshaven";
                        }
                        else if (values[2] == "FEIJENOORD")
                        {
                            values[2] = "Feijenoord";
                        }
                        else if (values[2] == "HILLEGERSBERG/SCHIEBROEK")
                        {
                            values[2] = "Hillegersberg_Schiebroek";
                        }
                        else if (values[2] == "HOEK VAN HOLLAND")
                        {
                            values[2] = "Hoek_van_Holland";
                        }
                        else if (values[2] == "HOOGVLIET" || values[2] == "PERNIS")
                        {
                            values[2] = "Hoogvliet_Pernis";
                        }
                        else if (values[2] == "IJSSELMONDE")
                        {
                            values[2] = "IJsselmonde";
                        }
                        else if (values[2] == "KRALINGEN-CROOSWIJK")
                        {
                            values[2] = "Kralingen_Crooswijk";
                        }
                        else if (values[2] == "NOORD")
                        {
                            values[2] = "Noord";
                        }
                        else if (values[2] == "OVERSCHIE")
                        {
                            values[2] = "Overschie";
                        }
                        else if (values[2] == "PRINS ALEXANDER")
                        {
                            values[2] = "Prins_Alexander";
                        }
                        else if (values[2] == "ROTTERDAM CENTRUM")
                        {
                            values[2] = "Stadscentrum";
                        }

                        // remove columns that aren't used in the other data
                        if (values[2] != "BEDRIJVENPARK RDAM NW" && values[2] != "RIVIUM" && values[2] != "ROZENBURG" && values[2] != "SPAANSE POLDER" && values[2] != "NIEUW MATHENESSE")
                        {
                            row = dt.NewRow();
                            row.ItemArray = values;
                            dt.Rows.Add(row);
                        }
                    }
                }
            }
        }

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

        public void WriteToDB()
        {
            datatableToCSV();

            MySqlConnection connection = new MySqlConnection("SERVER = localhost;" +
                                                             "Database = datasetsproject3;" +
                                                             "User ID = root;" +
                                                             "Password = root;");
            // use MySqlBulkLoader for speedy transfer to the db
            MySqlBulkLoader bl = new MySqlBulkLoader(connection);
            bl.TableName = "lamps";
            bl.FieldTerminator = ",";
            bl.LineTerminator = "\r\n";
            bl.FileName = "test.csv";
            bl.NumberOfLinesToSkip = 1;
            int inserted = bl.Load();
        }
    }
}
