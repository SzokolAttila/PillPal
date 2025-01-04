using PillPalLib.DTOs.MedicineRemedyForDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class MedicineRemedyForAPIHandler : APIHandlerBase
    {
        public MedicineRemedyForAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null) : base(baseURL, client)
        {

        }
        public IEnumerable<MedicineRemedyFor> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/MedicineRemedyFor").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<MedicineRemedyFor>>(json, _options)!;
        }

        public IEnumerable<MedicineRemedyFor> Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/MedicineRemedyFor/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<MedicineRemedyFor>>(json, _options)!;
        }
        public void CreateMedicineRemedyFor(CreateMedicineRemedyForDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PostAsync("PillPal/MedicineRemedyFor", content).Result;
            ExceptionHandler.CheckHttpResponse(message);

        }

        public void EditMedicineRemedyFor(int id, CreateMedicineRemedyForDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PutAsync($"PillPal/MedicineRemedyFor/{id}", content).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void DeleteMedicineRemedyFor(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/MedicineRemedyFor/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
