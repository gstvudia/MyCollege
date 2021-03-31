using MyCollege.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCollege.Services.Interfaces
{
    public interface ICourseService
    {
        List<Course> GetCoursesOverview();
    }
}
