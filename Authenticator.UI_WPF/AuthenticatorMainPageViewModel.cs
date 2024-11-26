using Authenticator.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Connector.DataModels;
using Connector.Interfaces;
using Microsoft.Extensions.Options;
using System.Reflection;
using UI_WPF.Services;
using UI_WPF.ViewModel;

namespace Authenticator.UI_WPF
{
    [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
    public partial class AuthenticatorMainPageViewModel : PageViewModelBase
    {
        NavigationModalService _navigationModalService;
        AppSettings _settings;

        [ObservableProperty]
        string _version;
        public AuthenticatorMainPageViewModel(NavigationModalService navigationModalService, IOptions<AppSettings> settings, IDialogService dialog)
        {
            _navigationModalService = navigationModalService;
            _settings = settings.Value;
            Version = _settings.Version;
            /*
            var testIcon = settings.Value.FullLogoPath ?? "null"; 
            var iconCheck = IconPath ?? testIcon;
            dialog.ShowDialog("Path", iconCheck);
            */
        }

        [RelayCommand]
        public void Login()
       {
            _navigationModalService.NavigateTo<ModalLoginViewModel>();
       }
    }
}
