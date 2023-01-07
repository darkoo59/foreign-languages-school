using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Service
{
    public class SchoolService
    {
        private readonly SchoolRepository _schoolRepository;
        private readonly List<School> _allSchools;
        public SchoolService(SchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
            _allSchools = populateAllSchools();
        }

        public School GetSchoolById(int id)
        {
            foreach(School school in _allSchools)
            {
                if (school.Id == id)
                    return school;
            }
            return null;
        }

        public List<School> populateAllSchools()
        {
            List<School> allSchools = _schoolRepository.GetAllSchools();
            foreach(School school in allSchools)
            {
                school.Languages = _schoolRepository.GetAllLanguagesBySchoolId(school.Id);
            }
            return allSchools;
        }

        public List<School> getAllSchooles()
        {
            return _schoolRepository.GetAllSchools();
        }

        public List<string> getAllLanguages()
        {
            return _schoolRepository.GetAllLanguages();
        }

        public List<School> getAllSchoolsForCity(string city)
        {
            List<School> allSchools = getAllSchooles();
            List<School> schoolsToReturn = new List<School>();
            foreach(School school in allSchools)
            {
                if (school.Address.City.Equals(city))
                    schoolsToReturn.Add(school);
            }
            return schoolsToReturn;
        }

        public List<School> getAllSchoolsForLanguage(string language)
        {
            return _schoolRepository.GetAllSchoolsForLanguage(language);
        }

        public List<School> getAllSchoolsForCityAndLanguage(string city, string language)
        {
            List<School> schoolByCity = getAllSchoolsForCity(city);
            List<School> schoolByLanguage = getAllSchoolsForLanguage(language);
            List<School> schoolsToReturn = new List<School>();
            foreach(School schByCity in schoolByCity)
            {
                foreach(School schByLanguage in schoolByLanguage)
                {
                    if(schByCity.Id == schByLanguage.Id)
                    {
                        schoolsToReturn.Add(schByCity);
                        break;
                    }
                }
            }
            return schoolsToReturn;
        }
    }
}
