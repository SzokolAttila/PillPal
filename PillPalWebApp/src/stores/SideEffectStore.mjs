import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const useSideEffectStore = defineStore('sideEffect-store', {
  state() {
    return {
      sideEffects: []
    }
  },
  actions: {
    async getSideEffects(){
      let resp = await http.get('SideEffect');
      this.sideEffects = resp.data;
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
