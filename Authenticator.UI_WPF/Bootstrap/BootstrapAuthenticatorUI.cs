using Authenticator.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_WPF.Components;
using UI_WPF.Containers;
using UI_WPF.Factories;
using UI_WPF.Interfaces;
using UI_WPF.Services;
using UI_WPF.ViewModel;

namespace Authenticator.UI_WPF.Bootstrap
{
    public static class BootstrapAuthenticatorUI
    {
        /// <summary>
        /// Full Initialization for Standalone
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServicesStandalone(IServiceCollection services)
        {
            // services.AddSingleton<IApplicationService, RevitApplication>();

            //Custom for standalone
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainPage>();
            services.AddSingleton<MainPageViewModel>();
            services.AddTransient<AuthenticatorMainPage>();
            services.AddTransient<AuthenticatorMainPageViewModel>();

            //TODO remove -> added in needed services
            services.AddSingleton<ViewModelFactory>();
            services.AddSingleton<PageDataTempleSelector>();

            var mapper = new ViewModelToViewMapper() as IViewModelToViewMapper;
            mapper.RegisterMapping<MainPageViewModel, MainPage>();
            mapper.RegisterMapping<AuthenticatorMainPageViewModel, AuthenticatorMainPage>();

            services.ConfigureServicesAuthenticatorUI(mapper, true);

            return services;
        }
        /// <summary>
        /// Configurates UI for Authentication as Service in App. NOTE: Use last after all UI Services are added.
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="mapper">ViewModel Mapper -> add all other mappings before -> mapper will be added to ServiceCollector</param>
        /// <param name="ensureNeedServices">Ensure to initilize needed Services for UI to work</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServicesAuthenticatorUI(this IServiceCollection services, IViewModelToViewMapper mapper, bool ensureNeedServices = true)
        {
            //Add AuthModals
            services.ConfigureViewModels();

            mapper = BootstrapAuthenticatorUI.ConfigureMapper(mapper);
            services.AddSingleton<IViewModelToViewMapper>(mapper);

            //Ensure needed services
            if (ensureNeedServices)
            {
                services.AddNeededServicesIfNotAdded();
            }

            return services;
        }

        /// <summary>
        /// Mapper config for Authentication modals
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static IViewModelToViewMapper ConfigureMapper(IViewModelToViewMapper mapper)
        {
            mapper.RegisterMapping<ModalLoginViewModel, ModalLogin>();
            mapper.RegisterMapping<ModalPasswordChangedViewModel, ModalPasswordChanged>();
            mapper.RegisterMapping<ModalRegisterViewModel, ModalRegister>();
            mapper.RegisterMapping<ModalRegisteredViewModel, ModalRegistered>();

            return mapper;
        }

        /// <summary>
        /// View models for Authenticator
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<ModalLoginViewModel>();
            services.AddTransient<ModalRegisterViewModel>();
            services.AddTransient<ModalRegisteredViewModel>();
            services.AddTransient<ModalPasswordChangedViewModel>();

            return services;
        }

        /// <summary>
        /// Configure needed services if not initialized already -> should be used after all other initialization.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNeededServicesIfNotAdded(this IServiceCollection services)
        {
            //Check if navigation is added previously
            if (!services.Any(x => x.GetType() == typeof(NavigationModalService)))
            {
                services.AddSingleton<NavigationModalService>();
            }

            if (!services.Any(x => x.GetType() == typeof(NavigationModalService)))
            {
                services.AddSingleton<ModalContainer>();
            }

            if (!services.Any(x => x.GetType() == typeof(INavigationService)))
            {
                services.AddSingleton<INavigationService, NavigationService>();
            }
            if (!services.Any(x => x.GetType() == typeof(INavigationService)))
            {
                services.AddSingleton<PageViewModelFactory>();
            }
            if (!services.Any(x => x.GetType() == typeof(PageContainer)))
            {
                services.AddSingleton<PageContainer>();
            }
            if (!services.Any(x => x.GetType() == typeof(PageDataTempleSelector)))
            {
                services.AddSingleton<PageDataTempleSelector>();
            }
            return services;
        }
    }
}
