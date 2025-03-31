import { defineStore } from "pinia";
import { http } from '@utils/http'

export const useMedicineRemedyForStore = defineStore("medicineRemedyFor-store", {
    state(){
        return {
            medicineRemedyFors: []
        }
    },
    actions: {
        async getMedicineRemedyFors(id){
            const response = await http.get(`MedicineRemedyFor/${id}`);
            this.medicineRemedyFors = response.data;
        },
        async addMedicineRemedyFor(data){
            const response = await http.post(`MedicineRemedyFor`, data);
            if(response.status === 200){
                this.medicineRemedyFors.push(response.data);
                return true;
            }
            return false;
        },
        async deleteMedicineRemedyFor(id){
            const response = await http.delete(`MedicineRemedyFor/${id}`);
            if(response.status === 204){
                this.medicineRemedyFors = this.medicineRemedyFors.filter(r => r.id !== id);
                return true;
            }
            return false;
        },
        async updateMedicineRemedyFor(id, data){
            const response = await http.put(`MedicineRemedyFor/${id}`, data);
            if(response.status === 200){
                let idx = this.medicineRemedyFors.findIndex(r => r.id === id);
                if(idx !== -1){
                    this.medicineRemedyFors[idx] = response.data;
                }
                return true;
            }
            return false;
        }
    }
});