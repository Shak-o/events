using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Newtonsoft.Json;

namespace Client
{
    public class ClientService : IClientService
    {

        public async Task<List<EventModel>> GetAll(string token, string baseUrl, string target)
        {
            List<EventModel> events = new List<EventModel>();

            using var client = MakeClient(token, baseUrl);
            HttpResponseMessage response = await client.GetAsync("api/" + target);

            if (response.IsSuccessStatusCode)
            {
                var eveResponse = response.Content.ReadAsStringAsync().Result;
                events = JsonConvert.DeserializeObject<List<EventModel>>(eveResponse);
            }

            return events;
        }

        public async Task<EventModel> GetOne(string token, string baseUrl, string target, int id)
        {
            EventModel userEvent = new EventModel();

            using var client = MakeClient(token, baseUrl);
            HttpResponseMessage response = await client.GetAsync("api/" + target + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                var eveResponse = response.Content.ReadAsStringAsync().Result;
                userEvent = JsonConvert.DeserializeObject<EventModel>(eveResponse);
            }

            return userEvent;

        }

        public async Task AddEvent(EventModel userEvent, string token, string baseUrl, string target)
        {
            using var client = MakeClient(token, baseUrl);
            var convertedData = JsonConvert.SerializeObject(userEvent);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");
            await client.PostAsync("api/" + target, data);
        }

        public async Task UpdateEvent(EventModel userEvent, string token, string baseUrl, string target, string currentUserId)
        {
            using var client = MakeClient(token, baseUrl);
            var convertedData = JsonConvert.SerializeObject(userEvent);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");
            await client.PutAsync("api/" + target + "?requesterid=" + currentUserId, data);
        }

        public async Task AddAttendant(string token, string baseUrl, string target, int id, string userId)
        {
            using var client = MakeClient(token, baseUrl);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(target);
            stringBuilder.Append("/");
            stringBuilder.Append(id);
            stringBuilder.Append("?attendantId=");
            stringBuilder.Append(userId);
            var convertedData = JsonConvert.SerializeObject(userId);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");

            await client.PutAsync(stringBuilder.ToString(), data);
        }

        public async Task<string> Authenticate(string baseUrl, string controller, Credentials credentials)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var convertedData = JsonConvert.SerializeObject(credentials);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/" + controller, data);

            if (result.IsSuccessStatusCode)
            {

                var response = await result.Content.ReadAsStringAsync();

                return response.Trim(new char[] { '\\', '\"' });
            }

            return null;
        }

        public async Task SetEndDate(string token, string baseUrl, string controller, int id, DateTime date)
        {
            using var client = MakeClient(token, baseUrl);
            var convertedData = JsonConvert.SerializeObject(date);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(controller);
            stringBuilder.Append("/");
            stringBuilder.Append(id);
            stringBuilder.Append("?date=");
            stringBuilder.Append(date);
            await client.PutAsync(stringBuilder.ToString(), data);
        }
        public async Task SetAlive(string token, string baseUrl, string controller, int id)
        {
            using var client = MakeClient(token, baseUrl);
            var convertedData = JsonConvert.SerializeObject(id);
            var data = new StringContent(convertedData, Encoding.UTF8, "application/json");

            await client.PutAsync("api/" + controller + "/" + id, data);
        }
        private HttpClient MakeClient(string token, string baseUrl)
        {
            var client = new HttpClient {BaseAddress = new Uri(baseUrl)};

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}
