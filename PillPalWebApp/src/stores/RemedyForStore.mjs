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
    },
    async destroyRemedyFor(id){
      await http.delete(`RemedyFor/${id}`);
      const idx = this.remedyFors.findIndex(x => x.id == id);
      this.remedyFors.splice(idx, 1);
    },
    async postRemedyFor(data){
      const response = await http.post('RemedyFor', data);
      this.remedyFors.push(response.data);
    },
    async updateRemedyFor(id, data){
      const response = await http.put(`RemedyFor/${id}`, data);
      const idx = this.remedyFors.findIndex(x => x.id == id);
      this.remedyFors.splice(idx, 1, response.data);
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
