using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class PetTabbedPageViewModel : ViewModelBase
    {
        public PetTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pet";
        }
    }
}