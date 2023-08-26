using ForeignLanguagesSchool.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Repository
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository()
        {
            _connectionString = "Data Source=LAPTOP-CKBIDUHH;Initial Catalog=ForeignLanguagesSchool;Integrated Security=True;Pooling=False";
        }

        public void Update(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE [User] SET first_name = @first_name, last_name = @last_name, jmbg = @jmbg, gender = @gender, address_id = " +
                    "@address_id, email=@email, password=@password, user_type=@user_type WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@first_name", user.FirstName);
                    command.Parameters.AddWithValue("@last_name", user.LastName);
                    command.Parameters.AddWithValue("@jmbg", user.JMBG);
                    command.Parameters.AddWithValue("@gender", Convert.ToInt32(user.Gender));
                    command.Parameters.AddWithValue("@address_id", user.Address.Id);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@user_type", Convert.ToInt32(user.UserType));
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Create(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO [User](id, first_name, last_name, jmbg, gender, address_id, email, password, user_type, deleted) " +
                    "VALUES (@id, @first_name, @last_name, @jmbg, @gender, @address_id, @email, @password, @user_type, 0)", connection))
                {
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@first_name", user.FirstName);
                    command.Parameters.AddWithValue("@last_name", user.LastName);
                    command.Parameters.AddWithValue("@jmbg", user.JMBG);
                    command.Parameters.AddWithValue("@gender", Convert.ToInt32(user.Gender));
                    command.Parameters.AddWithValue("@address_id", user.Address.Id);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@user_type", Convert.ToInt32(user.UserType));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE [User] SET deleted = 1 WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", userId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetAllLanguagesForProfessor(int professorId)
        {
            var languages = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM ProfessorLanguages WHERE professor_id="+professorId, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            languages.Add((string)reader["language"]);
                        }
                    }
                }
            }

            return languages;
        }

        public int GetSchoolIdByProfessorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Professor WHERE id=" + id, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (int)reader["school_id"];
                        }
                    }
                }
            }
            return 0;
        }

        public User GetUserById(int id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM [User] WHERE deleted=0 AND id=" + id, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User
                            {
                                Id = (int)reader["id"],
                                FirstName = (string)reader["first_name"],
                                LastName = (string)reader["last_name"],
                                Email = (string)reader["email"],
                                Gender = (Utils.Gender)(int)reader["gender"],
                                JMBG = (string)reader["jmbg"],
                                Password = (string)reader["password"],
                                UserType = (Utils.UserType)(int)reader["user_type"],
                                Address = GetAddressById((int)reader["address_id"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM [User] WHERE deleted=0", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = (int)reader["id"],
                                FirstName = (string)reader["first_name"],
                                LastName = (string)reader["last_name"],
                                Email = (string)reader["email"],
                                Gender = (Utils.Gender)(int)reader["gender"],
                                JMBG = (string)reader["jmbg"],
                                Password = (string)reader["password"],
                                UserType = (Utils.UserType)(int)reader["user_type"],
                                Address = GetAddressById((int)reader["address_id"])
                            });
                        }
                    }
                }
            }

            return users;
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

        public void AddProfessorLanguage(int id,int userId, string language)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO ProfessorLanguages(id, professor_id, language) " +
                    "VALUES (@id, @professor_id, @language)", connection))
                {
                    command.Parameters.AddWithValue("@id",id);
                    command.Parameters.AddWithValue("@professor_id", userId);
                    command.Parameters.AddWithValue("@language", language);
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void AddProfessor(int professorId, int schoolId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO Professor (id, school_id) " +
                    "VALUES (@id, @school_id)", connection))
                {
                    command.Parameters.AddWithValue("@id", professorId);
                    command.Parameters.AddWithValue("@school_id", schoolId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
