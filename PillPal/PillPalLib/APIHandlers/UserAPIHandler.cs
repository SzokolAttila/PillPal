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
    public class UserAPIHandler : APIHandlerBase
    {
        public UserAPIHandler(HttpClient? client = null) : base(client: client)
        {

        }

        public LoginDto Login(CreateUserDto user)
        {
            string json = JsonSerializer.Serialize(user);
            var message = _httpClient.PostAsync("PillPal/Login", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
            return JsonSerializer.Deserialize<LoginDto>(message.Content.ReadAsStringAsync().Result, _options)!;
        }

        public User GetUser(int id, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync($"PillPal/User/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            string json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<User>(json, _options)!;
        }

        public IEnumerable<User> GetUsers(string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync("PillPal/User").Result;
            ExceptionHandler.CheckHttpResponse(message);
            string json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<User>>(json, _options)!;
        }

        public void CreateUser(CreateUserDto user)
        {
            string json = JsonSerializer.Serialize(user);
            var message = _httpClient.PostAsync("PillPal/User", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void UpdateUser(int id, CreateUserDto user, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string json = JsonSerializer.Serialize(user);
            var message = _httpClient.PutAsync($"PillPal/User/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void DeleteUser(int id, string auth) {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.DeleteAsync($"PillPal/User/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
