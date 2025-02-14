using System.Reflection;
using Backend.Domains.User.Application.DI;
using SaveApis.Core.Common.Infrastructure.Extension;
using SaveApis.Core.Web.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WithAssemblies(Assembly.GetExecutingAssembly()).WithSaveApis(containerBuilder => containerBuilder.WithCommonModule<PermissionModule>());

var app = builder.Build();

await app.RunSaveApisAsync().ConfigureAwait(false);
