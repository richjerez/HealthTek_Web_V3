using Xamarin.Essentials;

namespace HealthTek_Mobile
{
    public static class Constants
    {
        // URL of REST service
        // URL of REST service (Android does not use localhost)
        // Use http cleartext for local deployment. Change to https for production

        public static string BaseUrl = @"https://73.244.55.9:44332/api/";
        public static string LoginRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "identitylogin?username={0}?password={1}"
            : BaseUrl + "identitylogin?username={0}?password={1}";
        public static string EmployeesRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "employees/{0}"
            : BaseUrl + "employees/{0}";
        public static string ClientsRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "clients/{0}"
            : BaseUrl + "clients/{0}";
        public static string AppointmentsRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "appointments/{0}"
            : BaseUrl + "appointments/{0}";
        public static string AuthorizationsRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "authorizations/{0}"
            : BaseUrl + "authorizations/{0}";
        public static string BaProgressNotesRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "baprogressnotes/{0}"
            : BaseUrl + "baprogressnotes/{0}";
        public static string DocumentationProcessesRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "documentationprocesses/{0}"
            : BaseUrl + "documentationprocesses/{0}";
        public static string TasksRestUrl = DeviceInfo.Platform
            == DevicePlatform.Android
            ? BaseUrl + "tasks/{0}"
            : BaseUrl + "tasks/{0}";
    }
}