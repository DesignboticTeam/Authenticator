using Authenticator.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_WPF.Containers;
using UI_WPF.Factories;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace Authenticator.UI_WPF
{
    public class BootstrapUI
    {
        public static IServiceCollection ConfigureAuthentication(IServiceCollection services)
        {
            //Check if navigation is added previously
            if(!services.Any(x => x.GetType() == typeof(NavigationModalService))){
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
            if (!services.Any(x => x.GetType() == typeof(PageViewModelFactory)))
            {
                services.AddSingleton<PageContainer>();
            }

            services.AddTransient<ModalLoginViewModel>();
            services.AddTransient<ModalRegisterViewModel>();
            services.AddTransient<ModalRegisteredViewModel>();
            services.AddTransient<ModalPasswordChangedViewModel>();

            return services;
        }
    }
}
