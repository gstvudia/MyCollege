using MyCollege.Data.Models;
using System.Collections.Generic;

namespace MyCollege.Services.Interfaces
{
    public interface IStudentService
    {
        Student Add(Student newStudent);
        bool Delete(int courseId);
        List<Student> GetStudents();
        List<Student> SearchStudentsBy(int courseId = 0, int studentid = 0);
        bool Update(Student newStudent);
    }
}