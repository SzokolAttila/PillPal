using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace PillPalLib.APIHandlers
{
    public class ReminderAPIHandler
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public ReminderAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null)
        {
            if (client == null)
            {
                _httpClient = new()
                {
                    BaseAddress = new Uri(baseURL)
                };
            }
            else
            {
                _httpClient = client;
            }
        }
        public IEnumerable<Reminder> GetAll(string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync("PillPal/Reminder").Result;
            if (message.IsSuccessStatusCode)
            {
                var json = message.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<IEnumerable<Reminder>>(json, _options)!;
            }
            throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
        }
        public IEnumerable<Reminder> Get(int id, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync($"PillPal/Reminder/{id}").Result;
            if (message.IsSuccessStatusCode)
            {
                var json = message.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<IEnumerable<Reminder>>(json, _options)!;
            }
            throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
        }
    }
}
