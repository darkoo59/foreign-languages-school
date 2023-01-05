using ForeignLanguagesSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Repository
{
    public class AddressRepository
    {
        private readonly string _connectionString;
        public AddressRepository()
        {
            _connectionString = "Data Source=LAPTOP-A2KV1B0B;Initial Catalog=ForeignLanguagesSchool;Integrated Security=True;Pooling=False";
        }

        public List<Address> GetAllAddresses()
        {
            var addresses = new List<Address>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Address", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addresses.Add(new Address
                            {
                                Id = (int)reader["id"],
                                Street = (string)reader["street"],
                                Number = (int)reader["number"],
                                City = (string)reader["city"],
                                Country = (string)reader["country"]
                            });
                        }
                    }
                }
            }

            return addresses;
        }

        public List<string> GetAllCities()
        {
            var cities = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT distinct city FROM Address", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(
                               (string)reader["city"]
                            ); 
                        }
                    }
                }
            }

            return cities;
        }
    }
}
