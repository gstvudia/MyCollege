using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;

namespace MyCollege.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public Teacher Add(Teacher newTeacher)
        {
            if (_teacherRepository.GetByName(newTeacher.Name) != null)
            {
                return null;
            }
            var created = _teacherRepository.Add(newTeacher);
            return created;
        }

        public bool Update(Teacher newTeacher)
        {
            var result = _teacherRepository.Update(newTeacher);
            return result >= 1;
        }
        public bool Delete(int teacherId)
        {
            var result = _teacherRepository.Remove(teacherId);
            return result;
        }
        public List<Teacher> GetTeachersBy(int courseId = 0, int teacherid = 0)
        {
            var teachers = _teacherRepository.GetTeachersBy(courseId, teacherid);
            return teachers;
        }
    }
}
