using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.IO;

namespace KeyValConsole
{
    class Program
    {
        public void CreateDB()
        {
            File.Delete("Test.sdf");
            string connString = "Data Source='Test.sdf'; LCID=1033;   Password=mssql44; Encrypt = TRUE;";
            SqlCeEngine engine = new SqlCeEngine(connString);
            engine.CreateDatabase();
        }

        public void InsertValuesInToDatabase()
        {

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter 2 CSV Values (key,Val): ");
            var val = Console.ReadLine();

            String[] split = val.Split(new Char[] { ',' });

            int newKey = Convert.ToInt32(split[0]);
            string newVal = split[1];

            //http://stackoverflow.com/questions/11694623/sql-server-compact-insertion           

            using (SqlCeConnection Conn = new SqlCeConnection("Data Source ='C:\\Users\\mkeneqa\\Dev\\KeyVal.sdf'; Password='ms-sql44';"))
            {
                Conn.Open();

                SqlCeCommand cmd = Conn.CreateCommand();
                cmd.CommandText = "INSERT INTO MyKeys (kvKey, kvValue) VALUES (@key,@value)";

                SqlCeParameter param = null;

                param = new SqlCeParameter("@key", newKey);
                cmd.Parameters.Add(param);

                param = new SqlCeParameter("@value", newVal);
                cmd.Parameters.Add(param);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
        }

    }
}
