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
            try{
                const response = await http.post(`MedicineRemedyFor`, data);
                this.medicineRemedyFors.push(response.data);
                return true;
            }catch(error){
                return false;
            }
        },
        async deleteMedicineRemedyFor(id){
            try{
                await http.delete(`MedicineRemedyFor/${id}`);
                this.medicineRemedyFors = this.medicineRemedyFors.filter(r => r.id !== id);
                return true;
            }catch(error){
                return false;
            }
        },
        async updateMedicineRemedyFor(id, data){
            try{
                const response = await http.put(`MedicineRemedyFor/${id}`, data);
                let idx = this.medicineRemedyFors.findIndex(r => r.id === id);
                if(idx !== -1){
                    this.medicineRemedyFors[idx] = response.data;
                }
                return true;
            }catch(error){
                return false;
            }
        }
    }
});