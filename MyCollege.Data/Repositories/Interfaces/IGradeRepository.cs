using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using System.Collections.Generic;

namespace MyCollege.Data.Repositories.Interfaces
{
    public interface IGradeRepository : IBaseRepository<Grade>
    {
        List<Grade> SearchGradesBy(int courseId, int studentId, int subjectId);
        int Update(Grade newStudent);
    }
}