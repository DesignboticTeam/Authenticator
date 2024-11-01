using Authenticator.Components;
using CommunityToolkit.Mvvm.Input;
using Connector.Interfaces;
using UI_WPF.Services;
using UI_WPF.ViewModel;

namespace Authenticator.UI_WPF
{
    public partial class AuthenticatorMainPageViewModel : PageViewModelBase
    {
        NavigationModalService _navigationModalService;

        public AuthenticatorMainPageViewModel(NavigationModalService navigationModalService)
        {
            _navigationModalService = navigationModalService;
        }

        [RelayCommand]
        public void Login()
       {
            _navigationModalService.NavigateTo<ModalLoginViewModel>();
       }
    }
}
