using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace HealthTek_Web_V3.Services
{
    public class UserInformation
    {
        private readonly HttpRequest _request;
        public UserInformation(HttpRequest request)
        {
            _request = request;
        }
        /// <summary>
        /// This method is used to retrieve the remote 
        /// Ip Address of a user that is requesting access
        /// to the application i.e. 10.0.0.1
        /// </summary>
        /// <returns>User IpAddress</returns>
        public string GetIpAddress()
        {
            string userip = _request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (userip != null)
            {
                Int64 macinfo = new Int64();
                string macSrc = macinfo.ToString("X");
                if (macSrc == "0")
                {
                    if (userip == "127.0.0.1")
                    {
                        _request.HttpContext.Response.WriteAsync("visited Localhost!");
                    }
                }
            }
            return userip;
        }
        /// <summary>
        /// This method is used to retrieve browser
        /// information of a user that is requesting access
        /// to the application i.e. Google Chrome
        /// </summary>
        /// <returns>Browser Info</returns>
        public string getBrowser()
        {
            var userAgent = _request.Headers["sec-ch-ua"].ToString();
            var split = userAgent.Split(",");
            return split[1];
        }
        /// <summary>
        /// This method is used to retrieve the device
        /// information of a user that is requesting access
        /// to the application i.e. Windows NT 10
        /// </summary>
        /// <returns>Agent Info</returns>
        public string getAgent()
        {
            var userAgent = _request.Headers["User-Agent"].ToString();
            var newAgent = userAgent.Substring(userAgent.IndexOf("(") + 1, userAgent.IndexOf(";"));
            var agent = newAgent.Substring(0, newAgent.LastIndexOf("."));
            return agent;
        }
        /// <summary>
        /// This method is used to retrieve the device
        /// information of a user that is requesting access
        /// to the application i.e. Miami, Fl, 33142
        /// </summary>
        /// <returns>User Location</returns>
        public string GetLocation(string ipAddress)
        {
            // When geting ipaddress, call this function and pass ipaddress as given below
            var newclient = new RestClient("https://ipinfo.io/?token=3873ca7575d5f4");
            var newrequest = new RestRequest();
            newrequest.Method = Method.Get;
            newrequest.AddHeader("accept", "application/json");
            RestResponse newresponse = newclient.Execute(newrequest);

            if (newresponse.StatusCode == HttpStatusCode.OK)
            {
                var json = JsonConvert.DeserializeObject<IpInfo>(newresponse.Content);
                return json.location;
            }
            return null;
        }
        /// <summary>
        /// Internal Class to Split and Fill
        /// Ip Address physical location
        /// </summary>
        internal class IpInfo
        {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? ip { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? hostname { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? city { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? region { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? country { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? loc { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? org { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? postal { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            public string? timezone { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

            public string location
            {
                get
                {
                    string temp = city + ", " + region + ", " + postal + ", " + country;
                    return temp;
                }
            }
        }
    }
}
