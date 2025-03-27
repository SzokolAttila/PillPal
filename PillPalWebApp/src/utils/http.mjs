import axios from 'axios'
import {useAdminStore} from '@stores/AdminStore'
import {router} from '@/router/index'

export const http = axios.create({
    baseURL: "https://dw94fxb7-5236.euw.devtunnels.ms/PillPal",
    headers:{
        "Accept": "application/json",
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
    }
});

http.interceptors.request.use(config => {
    const token = useAdminStore().token; 
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }else if(router.currentRoute.value.name !== 'login'){
        router.push({ name: 'login'});
        throw new axios.Cancel('Nincs bejelentkezve, ezért átirányítjuk.');
    }
    return config;
}, error => {
    return Promise.reject(error);
});