using HealthTek_Mobile.ViewModels;
using Xamarin.Forms;

namespace HealthTek_Mobile.Views
{
    public partial class EmployeesPage : ContentPage
    {
        EmployeesViewModel _viewModel;

        public EmployeesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EmployeesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}