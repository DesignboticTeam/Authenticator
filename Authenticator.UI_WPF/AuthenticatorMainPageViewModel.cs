using Authenticator.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Connector.DataModels;
using Connector.Interfaces;
using Microsoft.Extensions.Options;
using UI_WPF.Services;
using UI_WPF.ViewModel;

namespace Authenticator.UI_WPF
{
    public partial class AuthenticatorMainPageViewModel : PageViewModelBase
    {
        NavigationModalService _navigationModalService;
        AppSettings _settings;

        [ObservableProperty]
        string _version;
        public AuthenticatorMainPageViewModel(NavigationModalService navigationModalService, IOptions<AppSettings> settings)
        {
            _navigationModalService = navigationModalService;
            _settings = settings.Value;
            Version = _settings.Version;
        }

        [RelayCommand]
        public void Login()
       {
            _navigationModalService.NavigateTo<ModalLoginViewModel>();
       }
    }
}
