import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const usePackageUnitStore = defineStore('packageUnit-store', {
  state() {
    return {
      packageUnits: []
    }
  },
  actions: {
    async getPackageUnits(){
      let resp = await http.get('PackageUnit');
      this.packageUnits = resp.data;
    },
    async postPackageUnit(data){
      const response = await http.post('PackageUnit', data)
      this.packageUnits.push(response.data);
    },
    async destroyPackageUnit(id){
      await http.delete(`PackageUnit/${id}`);
      const idx = this.packageUnits.findIndex(x => x.id == id);
      this.packageUnits.splice(idx, 1);
    },
    async updatePackageUnit(id, data){
      const response = await http.put(`PackageUnit/${id}`, data);
      const idx = this.packageUnits.findIndex(x => x.id == id);
      this.packageUnits.splice(idx, 1, response.data);
    }
  },
  getters: {
    packageUnitOptions(){
      const options = {}
      for(let unit of this.packageUnits){
        options[unit.id] = unit.name;
      }
      return options;
    }
  }
})
