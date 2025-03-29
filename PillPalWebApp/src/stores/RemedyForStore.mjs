import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const useRemedyForStore = defineStore('remedyFor-store', {
  state() {
    return {
      remedyFors: []
    }
  },
  actions: {
    async getRemedyFors(){
      let resp = await http.get('RemedyFor');
      this.remedyFors = resp.data;
    }
  },
  getters: {
    remedyForOptions(){
      const options = {}
      for(let remedyFor of this.remedyFors){
        options[remedyFor.id] = remedyFor.ailment;
      }
      return options;
    }
  }
})
