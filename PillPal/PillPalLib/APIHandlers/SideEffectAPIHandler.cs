using PillPalLib.DTOs.SideEffectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class SideEffectAPIHandler
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public SideEffectAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null)
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
        public IEnumerable<SideEffect> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/SideEffect").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<SideEffect>>(json, _options)!;
        }
        public SideEffect Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/SideEffect/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<SideEffect>(json, _options)!;
        }
        public void CreateSideEffect(CreateSideEffectDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PostAsync("PillPal/SideEffect", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void EditSideEffect(int id, CreateSideEffectDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PutAsync($"PillPal/SideEffect/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void DeleteSideEffect(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/SideEffect/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
