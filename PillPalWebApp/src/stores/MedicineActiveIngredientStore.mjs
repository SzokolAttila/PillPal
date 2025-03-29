import { defineStore } from "pinia";
import { http } from '@utils/http'

export const useMedicineActiveIngredientStore = defineStore("medicineActiveIngredient-store", {
    state(){
        return {
            medicineActiveIngredients: []
        }
    },
    actions: {
        async getMedicineActiveIngredients(id){
            const response = await http.get(`MedicineActiveIngredient/${id}`);
            this.medicineActiveIngredients = response.data;
        },
        async addMedicineActiveIngredient(data){
            const response = await http.post(`MedicineActiveIngredient`, data);
            this.medicineActiveIngredients.push(response.data);
        },
        async deleteMedicineActiveIngredient(id){
            await http.delete(`MedicineActiveIngredient/${id}`);
            this.medicineActiveIngredients
            .splice(this.medicineActiveIngredients.findIndex((ingredient) => ingredient.id === id), 1);
        },
        async updateMedicineActiveIngredient(id, data){
            const response = await http.put(`MedicineActiveIngredient/${id}`, data);
            this.medicineActiveIngredients
            .splice(this.medicineActiveIngredients.findIndex((ingredient) => ingredient.id === id), 1, response.data);
        }
    }
});