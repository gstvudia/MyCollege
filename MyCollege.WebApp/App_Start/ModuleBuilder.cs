using Autofac;
using MyCollege.Data;
using MyCollege.Data.Repositories;
using MyCollege.Data.Repositories.Interfaces;
using MyCollege.Services;
using MyCollege.Services.Interfaces;
using MyCollege.WebApp.Controllers;
using System.Data.Entity;

namespace MyCollege.WebApp
{
    public class ModuleBuilder : Module
    {
        public ModuleBuilder()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().InstancePerRequest();

            #region Repositories
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerRequest();
            #endregion
            #region Services
            builder.RegisterType<CourseService>().As<ICourseService>().InstancePerRequest();
            #endregion


            base.Load(builder);
        }
    }
}