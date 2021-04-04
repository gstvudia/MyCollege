using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using System.Collections.Generic;

namespace MyCollege.Data.Repositories.Interfaces
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        List<Subject> SearchBy(int courseId, int teacherid, int studentId);
        List<Subject> GetAvailable(int? courseId);
        List<Subject> GetByCourseId(int courseId);
        List<Subject> GetListById(int[] subjectsId);
        int Update(Subject newSubject);
        bool Remove(int subjectId);
        Subject GetByName(string name);
    }
}