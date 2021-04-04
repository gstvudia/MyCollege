using MyCollege.Data.Models;
using System.Collections.Generic;

namespace MyCollege.Services.Interfaces
{
    public interface IGradeService
    {
        Grade Add(Grade newGrade);
        double GetAverageByCourse(int courseid);
        double GetAverageByStudent(int studentId);
        double GetAverageBySubject(int subjectId);
        IEnumerable<Grade> SearchGradesBy(int courseId = 0, int studentid = 0, int subjectId = 0);
        bool Update(Grade newGrade);
    }
}