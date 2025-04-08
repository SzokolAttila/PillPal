using PillPalLib.DTOs.MedicineSideEffectDTOs;
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
    public class MedicineSideEffectAPIHandler : APIHandlerBase
    {
        public MedicineSideEffectAPIHandler(HttpClient? client = null) : base(client: client)
        {

        }
        public IEnumerable<MedicineSideEffect> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/MedicineSideEffect").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<MedicineSideEffect>>(json, _options)!;
        }

        public IEnumerable<MedicineSideEffect> Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/MedicineSideEffect/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<MedicineSideEffect>>(json, _options)!;
        }
        public void CreateMedicineSideEffect(CreateMedicineSideEffectDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PostAsync("PillPal/MedicineSideEffect", content).Result;
            ExceptionHandler.CheckHttpResponse(message);

        }

        public void EditMedicineSideEffect(int id, CreateMedicineSideEffectDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PutAsync($"PillPal/MedicineSideEffect/{id}", content).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void DeleteMedicineSideEffect(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/MedicineSideEffect/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
