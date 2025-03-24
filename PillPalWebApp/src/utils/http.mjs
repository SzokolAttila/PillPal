import axios from 'axios'

export const http = axios.create({
    baseURL: "https://dw94fxb7-5236.euw.devtunnels.ms/PillPal",
    headers:{
        "Accept": "application/json",
        "Content-Type": "application/json" 
    }
})
