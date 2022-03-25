using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = Assembly.GetAssembly(typeof(DalModule));
            builder.RegisterAssemblyTypes(assembly!)
              .Where(type => type.Name.EndsWith("Repository"))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>()
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();
        }
    }
}
