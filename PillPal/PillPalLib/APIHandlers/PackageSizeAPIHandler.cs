using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using PillPalLib.DTOs.PackageSizeDTOs;

namespace PillPalLib.APIHandlers
{
    public class PackageSizeAPIHandler : APIHandlerBase
    {
        public PackageSizeAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null) : base(baseURL, client)
        {

        }
        public IEnumerable<PackageSize> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/PackageSize").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<PackageSize>>(json, _options)!;    
        }
        public IEnumerable<PackageSize> Get(int id) 
        {
            var message = _httpClient.GetAsync($"PillPal/PackageSize/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<PackageSize>>(json, _options)!;
        }

        public void CreatePackageSize(CreatePackageSizeDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PostAsync("PillPal/PackageSize", content).Result;
            ExceptionHandler.CheckHttpResponse(message);

        }

        public void EditPackageSize(int id, CreatePackageSizeDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var message = _httpClient.PutAsync($"PillPal/PackageSize/{id}", content).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }

        public void DeletePackageSize(int id, string token) 
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/PackageSize/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
