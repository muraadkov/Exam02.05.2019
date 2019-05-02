using Dapper;
using Exam02._05._2019;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess
{
    public class CityRepository
    {
        public string AddTheCity(string connectionString, string query, City city)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Execute(query, city);
                if (result < 1)
                {
                    throw new Exception("failed!");
                }
                return "gj!";
            }
        }
        public List<City> GetAllCities(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Query<City>(query).ToList();
                return result;
            }
        }
        public void UpdateCity(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                sql.Execute(query);
            }
        }
        public List<City> DeleteAllCities(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Query<City>(query).ToList();
                return result;
            }
        }
    }
}
