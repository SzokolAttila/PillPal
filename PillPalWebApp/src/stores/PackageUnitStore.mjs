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
