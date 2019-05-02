using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam02._05._2019
{
    public class CountryRepository
    {
        public string AddTheCountry(string connectionString, string query, Country country)
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
        public List<Country> GetAllCountries(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Query<Country>(query).ToList();
                return result;
            }
        }
        public void UpdateCountries(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                sql.Execute(query);
            }
        }
        public List<Country> DeleteAllCountries(string connectionString, string query)
        {
            using (var sql = new SqlConnection(connectionString))
            {
                var result = sql.Query<Country>(query).ToList();
                return result;
            }
        }
    }
}
