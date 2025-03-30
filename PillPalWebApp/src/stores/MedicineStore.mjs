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
      let resp = await http.post('Medicine', medicine);
      if(resp.status === 200){
        this.medicines.push(resp.data);
        return true;
      }
      return false;
    },
    async updateMedicine(id, medicine){
      let resp = await http.put(`Medicine/${id}`, medicine);
      if(resp.status === 200){
        let index = this.medicines.findIndex(m => m.id === medicine.id);
        if(index !== -1){
          this.medicines[index] = resp.data;
        }
        return true;
      }
      return false;
    },
    async deleteMedicine(id){
      let resp = await http.delete(`Medicine/${id}`);
      if(resp.status === 204){
        this.medicines = this.medicines.filter(m => m.id !== id);
        return true;
      }
      return false;
    }
  }
})
