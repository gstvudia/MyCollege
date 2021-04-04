using MyCollege.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollege.Services.Interfaces
{
    public interface ICourseService
    {
        Course Add(Course newCourse);
        bool AssociateSubjects(int courseId, int[] subjectsId);
        bool Delete(int courseId);
        List<Course> GetCourses();
        bool Update(Course newCourse);
    }
}
