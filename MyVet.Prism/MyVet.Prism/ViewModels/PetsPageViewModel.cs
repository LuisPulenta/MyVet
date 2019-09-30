using MyVet.Common.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MyVet.Prism.ViewModels
{
    public class PetsPageViewModel : ViewModelBase
    {
        private OwnerResponse _owner;
        private ObservableCollection<PetResponse> _pets;
        public PetsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pets";
        }
        public ObservableCollection<PetResponse> Pets
        {
            get => _pets;
            set => SetProperty(ref _pets, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("owner"))
            {
                _owner = parameters.GetValue<OwnerResponse>("owner");
                Title = $"Pets of: {_owner.FullName}";
                Pets = new ObservableCollection<PetResponse>(_owner.Pets);
            }
        }
    }
}
