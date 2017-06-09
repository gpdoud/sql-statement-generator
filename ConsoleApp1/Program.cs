using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlStatement sss = new SqlStatement(
                "Vendor",
                null, //new string[] { "Id", "Code", "Name", "Address", "City", "State", "Zip", "Email", "Phone", "IsRecommended" },
                "Code is not null",
                null //new string[] { "Code", "Zip desc"}
            );
            string sqlselect = sss.ToSelect();
            string sqldelete = sss.ToDelete();
            var i = 0;
        }
    }
}
