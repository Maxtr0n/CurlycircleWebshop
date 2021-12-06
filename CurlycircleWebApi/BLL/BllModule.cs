using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace BLL
{
  public class BllModule : Autofac.Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      base.Load(builder);

      var assembly = Assembly.GetAssembly(typeof(BllModule));
      builder.RegisterAssemblyTypes(assembly)
        .Where(type => type.Name.EndsWith("Service"))
        .AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
