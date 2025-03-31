import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const useSideEffectStore = defineStore('sideEffect-store', {
  state() {
    return {
      sideEffects: []
    }
  },
  actions: {
    async destroySideEffect(id){
      await http.delete(`SideEffect/${id}`);
      const idx =  this.sideEffects.findIndex(x => x.id == id);
      this.sideEffects.splice(idx, 1);
    },
    async getSideEffects(){
      let resp = await http.get('SideEffect');
      this.sideEffects = resp.data;
    },
    async postSideEffect(data){
      const response = await http.post('SideEffect', data);
      this.sideEffects.push(response.data);
    },
    async updateSideEffect(id, data){
      const response = await http.put(`SideEffect/${id}`, data);
      const idx = this.sideEffects.findIndex(x => x.id == id);
      this.sideEffects.splice(idx, 1, response.data);
    }
  },
  getters: {
    sideEffectOptions(){
      const options = {}
      for(let effect of this.sideEffects){
        options[effect.id] = effect.effect;
      }
      return options;
    }
  }
})
