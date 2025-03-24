import axios from 'axios'

export const http = axios.create({
    baseURL: "https://dw94fxb7-5236.euw.devtunnels.ms/PillPal",
    headers:{
        "Accept": "application/json",
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        "Authorization": `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJhZG1pbmlzdHJhdG9yIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3NDI4OTkxOTQsImlzcyI6Ik1pIGJvY3PDoXRqdWsga2kgYSB0b2tlbnQiLCJhdWQiOiJOZWtpayBhZGp1ayBraSBhIHRva2VudCJ9.bi9_sxxOUuUMJFFm4eIYeMBP7jophwG10S7MNbchSOM`
    }
})
