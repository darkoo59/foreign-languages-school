using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Repository;
using ForeignLanguagesSchool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ForeignLanguagesSchool.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly SchoolService _schoolService;
        private List<User> _allUsers;
        private List<Professor> _allProfessors;
        public UserService(UserRepository userRepository, SchoolService schoolService)
        {
            _userRepository = userRepository;
            _schoolService = schoolService;
            _allUsers = PopulateAllUsers();
            _allProfessors = PopulateAllProfessors();
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
            _allUsers = new List<User>();
            _allUsers = PopulateAllUsers();
        }

        public void AddProfessorLanguage(int userId,string language)
        {
            Random rnd = new Random();
            _userRepository.AddProfessorLanguage(rnd.Next(1000000000),userId,language);
        }
        public void Update(User user)
        {
            _userRepository.Update(user);
            _allUsers = new List<User>();
            _allUsers = PopulateAllUsers();
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user.Id);
            _allUsers = new List<User>();
            _allUsers = PopulateAllUsers();
        }

        public User GetUserByCredentials(string jmbg, string password)
        {
            foreach(User user in _allUsers)
            {
                if (user.JMBG.Equals(jmbg) && user.Password.Equals(password))
                    return user;
            }
            return null;
        }

        public User GetUserById(int userId)
        {
            _allUsers = PopulateAllUsers();
            foreach (User user in _allUsers)
            {
                if (user.Id == userId)
                    return user;
            }
            return null;
        }

        public List<User> PopulateAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public List<Professor> PopulateAllProfessors()
        {
            _allUsers = PopulateAllUsers();
            List<Professor> allProfessors = new List<Professor>();
            foreach(User user in PopulateAllUsers())
            {
                if(user.UserType.Equals(Utils.UserType.Professor))
                {
                    allProfessors.Add(new Professor
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        Address = user.Address,
                        Email = user.Email,
                        Gender = user.Gender,
                        JMBG = user.JMBG,
                        LastName = user.LastName,
                        Password = user.Password,
                        UserType = user.UserType,
                        School = _schoolService.GetSchoolById(_userRepository.GetSchoolIdByProfessorId(user.Id)),
                        Languages = _userRepository.GetAllLanguagesForProfessor(user.Id)
                    });
                }
            }
            return allProfessors;
        }

        public bool IsJmbgUnique(string jmbgToCheck)
        {
            foreach(User user in _allUsers)
            {
                if (user.JMBG.Equals(jmbgToCheck))
                    return false;
            }
            return true;
        }

        public List<Professor> GetAllProfessorsBySchoolId(int schoolId)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach(Professor professor in PopulateAllProfessors())
            {
                if (professor.School != null)
                {
                    if (professor.School.Id == schoolId)
                        professorsToReturn.Add(professor);
                }
            }
            return professorsToReturn;
        }

        public List<User> getAllUsers()
        {
            _allUsers = PopulateAllUsers();
            return _allUsers;
        }

        public List<Professor> getAllProfessors()
        {
            _allProfessors = PopulateAllProfessors();
            return _allProfessors;
        }

        public int GenerateId()
        {
            int id = 1;
            foreach (User user in _allUsers)
            {
                if (user.Id >= id)
                    id = user.Id + 1;
            }
            return id;
        }

        internal bool IsJmbgUniqueForUpdate(string jmbgToCheck, int id)
        {
            foreach (User user in _allUsers)
            {
                if (user.JMBG.Equals(jmbgToCheck) && user.Id != id)
                    return false;
            }
            return true;
        }

        public void AddProfessor(int professorId, int schoolId)
        {
            _userRepository.AddProfessor(professorId, schoolId);
        }

        internal List<User> FilterListByFirstName(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach(User user in usersToFilter)
            {
                if (user.FirstName.Contains(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByLastName(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.LastName.Contains(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByEmail(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.Email.Contains(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByCountry(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.Address.Country.Contains(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByCity(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.Address.City.Contains(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByStreet(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.Address.Street.Contains(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByNumber(List<User> usersToFilter, string text)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.Address.Number == Convert.ToInt32(text))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }

        internal List<User> FilterListByUserType(List<User> usersToFilter, UserType type)
        {
            List<User> usersToReturn = new List<User>();
            foreach (User user in usersToFilter)
            {
                if (user.UserType.Equals(type))
                    usersToReturn.Add(user);
            }
            return usersToReturn;
        }


        internal List<Professor> FilterListByFirstName(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.FirstName.Contains(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByLastName(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.LastName.Contains(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByEmail(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.Email.Contains(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByCountry(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.Address.Country.Contains(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByCity(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.Address.City.Contains(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByStreet(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.Address.Street.Contains(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByNumber(List<Professor> professorsToFilter, string text)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.Address.Number == Convert.ToInt32(text))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        internal List<Professor> FilterListByLanguages(List<Professor> professorsToFilter, string language)
        {
            List<Professor> professorsToReturn = new List<Professor>();
            foreach (Professor professor in professorsToFilter)
            {
                if (professor.Languages.Contains(language))
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }
    }
}
