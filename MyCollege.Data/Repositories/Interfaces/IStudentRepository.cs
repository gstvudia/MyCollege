using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using System.Collections.Generic;

namespace MyCollege.Data.Repositories.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Student GetByName(string name);
        bool Remove(int studentId);
        List<Student> SearchStudentsBy(int courseId, int studentid);
        int Update(Student newStudent);
    }
}