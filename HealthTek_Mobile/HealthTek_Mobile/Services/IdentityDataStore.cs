using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthTek_Mobile.Services
{
    public class IdentityDataStore
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public IdentityDataStore()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<string> RefreshDataAsync(string username, string password)
        {
            string content = "";
            Uri uri = new Uri(string.Format(Constants.LoginRestUrl, username, password));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return content;
        }
    }
}