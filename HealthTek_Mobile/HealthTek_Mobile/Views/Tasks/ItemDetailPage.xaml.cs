using HealthTek_Mobile.ViewModels;
using Xamarin.Forms;

namespace HealthTek_Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}