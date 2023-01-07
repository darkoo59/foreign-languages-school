using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Service;
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

        public void Update(Address address)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE Address SET Street = @street, Number = @number, City = @city, Country = @country WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", address.Id);
                    command.Parameters.AddWithValue("@street", address.Street);
                    command.Parameters.AddWithValue("@number", address.Number);
                    command.Parameters.AddWithValue("@city", address.City);
                    command.Parameters.AddWithValue("@country", address.Country);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Create(Address address)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO Address(Id, Street, Number, City, Country) VALUES(@id, @street, @number, @city, @country)", connection))
                {
                    command.Parameters.AddWithValue("@id", address.Id);
                    command.Parameters.AddWithValue("@street", address.Street);
                    command.Parameters.AddWithValue("@number", address.Number);
                    command.Parameters.AddWithValue("@city", address.City);
                    command.Parameters.AddWithValue("@country", address.Country);

                    command.ExecuteNonQuery();
                }
            }
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
