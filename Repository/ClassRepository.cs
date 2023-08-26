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
    public class ClassRepository
    {
        private readonly string _connectionString;
        public ClassRepository()
        {
            _connectionString = "Data Source=LAPTOP-CKBIDUHH;Initial Catalog=ForeignLanguagesSchool;Integrated Security=True;Pooling=False";
        }

        public List<Class> GetAllClasses()
        {
            var classes = new List<Class>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Class", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classes.Add(new Class
                            {
                                Id = (int)reader["Id"],
                                ProfessorId = (int)reader["Professor_id"],
                                StartDate = Convert.ToDateTime(reader["Start_date"]),
                                StartTime = (TimeSpan)reader["Start_time"],
                                Duration = (int)reader["Duration"],
                                ClassStatus = (Utils.ClassStatus)(int)reader["Class_status"],
                                StudentId = (int)reader["Student_id"],
                                Deleted = (int)reader["Deleted"]
                            });
                        }
                    }
                }
            }

            return classes;
        }

        internal void Create(Class classToAdd)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

            using (var command = new SqlCommand("INSERT INTO Class(Id, Professor_id, Start_date, Start_time, Duration, Class_status, Student_id, Deleted) " +
                "VALUES (@id, @professor_id, @start_date, @start_time, @duration, @class_status, @student_id, @deleted)", connection))
            {
                command.Parameters.AddWithValue("@id", classToAdd.Id);
                command.Parameters.AddWithValue("@professor_id", classToAdd.ProfessorId);
                command.Parameters.AddWithValue("@start_date", classToAdd.StartDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@start_time", classToAdd.StartTime.ToString());
                command.Parameters.AddWithValue("@duration", classToAdd.Duration);
                command.Parameters.AddWithValue("@class_status", Convert.ToInt32(classToAdd.ClassStatus));
                command.Parameters.AddWithValue("@student_id", classToAdd.StudentId);
                command.Parameters.AddWithValue("@deleted", classToAdd.Deleted);
                command.ExecuteNonQuery();
            }
            }
        }
        public void Delete(int classId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE Class SET Deleted = 1 WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", classId);

                    command.ExecuteNonQuery();
                }
            }
        }

        internal void ScheduleClass(Class selectedClass)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE Class SET Class_status = 1,Student_id = @student_id WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@student_id", selectedClass.StudentId);
                    command.Parameters.AddWithValue("@id", selectedClass.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void CancelClass(Class selectedClass)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE Class SET Class_status = 0,Student_id = 0 WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", selectedClass.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
