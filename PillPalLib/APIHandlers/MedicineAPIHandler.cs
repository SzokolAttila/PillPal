using PillPalLib.DTOs.MedicineDTOs;
using PillPalLib.StaticTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class MedicineAPIHandler : APIHandlerBase
    {
        public MedicineAPIHandler(HttpClient? client = null) : base(client: client)
        {

        }

        public Medicine GetMedicine(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/Medicine/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            string json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<Medicine>(json, _options)!;
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            var message = _httpClient.GetAsync("PillPal/Medicine").Result;
            ExceptionHandler.CheckHttpResponse(message);
            string json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<Medicine>>(json, _options)!;
        }


        public void CreateMedicine(CreateMedicineDto medicine, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string json = JsonSerializer.Serialize(medicine);
            var message = _httpClient.PostAsync("PillPal/Medicine", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void UpdateMedicine(int id, CreateMedicineDto medicine, string auth)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            string json = JsonSerializer.Serialize(medicine);
            var message = _httpClient.PutAsync($"PillPal/Medicine/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void DeleteMedicine(int id, string auth)
        {
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth);
            var message = _httpClient.DeleteAsync($"PillPal/Medicine/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
