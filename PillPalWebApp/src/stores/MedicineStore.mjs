import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const useMedicineStore = defineStore('medicine-store', {
  state() {
    return {
      medicines: []
    }
  },
  actions: {
    async getMedicines(){
      let resp = await http.get('Medicine');
      this.medicines = resp.data;
    },
    async addMedicine(medicine){
      try{
        let resp = await http.post('Medicine', medicine);
        this.medicines.push(resp.data);
        return true;
      }catch(error){
        return false;
      }
    },
    async updateMedicine(id, medicine){
      try{
        let resp = await http.put(`Medicine/${id}`, medicine);
        let index = this.medicines.findIndex(m => m.id === medicine.id);
        if(index !== -1){
          this.medicines[index] = resp.data;
        }
        return true;
      }catch(error){
        return false;
      }
    },
    async deleteMedicine(id){
      try{
        await http.delete(`Medicine/${id}`);
        this.medicines = this.medicines.filter(m => m.id !== id);
        return true;
      }catch(error){
        return false;
      }
    }
  }
})
