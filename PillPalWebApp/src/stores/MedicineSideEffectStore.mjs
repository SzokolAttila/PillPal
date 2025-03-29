import { defineStore } from "pinia";
import { http } from '@utils/http'

export const useMedicineSideEffectStore = defineStore("medicineSideEffect-store", {
    state(){
        return {
            medicineSideEffects: []
        }
    },
    actions: {
        async getMedicineSideEffects(id){
            const response = await http.get(`MedicineSideEffect/${id}`);
            this.medicineSideEffects = response.data;
        },
        async addMedicineSideEffect(data){
            const response = await http.post(`MedicineSideEffect`, data);
            this.medicineSideEffects.push(response.data);
        },
        async deleteMedicineSideEffect(id){
            await http.delete(`MedicineSideEffect/${id}`);
            this.medicineSideEffects
            .splice(this.medicineSideEffects.findIndex((effect) => effect.id === id), 1);
        },
        async updateMedicineSideEffect(id, data){
            const response = await http.put(`MedicineSideEffect/${id}`, data);
            this.medicineSideEffects
            .splice(this.medicineSideEffects.findIndex((effect) => effect.id === id), 1, response.data);
        }
    }
});