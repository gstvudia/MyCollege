using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MyCollege.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        public CourseService(ICourseRepository courseRepository,
            ISubjectRepository subjectRepository)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
        }


        public List<Course> GetCourses()
        {
            var courses = _courseRepository.GetAll();
            return courses;
        }

        public Course Add(Course newCourse)
        {
            if (_courseRepository.GetByName(newCourse.Name) != null)
            {
                return null;
            }
            var created = _courseRepository.Add(newCourse);
            return created;
        }

        public bool Update(Course newCourse)
        {
            var result = _courseRepository.UpdateCourse(newCourse);
            return result;
        }
        public bool Delete(int courseId)
        {
            var result = _courseRepository.Remove(courseId);
            return result;
        }

        public bool AssociateSubjects(int courseId, int[] subjectsId)
        {
            var currentSubjects = _subjectRepository.GetByCourseId(courseId);
            var newSubjects = _subjectRepository.GetListById(subjectsId);
            int result = 0;
            foreach (var oldSubject in currentSubjects)
            {
                if (!newSubjects.Any(x=>x.Id == oldSubject.Id))
                {
                    oldSubject.CourseId = null;
                    result += _subjectRepository.Update(oldSubject);                    
                }
            }

            foreach (var subject in newSubjects)
            {
                subject.CourseId = courseId;
                result += _subjectRepository.Update(subject);
            }

            return result >= 0;
        }
    }
}
