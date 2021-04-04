using MyCollege.Data.Models;
using System.Collections.Generic;

namespace MyCollege.Services.Interfaces
{
    public interface ITeacherService
    {
        Teacher Add(Teacher newTeacher);
        bool Delete(int teacherId);
        List<Teacher> GetTeachersBy(int courseId = 0, int teacherid = 0);
        bool Update(Teacher newTeacher);
    }
}