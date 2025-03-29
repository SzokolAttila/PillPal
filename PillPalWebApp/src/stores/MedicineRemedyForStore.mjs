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
            this.medicineRemedyFors.push(response.data);
        },
        async deleteMedicineRemedyFor(id){
            await http.delete(`MedicineRemedyFor/${id}`);
            this.medicineRemedyFors
            .splice(this.medicineRemedyFors.findIndex((effect) => effect.id === id), 1);
        },
        async updateMedicineRemedyFor(id, data){
            const response = await http.put(`MedicineRemedyFor/${id}`, data);
            this.medicineRemedyFors
            .splice(this.medicineRemedyFors.findIndex((effect) => effect.id === id), 1, response.data);
        }
    }
});