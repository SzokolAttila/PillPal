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
      let resp = await http.post(`PackageSize`, data);
      if(resp.status === 200){
        this.packageSizes.push(resp.data);
        return true;
      }
      return false;
    },
    async deletePackageSize(id){
      let resp = await http.delete(`PackageSize/${id}`);
      if(resp.status === 204){
        this.packageSizes = this.packageSizes.filter(s => s.id !== id);
        return true;
      }
      return false;
    },
    async updatePackageSize(id, data){
      let resp = await http.put(`PackageSize/${id}`, data);
      if(resp.status === 200){
        let idx = this.packageSizes.findIndex(s => s.id === id);
        if(idx !== -1){
          this.packageSizes[idx] = resp.data;
        }
        return true;
      }
      return false;
    }
  }
})
