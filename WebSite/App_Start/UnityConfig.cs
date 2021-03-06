using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using WebSite.Models;
using Microsoft.AspNet.Identity;
using Identity.Infrastructure.Contexts;
using Core.Repositories;
using Core.Entities;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Stores;
using Microsoft.Owin.Security;
using System.Web;
using WebSite.Core;

namespace WebSite
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext>(new PerRequestLifetimeManager());

            container.RegisterType<IUserRepository<string, IdentityUser, IdentityUserRole<string>, IdentityRoleClaim<string>>, UserRepository<string, IdentityUser, IdentityUserRole<string>, IdentityRoleClaim<string>>>();
            container.RegisterType<IRoleRepository<string, IdentityRole, IdentityUserRole<string>, IdentityRoleClaim<string>>, RoleRepository<string, IdentityRole, IdentityUserRole<string>, IdentityRoleClaim<string>>> ();
            container.RegisterType<AppUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<AppSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => System.Web.HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<UserStore<string, AppMember, IdentityUserRole<string>, IdentityRoleClaim<string>>>(new PerRequestLifetimeManager());
            container.RegisterType<RoleStore<string, IdentityRole, IdentityUserRole<string>, IdentityRoleClaim<string>>>(new PerRequestLifetimeManager());
        }
    }
}