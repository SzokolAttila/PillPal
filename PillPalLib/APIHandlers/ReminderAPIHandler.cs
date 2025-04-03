using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using PillPalLib.DTOs.ReminderDTOs;

namespace PillPalLib.APIHandlers
{
    public class ReminderAPIHandler : APIHandlerBase
    {
        public ReminderAPIHandler(HttpClient? client = null) : base(client: client)
        {

        }
        public IEnumerable<Reminder> GetAll(string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync("PillPal/Reminder").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<Reminder>>(json, _options)!;
        }
        public IEnumerable<Reminder> Get(int id, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.GetAsync($"PillPal/Reminder/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<Reminder>>(json, _options)!;
        }
        public Reminder? CreateReminder(CreateReminderDto reminderDto, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var json = JsonSerializer.Serialize(reminderDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PostAsync("PillPal/Reminder", content).Result;
            ExceptionHandler.CheckHttpResponse(message);
            return JsonSerializer.Deserialize<Reminder>(message.Content.ReadAsStringAsync().Result, _options);
        }
        public void EditReminder(int id, CreateReminderDto reminderDto, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var json = JsonSerializer.Serialize(reminderDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PutAsync($"PillPal/Reminder/{id}", content).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void DeleteReminder(int id, string auth) 
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.DeleteAsync($"PillPal/Reminder/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
