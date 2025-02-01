﻿using PillPalLib.DTOs.PackageUnitDTOs;
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
    public class PackageUnitAPIHandler : APIHandlerBase
    {
        public PackageUnitAPIHandler(string baseURL = "http://localhost:5236/", HttpClient? client = null) : base(baseURL, client)
        {

        }
        public IEnumerable<PackageUnit> GetAll()
        {
            var message = _httpClient.GetAsync("PillPal/PackageUnit").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<IEnumerable<PackageUnit>>(json, _options)!;
        }
        public PackageUnit Get(int id)
        {
            var message = _httpClient.GetAsync($"PillPal/PackageUnit/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
            var json = message.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<PackageUnit>(json, _options)!;
        }
        public void CreateRemedyFor(CreatePackageUnitDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PostAsync("PillPal/PackkageUnit", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void EditRemedyFor(int id, CreatePackageUnitDto createDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = JsonSerializer.Serialize(createDto);
            var message = _httpClient.PutAsync($"PillPal/PackageUnit/{id}", new StringContent(json, Encoding.UTF8, "application/json")).Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
        public void DeleteRemedyFor(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var message = _httpClient.DeleteAsync($"PillPal/PackageUnit/{id}").Result;
            ExceptionHandler.CheckHttpResponse(message);
        }
    }
}
