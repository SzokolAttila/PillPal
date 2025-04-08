using PillPalLib.DTOs.ActiveIngredientDTOs;
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
    public class ActiveIngredientAPIHandler : APIHandlerBase
    {
        public ActiveIngredientAPIHandler(HttpClient? client = null) : base(client: client)
        {
            
        }
        public IEnumerable<ActiveIngredient> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/ActiveIngredient").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<ActiveIngredient>>(json, _options)!;
        }
        public ActiveIngredient Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/ActiveIngredient/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<ActiveIngredient>(json, _options)!;
        }
        public void CreateActiveIngredient(CreateActiveIngredientDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PostAsync("PillPal/ActiveIngredient", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void EditActiveIngredient(int id, CreateActiveIngredientDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PutAsync($"PillPal/ActiveIngredient/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void DeleteActiveIngredient(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/ActiveIngredient/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
