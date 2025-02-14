using System.Reflection;
using Autofac;
using Backend.Domains.User.Infrastructure;
using SaveApis.Core.Common.Application.Helpers;
using SaveApis.Core.Common.Infrastructure.DI;

namespace Backend.Domains.User.Application.DI;

public class PermissionModule : BaseModule
{
    protected override void Load(ContainerBuilder builder)
    {
        var helper = new AssemblyHelper();
        helper.RegisterAssembly(Assembly.GetExecutingAssembly());

        builder.RegisterAssemblyTypes(helper.GetAssemblies().ToArray())
            .Where(type => type.IsAssignableTo<IPermission>())
            .AsImplementedInterfaces();
    }
}
