using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3Retake
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
