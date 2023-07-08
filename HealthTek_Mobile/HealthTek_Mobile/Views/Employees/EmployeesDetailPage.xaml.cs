using HealthTek_Mobile.ViewModels;
using Xamarin.Forms;

namespace HealthTek_Mobile.Views
{
    public partial class EmployeesDetailPage : ContentPage
    {
        public EmployeesDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}