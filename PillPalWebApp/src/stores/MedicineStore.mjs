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
      this.medicines.push(resp.data);
    }
  }
})
