using MyCollege.Data.Models;
using MyCollege.Data.Repositories.Base;
using System.Collections.Generic;

namespace MyCollege.Data.Repositories.Interfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Teacher GetByName(string name);
        List<Teacher> GetTeachersBy(int courseId, int teacherid);
        bool Remove(int teacherId);
        int Update(Teacher newTeacher);
    }
}