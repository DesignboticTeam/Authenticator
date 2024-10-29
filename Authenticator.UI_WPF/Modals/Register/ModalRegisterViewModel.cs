using CommunityToolkit.Mvvm.Input;
using Connector.Interfaces;
using UI_WPF.Interfaces;
using UI_WPF.Services;

namespace NextDesignerWPF.ViewModel
{
    public partial class ModalRegisterViewModel : ModalLoginViewModel
    {


        public ModalRegisterViewModel(IAuthenticationService authenticationService, INavigationService navigationPageService, NavigationModalService navigationModalService, ModalRepository modalRepository) : base(authenticationService, navigationPageService, navigationModalService, modalRepository)
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
