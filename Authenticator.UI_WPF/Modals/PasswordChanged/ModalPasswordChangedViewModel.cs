using CommunityToolkit.Mvvm.Input;
using Connector.DataModels;
using Connector.Interfaces;
using Microsoft.Extensions.Options;
using UI_WPF.Containers;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace Authenticator.Components
{
    public partial class ModalPasswordChangedViewModel : ModalLoginViewModel
    {
        public ModalPasswordChangedViewModel(
            IAuthenticationService authenticationService,
            INavigationService navigationPageService,
            NavigationModalService navigationModalService,
            ModalContainer modalRepository,
            IOptions<AppSettings> settings
            ) : base(authenticationService, navigationPageService, navigationModalService, modalRepository, settings)
        {
            Name = "ModalPasswordChanged";
        }

        [RelayCommand]
        public async Task GoToLogin()
        {
            _navigationModalService.NavigateTo<ModalLoginViewModel>();
        }
    }
}
