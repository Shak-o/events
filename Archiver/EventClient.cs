using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Archiver.Models;
using Newtonsoft.Json;

namespace Archiver
{
    public static class EventClient
    {
        public static async Task<List<EventModel>> GetAllEvents(string baseUrl, string controller, string method)
        {
            List<EventModel> events = new List<EventModel>();
            using var client = new HttpClient();
            var token = await Authenticate();

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage Res = await client.GetAsync("api/" + controller + method);

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                events = JsonConvert.DeserializeObject<List<EventModel>>(EmpResponse);
            }

            return events;
        }
        public static async Task DeleteEvent(string baseUrl, string controller, string method, int id)
        {
            using var client = new HttpClient();
            var token = await Authenticate();

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage Res = await client.DeleteAsync("api/" + controller + "/"+id);

            if (Res.IsSuccessStatusCode)
            {

                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

            }

        }
        public static async Task AddArchive(string baseUrl, string controller, string method, EventModel newEvent)
        {
            using var client = new HttpClient();
            var token = await Authenticate();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var convertedData = JsonConvert.SerializeObject(newEvent);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/" + controller, data);
        }

        private static async Task<string> Authenticate()
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44384/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var credentials = new Credentials() {UserName = "sparta", Password = "Qwerty"};
            var convertedData = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/auth", data);

            if (result.IsSuccessStatusCode)
            {

                var response = await result.Content.ReadAsStringAsync();

                return response.Trim(new char[] { '\\', '\"' });
            }

            return null;
        }
    }
}
