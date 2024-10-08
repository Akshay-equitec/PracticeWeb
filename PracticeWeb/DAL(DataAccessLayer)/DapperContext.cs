using System;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;    

namespace PracticeWeb.DAL_DataAccessLayer_
{
    public class DapperContext
    {
        public readonly string _ConnectionString;



        public DapperContext()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ConnectionString;
        }



        public IDbConnection CreateConnection()
        {

            return new SqlConnection(_ConnectionString);
        }

    }
}