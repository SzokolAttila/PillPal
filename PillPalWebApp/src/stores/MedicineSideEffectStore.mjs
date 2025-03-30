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
            if(response.status === 200){
                this.medicineSideEffects.push(response.data);
                return true;
            }
            return false;
        },
        async deleteMedicineSideEffect(id){
            const response = await http.delete(`MedicineSideEffect/${id}`);
            if(response.status === 204){
                this.medicineSideEffects = this.medicineSideEffects.filter(e => e.id !== id);
                return true;
            }
            return false;
        },
        async updateMedicineSideEffect(id, data){
            const response = await http.put(`MedicineSideEffect/${id}`, data);
            if(response.status === 200){
                let idx = this.medicineSideEffects.findIndex(e => e.id === id);
                if(idx !== -1){
                    this.medicineSideEffects[idx] = response.data;
                }
                return true;
            }
            return false;
        }
    }
});