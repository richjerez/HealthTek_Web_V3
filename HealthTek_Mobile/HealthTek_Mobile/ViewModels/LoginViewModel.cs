using HealthTek_Mobile.Services;
using HealthTek_Mobile.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace HealthTek_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public string username { get; set; }
        public string password { get; set; }
        public string success { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            IsBusy = true;

            try
            {
                IdentityDataStore mockDataStore = new IdentityDataStore();
                var items = await mockDataStore.RefreshDataAsync(username, password);
                if (items != null)
                {
                    success = items;
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
