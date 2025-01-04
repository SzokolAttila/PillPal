using PillPalLib.DTOs.RemedyForDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public class RemedyForAPIHandler : APIHandlerBase
    {
        public RemedyForAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null) : base(baseURL, client)
        {

        }
        public IEnumerable<RemedyFor> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/RemedyFor").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<RemedyFor>>(json, _options)!;
        }
        public RemedyFor Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/RemedyFor/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<RemedyFor>(json, _options)!;
        }
        public void CreateRemedyFor(CreateRemedyForDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PostAsync("PillPal/RemedyFor", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void EditRemedyFor(int id, CreateRemedyForDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PutAsync($"PillPal/RemedyFor/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void DeleteRemedyFor(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/RemedyFor/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
