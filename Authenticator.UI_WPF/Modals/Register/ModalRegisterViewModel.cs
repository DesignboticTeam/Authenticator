using CommunityToolkit.Mvvm.Input;
using Connector.DataModels;
using Connector.Interfaces;
using Microsoft.Extensions.Options;
using UI_WPF.Containers;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace Authenticator.Components
{
    public partial class ModalRegisterViewModel : ModalLoginViewModel
    {
        public ModalRegisterViewModel(
            IAuthenticationService authenticationService,
            INavigationService navigationPageService,
            NavigationModalService navigationModalService,
            ModalContainer modalRepository,
            IOptions<AppSettings> settings
            ) : base(authenticationService, navigationPageService, navigationModalService, modalRepository, settings)
        {
            Name = "ModalRegister";
        }

        [RelayCommand]
        public async Task Register()
        {
            await _authenticationService.Register(User.Email, User.Password, OnLogged);
        }
        [RelayCommand]
        public void GoToLogin()
        {
            _navigationModalService.NavigateTo<ModalLoginViewModel>();
        }
    }
}
