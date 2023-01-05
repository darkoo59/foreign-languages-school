using ForeignLanguagesSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Repository
{
    public class SchoolRepository
    {
        private readonly string _connectionString;
        public SchoolRepository()
        {
            _connectionString = "Data Source=LAPTOP-A2KV1B0B;Initial Catalog=ForeignLanguagesSchool;Integrated Security=True;Pooling=False";
        }

        public List<School> GetAllSchools()
        {
            var schools = new List<School>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM School", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schools.Add(new School
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Address = GetAddressById((int)reader["address_id"])
                            });
                        }
                    }
                }
            }

            return schools;
        }

        public School GetSchoolById(int id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM School WHERE id=" + id, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new School
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Address = GetAddressById((int)reader["address_id"])
                            };
                        }
                    }
                }
            }
            return null;
        }
        public Address GetAddressById(int id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Address WHERE id=" + id, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Address
                            {
                                Id = (int)reader["id"],
                                Street = (string)reader["street"],
                                Number = (int)reader["number"],
                                City = (string)reader["city"],
                                Country = (string)reader["country"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<string> GetAllLanguages()
        {
            var languages = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT distinct language FROM SchoolLanguages", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            languages.Add(
                               (string)reader["language"]
                            );
                        }
                    }
                }
            }

            return languages;
        }

        public List<School> GetAllSchoolsForLanguage(string language)
        {
            var schools = new List<School>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM SchoolLanguages WHERE language='"+language+"'", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schools.Add(
                               GetSchoolById((int)reader["school_id"])
                            );
                        }
                    }
                }
            }

            return schools;
        }
    }
}
