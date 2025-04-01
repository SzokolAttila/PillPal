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
            try{
                const response = await http.post(`MedicineActiveIngredient`, data);
                this.medicineActiveIngredients.push(response.data);
                return true;
            }catch(error){
                return false;
            }
        },
        async deleteMedicineActiveIngredient(id){
            try{
                await http.delete(`MedicineActiveIngredient/${id}`);
                this.medicineActiveIngredients = this.medicineActiveIngredients.filter(i => i.id !== id);
                return true;
            }catch(error){
                return false;
            }
        },
        async updateMedicineActiveIngredient(id, data){
            try{
                const response = await http.put(`MedicineActiveIngredient/${id}`, data);
                let index = this.medicineActiveIngredients.findIndex(i => i.id === id);
                if(index !== -1){
                    this.medicineActiveIngredients[index] = response.data;
                }
                return true;
            }catch(error){
                return false;
            }
        }
    }
});