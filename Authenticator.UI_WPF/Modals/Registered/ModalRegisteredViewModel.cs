using CommunityToolkit.Mvvm.Input;
using Connector.Interfaces;
using UI_WPF.Containers;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace Authenticator.Components
{
    public partial class ModalRegisteredViewModel : ModalLoginViewModel
    {
        public ModalRegisteredViewModel(IAuthenticationService authenticationService, INavigationService navigationPageService, NavigationModalService navigationModalService, ModalContainer modalRepository) : base(authenticationService, navigationPageService, navigationModalService, modalRepository)
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
