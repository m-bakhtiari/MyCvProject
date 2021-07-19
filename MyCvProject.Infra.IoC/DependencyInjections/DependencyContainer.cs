using Microsoft.Extensions.DependencyInjection;
using MyCvProject.Core.Convertors;
using MyCvProject.Core.Interfaces;
using MyCvProject.Core.Services;
using MyCvProject.Domain.Interfaces;
using MyCvProject.Infra.Data.Repositories;

namespace MyCvProject.Infra.IoC.DependencyInjections
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            #region Core Layer

            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IViewRenderService, RenderViewToString>();
            service.AddTransient<IPermissionService, PermissionService>();
            service.AddTransient<ICourseService, CourseService>();
            service.AddTransient<IOrderService, OrderService>();

            #endregion

            #region Data Layer

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IPermissionRepository, PermissionRepository>();
            service.AddTransient<ICourseRepository, CourseRepository>();
            service.AddTransient<IOrderRepository, OrderRepository>();

            #endregion
        }

    }
}
