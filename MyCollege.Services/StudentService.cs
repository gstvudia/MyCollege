using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services.Interfaces;
using System.Collections.Generic;

namespace MyCollege.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public List<Student> SearchStudentsBy(int courseId = 0, int studentid = 0)
        {
            var students = _studentRepository.SearchStudentsBy(courseId, studentid);
            return students;
        }

        public List<Student> GetStudents()
        {
            var students = _studentRepository.GetAll();
            return students;
        }

        public Student Add(Student newStudent)
        {
            if (_studentRepository.GetByName(newStudent.Name) != null)
            {
                return null;
            }
            var created = _studentRepository.Add(newStudent);
            return created;
        }

        public bool Update(Student newStudent)
        {
            var result = _studentRepository.Update(newStudent);
            return result >= 1;
        }
        public bool Delete(int studentId)
        {
            var result = _studentRepository.Remove(studentId);
            return result;
        }
    }
}
