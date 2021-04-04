using MyCollege.Data.Models;
using System.Collections.Generic;

namespace MyCollege.Services.Interfaces
{
    public interface ISubjectService
    {
        Subject Add(Subject newCourse);
        bool Delete(int courseId);
        List<Subject> GetSubjects();
        List<Subject> GetSubjectsList(int courseId, int teacherid, int studentId);
        bool Update(Subject newCourse);
    }
}