﻿using MyVet.Common.Helpers;
using MyVet.Common.Models;
using MyVet.Common.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace MyVet.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _registerCommand;

        public RegisterPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Register New User";
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(Register));

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void Register()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var request = new UserRequest
            {
                Address = Address,
                Document = Document,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Phone = Phone
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.RegisterUserAsync(
                url,
                "api",
                "/Account",
                request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok",response.Message,"Accept");
            await _navigationService.GoBackAsync();
        }

    private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Document))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Documento", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Nombre", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Apellido", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Domicilio", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Email) || !RegexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Email", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Phone))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Teléfono", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Password", "Aceptar");
                return false;
            }

            if (Password.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert("Error", "El Password debe contener al menos 6 caracteres", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar Confirmación de Password", "Aceptar");
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert("Error", "El Password y su confirmación no coinciden", "Aceptar");
                return false;
            }

            return true;
        }
    }
}
