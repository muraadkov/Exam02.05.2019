using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam02._05._2019
{
    public class StreetRepository
    {
        public string AddTheStreet(string connectionString, string query, Street country)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Execute(query, country);
                if (result < 1)
                {
                    throw new Exception("failed!");
                }
                return "gj!";
            }
        }
        public List<Street> GetAllStreets(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Query<Street>(query).ToList();
                return result;
            }
        }
        public void UpdateStreet(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                sql.Execute(query);
            }
        }
        public List<Street> DeleteAllStreets(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Query<Street>(query).ToList();
                return result;
            }
        }
    }
}
