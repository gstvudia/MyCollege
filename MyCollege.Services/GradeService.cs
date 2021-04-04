using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }


        public IEnumerable<Grade> SearchGradesBy(int courseId = 0, int studentid = 0, int subjectId = 0)
        {
            var grades = _gradeRepository.SearchGradesBy(courseId, studentid, subjectId);
            return grades;
        }

        public double GetAverageByCourse(int courseid)
        {
            var grades = _gradeRepository.SearchGradesBy(courseid, 0, 0)
                .Select(g => g.Value)
                .ToArray();

            var average = grades.DefaultIfEmpty(0).Average(a => a);

            return average;
        }

        public double GetAverageBySubject(int subjectId)
        {
            var grades = _gradeRepository.SearchGradesBy(0, 0, subjectId)
                .Select(g => g.Value)
                .ToArray();

            var average = grades.DefaultIfEmpty(0).Average(a => a);

            return average;
        }
        public double GetAverageByStudent(int studentId)
        {
            var grades = _gradeRepository.SearchGradesBy(0, studentId, 0)
                .Select(g => g.Value)
                .ToArray();

            var average = grades.DefaultIfEmpty(0).Average(a => a);

            return average;
        }

        public Grade Add(Grade newGrade)
        {
            if (_gradeRepository.SearchGradesBy(0, newGrade.StudentId, newGrade.SubjectId).Any())
            {
                return null;
            }
            var created = _gradeRepository.Add(newGrade);
            return created;
        }

        public bool Update(Grade newGrade)
        {
            var result = _gradeRepository.Update(newGrade);
            return result >= 1;
        }
    }
}
