﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PillPalLib.APIHandlers
{
    public abstract class APIHandlerBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public APIHandlerBase(string baseURL = "http://localhost:5236/", HttpClient? client = null)
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
    }
}
