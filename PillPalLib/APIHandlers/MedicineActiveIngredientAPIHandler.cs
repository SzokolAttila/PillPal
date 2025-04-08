using PillPalLib.DTOs.MedicineActiveIngredientDTOs;
using PillPalLib.StaticTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class MedicineActiveIngredientAPIHandler : APIHandlerBase
    {
        public MedicineActiveIngredientAPIHandler(HttpClient? client = null) : base(client: client)
        {
        
        }
        public IEnumerable<MedicineActiveIngredient> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/MedicineActiveIngredient").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<MedicineActiveIngredient>>(json, _options)!;
        }

        public IEnumerable<MedicineActiveIngredient> Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/MedicineActiveIngredient/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<MedicineActiveIngredient>>(json, _options)!;
        }
        public void CreateMedicineActiveIngredient(CreateMedicineActiveIngredientDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PostAsync("PillPal/MedicineActiveIngredient", content).Result;
            ExceptionHandler.CheckHttpResponse(message);

        }

        public void EditMedicineActiveIngredient(int id, CreateMedicineActiveIngredientDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PutAsync($"PillPal/MedicineActiveIngredient/{id}", content).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void DeleteMedicineActiveIngredient(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/MedicineActiveIngredient/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }   
    }
}
