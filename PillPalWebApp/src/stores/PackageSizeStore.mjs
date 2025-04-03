import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const usePackageSizeStore = defineStore('packageSize-store', {
  state() {
    return {
      packageSizes: []
    }
  },
  actions: {
    async getPackageSizes(medicineId){
      let resp = await http.get(`PackageSize/${medicineId}`);
      this.packageSizes = resp.data;
    },
    async addPackageSize(data){
      try{
        let resp = await http.post(`PackageSize`, data);
        this.packageSizes.push(resp.data);
        return true;
      }catch(error){
        return false;
      }
    },
    async deletePackageSize(id){
      try{
        await http.delete(`PackageSize/${id}`);
        this.packageSizes = this.packageSizes.filter(s => s.id !== id);
        return true;
      }catch(error){
        return false;
      }
    },
    async updatePackageSize(id, data){
      try{
        let resp = await http.put(`PackageSize/${id}`, data);
        let idx = this.packageSizes.findIndex(s => s.id === id);
        if(idx !== -1){
          this.packageSizes[idx] = resp.data;
        }
        return true;
      }catch(error){
        return false;
      }
    }
  }
})
