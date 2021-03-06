﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyVet.Common.Helpers;
using MyVet.Common.Models;
using MyVet.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class AssignModifyAgendaPageViewModel : ViewModelBase
    {
        private AgendaResponse _agenda;
        private PetResponse _pet;
        private ObservableCollection<PetResponse> _pets;
        private bool _isRunning;
        private bool _isEnabled;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _assignCommand;
        private DelegateCommand _cancelCommand;


        public DelegateCommand AssignCommand => _assignCommand ?? (_assignCommand = new DelegateCommand(Assign));

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));


        public AssignModifyAgendaPageViewModel(
            INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Assign Modify Agenda";
            IsEnabled = true;
        }

        public AgendaResponse Agenda
        {
            get => _agenda;
            set => SetProperty(ref _agenda, value);
        }

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        public ObservableCollection<PetResponse> Pets
        {
            get => _pets;
            set => SetProperty(ref _pets, value);
        }

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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Agenda"))
            {
                Agenda = parameters.GetValue<AgendaResponse>("Agenda");
                LoadPets();
            }
        }

        private void LoadPets()
        {
            var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);
            Pets = new ObservableCollection<PetResponse>(owner.Pets);
            Pet = Pets.FirstOrDefault(p => p.Id == _agenda.Pet.Id);
        }

        private async void Assign()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);

            var request = new AssignRequest
            {
                AgendaId = Agenda.Id,
                OwnerId = owner.Id,
                PetId = Pet.Id,
                Remarks = Agenda.Remarks
            };

            var response = await _apiService.PostAsync(
                url,
                "api",
                "/Agenda/AssignAgenda",
                request,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var parameters = new NavigationParameters
            {
                { "Refresh", true }
            };

            await _navigationService.GoBackAsync(parameters);
        }

        private async Task<bool> ValidateData()
        {
            if (Pet == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Pet Error",
                    "Accept");
                return false;
            }

            return true;
        }
        private async void Cancel()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                "Confirm",
                "Cancel Agenda Message",
                "Yes",
                "No");

            if (!answer)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            var request = new UnAssignRequest { AgendaId = Agenda.Id };
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await _apiService.PostAsync(
                url,
                "api",
                "/Agenda/UnAssignAgenda",
                request,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var parameters = new NavigationParameters
    {
        { "Refresh", true }
    };

            await _navigationService.GoBackAsync(parameters);
        }


    }
}
