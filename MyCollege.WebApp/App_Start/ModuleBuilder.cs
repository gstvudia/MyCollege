using Autofac;
using AutoMapper;
using MyCollege.Data;
using MyCollege.Data.Repositories;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services;
using MyCollege.Services.Interfaces;
using MyCollege.WebApp.Mapping;

namespace MyCollege.WebApp
{
    public class ModuleBuilder : Module
    {
        public ModuleBuilder(){}
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().InstancePerRequest();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<MappingProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
            .As<IMapper>()
            .InstancePerLifetimeScope();

            #region Repositories
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerRequest();
            builder.RegisterType<SubjectRepository>().As<ISubjectRepository>().InstancePerRequest();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerRequest();
            builder.RegisterType<TeacherRepository>().As<ITeacherRepository>().InstancePerRequest();
            builder.RegisterType<GradeRepository>().As<IGradeRepository>().InstancePerRequest();
            #endregion
            #region Services
            builder.RegisterType<CourseService>().As<ICourseService>().InstancePerRequest();
            builder.RegisterType<SubjectService>().As<ISubjectService>().InstancePerRequest();
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerRequest();
            builder.RegisterType<TeacherService>().As<ITeacherService>().InstancePerRequest();
            builder.RegisterType<GradeService>().As<IGradeService>().InstancePerRequest();
            #endregion


            base.Load(builder);
        }
    }
}