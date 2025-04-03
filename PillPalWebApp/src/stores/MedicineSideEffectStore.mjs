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
            try{
                const response = await http.post(`MedicineSideEffect`, data);
                this.medicineSideEffects.push(response.data);
                return true;
            }catch(error){
                return false;
            }
        },
        async deleteMedicineSideEffect(id){
            try{
                await http.delete(`MedicineSideEffect/${id}`);
                this.medicineSideEffects = this.medicineSideEffects.filter(e => e.id !== id);
                return true;
            }catch(error){
                return false;
            }
        },
        async updateMedicineSideEffect(id, data){
            try{
                const response = await http.put(`MedicineSideEffect/${id}`, data);
                let idx = this.medicineSideEffects.findIndex(e => e.id === id);
                if(idx !== -1){
                    this.medicineSideEffects[idx] = response.data;
                }
                return true;
            }catch(error){
                return false;
            }
        }
    }
});