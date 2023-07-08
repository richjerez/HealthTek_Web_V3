using HealthTek_Mobile.Models;
using HealthTek_Mobile.ViewModels;
using Xamarin.Forms;

namespace HealthTek_Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}