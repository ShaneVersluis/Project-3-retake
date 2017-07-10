using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using OxyPlot;
using OxyPlot.Series;


namespace WpfApp1
{
    //Interface that i use to help connect to the database
    interface IConnection
    {
        void CreateDB();
        void InsertIntoDB();
        void OpenConnection();
        void CloseConnection();
        void dbCommand(string sqlQuery);
    }
}
