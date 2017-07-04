using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    //Interface that i use to parse data to the database
    interface IParse
    {
        void WriteToDB();
        void datatableToCSV();
        void ReadData();
    }
}
