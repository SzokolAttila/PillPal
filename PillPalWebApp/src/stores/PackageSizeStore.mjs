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
      this.packageSizes.push(resp.data);
    },
    async deletePackageSize(id){
      await http.delete(`PackageSize/${id}`);
      this.packageSizes.splice(this.packageSizes.findIndex((size) => size.id === id), 1);
    },
    async updatePackageSize(id, data){
      let resp = await http.put(`PackageSize/${id}`, data);
      this.packageSizes.splice(this.packageSizes.findIndex((size) => size.id === id), 1, resp.data);
    }
  }
})
