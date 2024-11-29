using CommunityToolkit.Mvvm.Input;
using Connector.DataModels;
using Connector.Interfaces;
using Microsoft.Extensions.Options;
using UI_WPF.Containers;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace Authenticator.Components
{
    public partial class ModalRegisteredViewModel : ModalLoginViewModel
    {
        public ModalRegisteredViewModel(
            IAuthenticationService authenticationService, 
            INavigationService navigationPageService, 
            NavigationModalService navigationModalService, 
            ModalContainer modalRepository,
            IOptions<AppSettings> settings
            ) : base(authenticationService, navigationPageService, navigationModalService, modalRepository, settings)
        {
            Name = "ModalRegistered";
        }

        [RelayCommand]
        public void GoToLogin()
        {
            _navigationModalService.NavigateTo<ModalLoginViewModel>();
        }

        [RelayCommand]
        public async Task ResendChangeVerification()
        {
            await _authenticationService.SendVerificationEmailAsync();
        }
    }
}
