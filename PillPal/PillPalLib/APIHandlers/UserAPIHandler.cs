using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class UserAPIHandler
    {
        private readonly HttpClient _httpClient;

        //able to pass HttpClient in constructor so it can be tested
        public UserAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null)
        {
            if (client == null)
            {
                _httpClient = new();
                _httpClient.BaseAddress = new Uri(baseURL);
            }
            else
            {
                _httpClient = client;
            }
        }

        public string? Login(CreateUserDto user)
        {
            string json = JsonSerializer.Serialize(user);
            var message = _httpClient.PostAsync("api/Login", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (message.IsSuccessStatusCode)
            {
                return message.Content.ReadAsStringAsync().Result;
            }
            return null;
        }

        public User? GetUser(int id, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync($"PillPal/User/{id}").Result;
            if (message.IsSuccessStatusCode)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<User>(json);
            }
            return null;
        }

        public IEnumerable<User> GetUsers(string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync("PillPal/User").Result;
            if (message.IsSuccessStatusCode)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<IEnumerable<User>>(json);
            }
            return null;
        }

        public bool CreateUser(CreateUserDto user)
        {
            string json = JsonSerializer.Serialize(user);
            var message = _httpClient.PostAsync("PillPal/User", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            return message.IsSuccessStatusCode;
        }

        public bool UpdateUser(int id, CreateUserDto user, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string json = JsonSerializer.Serialize(user);
            var message = _httpClient.PutAsync($"PillPal/User/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            return message.IsSuccessStatusCode;
        }

        public bool DeleteUser(int id, string auth) {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.DeleteAsync($"PillPal/User/{id}").Result;
            return message.IsSuccessStatusCode;
        }
    }
}
