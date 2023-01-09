using ForeignLanguagesSchool.Model;
using ForeignLanguagesSchool.Repository;
using ForeignLanguagesSchool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Service
{
    public class ClassService
    {
        private readonly ClassRepository _classRepository;
        public ClassService(ClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        List<Class> GetAllClasses()
        {
            List<Class> classesToReturn = new List<Class>();
            foreach(Class classIter in _classRepository.GetAllClasses())
            {
                if (classIter.Deleted == 0)
                    classesToReturn.Add(classIter);
            }
            return classesToReturn;
        }

        public List<Class> GetAllClassesForProfessorId(int id)
        {
            List<Class> classesToReturn = new List<Class>();
            foreach(Class classIter in GetAllClasses())
            {
                if (classIter.ProfessorId == id)
                    classesToReturn.Add(classIter);                   
            }
            return classesToReturn;
        }

        internal void Create(Class classToAdd)
        {
            _classRepository.Create(classToAdd);
        }

        internal void Delete(Class classToDelete)
        {
            _classRepository.Delete(classToDelete.Id);
        }

        internal int GenerateId()
        {
            int id = 1;
            foreach (Class classIter in _classRepository.GetAllClasses())
            {
                if (classIter.Id >= id)
                    id = classIter.Id + 1;
            }
            return id;
        }

        internal List<Class> GetClassesForProfessorAndDate(int professorId, DateTime selectedDate)
        {
            List<Class> allClasses = GetAllClassesForProfessorId(professorId);
            List<Class> classesToReturn = new List<Class>();
            foreach(Class classIter in allClasses)
            {
                if (classIter.StartDate.Date.Equals(selectedDate.Date))
                    classesToReturn.Add(classIter);
            }
            return classesToReturn;
        }

        internal List<Class> GetAllAvailableClassesForProfessorId(int id)
        {
            List<Class> classesToReturn = new List<Class>();
            foreach (Class classIter in GetAllClassesForProfessorId(id))
            {
                if (classIter.ClassStatus.Equals(ClassStatus.Available))
                    classesToReturn.Add(classIter);
            }
            return classesToReturn;
        }

        internal List<Class> GetAllReservedClassesForProfessorIdAndStudentId(int professorId, int studentId)
        {
            List<Class> classesToReturn = new List<Class>();
            foreach (Class classIter in GetAllClassesForProfessorId(professorId))
            {
                if (classIter.ClassStatus.Equals(ClassStatus.Reserved) && classIter.StudentId == studentId)
                    classesToReturn.Add(classIter);
            }
            return classesToReturn;
        }

        internal void ScheduleClass(Class selectedClass)
        {
            _classRepository.ScheduleClass(selectedClass);
        }

        internal void CancelClass(Class selectedClass)
        {
            _classRepository.CancelClass(selectedClass);
        }
    }
}
