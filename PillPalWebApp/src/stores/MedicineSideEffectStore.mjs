import { defineStore } from "pinia";
import { http } from '@utils/http'

export const useMedicineSideEffectStore = defineStore("medicineSideEffect-store", {
    actions: {
        async getMedicineSideEffects(id){
            const response = await http.get(`MedicineSideEffect/${id}`);
            return response.data;
        },
        async addMedicineSideEffect(data){
            const response = await http.post(`MedicineSideEffect`, data);
            return response.data;
        },
        async deleteMedicineSideEffect(id){
            const response = await http.delete(`MedicineSideEffect/${id}`);
            return response.data;
        },
        async updateMedicineSideEffect(id, data){
            const response = await http.put(`MedicineSideEffect/${id}`, data);
            return response.data;
        }
    }
});