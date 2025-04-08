import { defineStore } from "pinia";
import { http } from '@utils/http'

export const useAdminStore = defineStore("admin", {
    persist: {
        storage: sessionStorage
    },
    state(){
        return {
            token: null,
            id: null
        }
    },
    actions: {
        async login(data){
            const response = await http.post("Login", data);
            if (data.username != 'administrator')
                throw new Error('Admin hozzáférésre van szüksége!');
            this.token = response.data.token;
            this.id = response.data.id;
        },
        async logout(){
            this.token = null;
            this.id = null;
        }
    }
});