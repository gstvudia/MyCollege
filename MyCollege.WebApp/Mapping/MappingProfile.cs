using AutoMapper;
using MyCollege.Data.Models;
using MyCollege.WebApp.Models;

namespace MyCollege.WebApp.Mapping
{
    public class MappingProfile 
        : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember(c=>c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.CourseName, opt => opt.MapFrom(source => source.Name))
                .ForMember(c => c.SelectedSubjects, opt => opt.Ignore());

            CreateMap<CourseDTO, Course>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(source => source.CourseName))
                .ForMember(c => c.Subjects, opt => opt.Ignore());


            CreateMap<Grade, GradeDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Student, opt => opt.MapFrom(source => source.StudentId))
                .ForMember(c => c.Subject, opt => opt.MapFrom(source => source.SubjectId))
                .ForMember(c => c.value, opt => opt.MapFrom(source => source.Value));

            CreateMap<GradeDTO, Grade>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.SubjectId, opt => opt.MapFrom(source => source.Subject))
                .ForMember(c => c.StudentId, opt => opt.MapFrom(source => source.Student))
                .ForMember(c => c.Value, opt => opt.MapFrom(source => source.value));

            CreateMap<Subject, SubjectDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.SubjectName, opt => opt.MapFrom(source => source.Name))
                .ForMember(c => c.SelectedTeacher, opt => opt.Ignore());

            CreateMap<SubjectDTO, Subject>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(source => source.SubjectName))
                .ForMember(c => c.Teacher, opt => opt.MapFrom(source => source.Teacher));


            CreateMap<Student, StudentDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Birthdate, opt => opt.MapFrom(source => source.Birthdate))
                .ForMember(c => c.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(c => c.RegistrationNumber, opt => opt.MapFrom(source => source.RegistrationNumber))
                .ForMember(c => c.Grades, opt => opt.MapFrom(source => source.Grades));

            CreateMap<StudentDTO, Student>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Birthdate, opt => opt.MapFrom(source => source.Birthdate))
                 .ForMember(c => c.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(c => c.RegistrationNumber, opt => opt.MapFrom(source => source.RegistrationNumber))
                .ForMember(c => c.Grades, opt => opt.MapFrom(source => source.Grades));


            CreateMap<Teacher, TeacherDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(c => c.Salary, opt => opt.MapFrom(source => source.Salary))
                .ForMember(c => c.Birthdate, opt => opt.MapFrom(source => source.Birthdate));

            CreateMap<TeacherDTO, Teacher>()
                .ForMember(c => c.Id, opt => opt.MapFrom(source => source.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(source => source.Name))
                .ForMember(c => c.Salary, opt => opt.MapFrom(source => source.Salary))
                .ForMember(c => c.Birthdate, opt => opt.MapFrom(source => source.Birthdate));

        }
    }
}