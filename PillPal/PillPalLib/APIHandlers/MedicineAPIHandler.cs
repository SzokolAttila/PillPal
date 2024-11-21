using PillPalLib.DTOs.MedicineDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class MedicineAPIHandler
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions();

        //able to pass HttpClient in constructor so it can be tested
        public MedicineAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null)
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

        public Medicine GetMedicine(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/Medicine/{id}").Result;
            if (message.IsSuccessStatusCode)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Medicine>(json, _options)!;
            }
            throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            var message = _httpClient.GetAsync("PillPal/Medicine").Result;
            if (message.IsSuccessStatusCode)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<IEnumerable<Medicine>>(json, _options)!;
            }
            throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
        }


        public void CreateMedicine(CreateMedicineDto medicine, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string json = JsonSerializer.Serialize(medicine);
            var message = _httpClient.PostAsync("PillPal/Medicine", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!message.IsSuccessStatusCode)
            {
                throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
            }
        }

        public void UpdateMedicine(int id, CreateMedicineDto medicine, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string json = JsonSerializer.Serialize(medicine);
            var message = _httpClient.PutAsync($"PillPal/Medicine/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!message.IsSuccessStatusCode)
            {
                throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
            }
        }
        public void DeleteMedicine(int id, string auth)
        {
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.DeleteAsync($"PillPal/Medicine/{id}").Result;
            if (!message.IsSuccessStatusCode)
            {
                throw new ArgumentException(message.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
