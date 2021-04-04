using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }


        public List<Subject> GetSubjectsList(int courseId, int teacherid, int studentId)
        {
            var subjects = _subjectRepository.SearchBy(courseId, teacherid, studentId);
            return subjects;
        }

        public List<Subject> GetSubjects()
        {
            var subjects = _subjectRepository.GetAll();
            return subjects;
        }

        public Subject Add(Subject newSubject)
        {
            if (_subjectRepository.GetByName(newSubject.Name) != null)
            {
                return null;
            }
            var created = _subjectRepository.Add(newSubject);
            return created;
        }

        public bool Update(Subject newSubject)
        {
            var result = _subjectRepository.Update(newSubject);
            return result >= 1;
        }
        public bool Delete(int subjectId)
        {
            var result = _subjectRepository.Remove(subjectId);
            return result;
        }
    }
}
