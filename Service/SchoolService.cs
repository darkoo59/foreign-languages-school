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
        public SchoolService(SchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
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
