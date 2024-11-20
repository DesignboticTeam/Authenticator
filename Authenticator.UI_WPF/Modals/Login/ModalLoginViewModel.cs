using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Connector.Interfaces;
using Connector.DataModels;
using Connector.Enums;
using UI_WPF.Components;
using UI_WPF.Interfaces;
using UI_WPF.ViewModel;
using UI_WPF.Services;
using UI_WPF.Containers;
using WPFLocalizeExtension.Providers;
using Microsoft.Extensions.Options;
using System.Resources;

namespace Authenticator.Components
{

    public partial class ModalLoginViewModel : ViewModelBase
    {
        protected IAuthenticationService _authenticationService;
        protected INavigationService _navigationPageService;

        protected NavigationModalService _navigationModalService;
        protected ModalContainer _modalContainer;

        [ObservableProperty]
        protected SettingsWindowParameterViewModel _userDataPanelViewModel;

        //TODO make user container ?
        [ObservableProperty]
        protected UserData _user;

        [ObservableProperty]
        protected string _status;

        public ModalLoginViewModel(IAuthenticationService authenticationService, INavigationService navigationPageService, NavigationModalService navigationModalService, ModalContainer modalRepository, IOptions<AppSettings> settings)
        {
            Name = "ModalLogin"; //Can be in VMbase -> Modlog - viewmodel
            _navigationModalService = navigationModalService;
            _modalContainer = modalRepository;
            _authenticationService = authenticationService;
            if (User is null || User.Email == String.Empty) { 
                User = authenticationService.UserData;
            }
            User.Password = "";            
            _navigationPageService = navigationPageService;
            //IconPath = settings.Value.FullLogoPath;
        }

        [RelayCommand]
        public void CloseModal()
        {
            _modalContainer.Close();
        }

        [RelayCommand]
        public async Task Login()
        {
            await _authenticationService.Login(User, OnLogged);

            User.IsAuthenticated = _authenticationService.GetIsAuthenticated();
        }
        [RelayCommand]
        public void SignUp()
        {
          _navigationModalService.NavigateTo<ModalRegisterViewModel>();
        }

        [RelayCommand]
        public async Task Logout()
        {
            await _authenticationService.Logout(OnLogged);
            User.IsAuthenticated = _authenticationService.GetIsAuthenticated();
        }

        [RelayCommand]
        public async Task ChangePassword()
        {
            await _authenticationService.ResetPassword(User.Email, OnLogged);
        }

        public void OnLogged(object sender, LoginEventArgs e)
        {
            var status = e.Status;

            switch (status) {
                case LoginStatus.Valid: {
                        Status = "Logged In";
                        Task.Delay(1000);

                        //TODO Consider injecting custom behaviour

                        //_navigationPageService.NavigateTo<PageStartViewModel>();
                        _modalContainer.Close();
                        break;
                }
                case LoginStatus.InValidCridentials: {
                        Status = "Invalid credentials";
                        break;
                }
                case LoginStatus.Unavaliable: {
                        Status = "No Internet connection";
                        break;
                }
                case LoginStatus.NotVerified: {
                        Status = "Email Not Verified. Check your email";
                        break;
                }
                case LoginStatus.Error: {
                        Status = "Error: " + e.Data.ToString();
                        break;
                    }
                case LoginStatus.Expired: {
                        Status = "Expired License";
                        break;
                    }
                case LoginStatus.PasswordChanged: {
                        _navigationModalService.NavigateTo<ModalPasswordChangedViewModel>();
                        Status = "Password Changed";
                        break;
                    }
                case LoginStatus.PasswordNotChanged: {
                        Status = "Password not Changed - To many attempts";
                        break;
                    }
                case LoginStatus.PasswordWeak: {
                        Status = "Password Weak";
                        break;
                    }
                case LoginStatus.Logout: {
                        Status = "Logged out"; 
                        _navigationModalService.NavigateTo<ModalLoginViewModel>();
                        break;
                    }
                case LoginStatus.Created: {
                        Status = "Created. Verify email";
                        _navigationModalService.NavigateTo<ModalRegisteredViewModel>();
                        break;
                    }
                case LoginStatus.Exists: {
                        Status = "User already exists.";
                        break;
                    }
                case LoginStatus.InvalidEmail: {
                        Status = "Invalid Email.";
                        break;
                    }
            };
        }
    }
}
