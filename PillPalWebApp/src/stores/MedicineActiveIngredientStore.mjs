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
            if(response.status === 200){
                this.medicineActiveIngredients.push(response.data);
                return true;
            }
            return false;
        },
        async deleteMedicineActiveIngredient(id){
            const response = await http.delete(`MedicineActiveIngredient/${id}`);
            if(response.status === 204){
                this.medicineActiveIngredients = this.medicineActiveIngredients.filter(i => i.id !== id);
                return true;
            }
            return false;
        },
        async updateMedicineActiveIngredient(id, data){
            const response = await http.put(`MedicineActiveIngredient/${id}`, data);
            if(response.status === 200){
                let index = this.medicineActiveIngredients.findIndex(i => i.id === id);
                if(index !== -1){
                    this.medicineActiveIngredients[index] = response.data;
                }
                return true;
            }
            return false;
        }
    }
});