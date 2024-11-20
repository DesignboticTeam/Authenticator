using CommunityToolkit.Mvvm.Input;
using Connector.Interfaces;
using UI_WPF.Containers;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace Authenticator.Components
{
    public partial class ModalPasswordChangedViewModel : ModalLoginViewModel
    {
        public ModalPasswordChangedViewModel(IAuthenticationService authenticationService, INavigationService navigationPageService, NavigationModalService navigationModalService, ModalContainer modalRepository) : base(authenticationService, navigationPageService, navigationModalService, modalRepository)
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
