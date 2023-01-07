using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly SchoolService _schoolService;
        private List<User> _allUsers;
        private readonly List<Professor> _allProfessors;
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

        public List<User> PopulateAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public List<Professor> PopulateAllProfessors()
        {
            List<Professor> allProfessors = new List<Professor>();
            foreach(User user in _allUsers)
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
            foreach(Professor professor in _allProfessors)
            {
                if (professor.School.Id == schoolId)
                    professorsToReturn.Add(professor);
            }
            return professorsToReturn;
        }

        public List<User> getAllUsers()
        {
            return _userRepository.GetAllUsers();
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
    }
}
