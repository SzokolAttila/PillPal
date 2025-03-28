import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const useActiveIngredientStore = defineStore('activeIngredient-store', {
  state() {
    return {
      activeIngredients: []
    }
  },
  actions: {
    async getActiveIngredients(){
      let resp = await http.get('ActiveIngredient');
      this.activeIngredients = resp.data;
    }
  },
  getters: {
    activeIngredientOptions(){
      const options = {}
      for(let ingredient of this.activeIngredients){
        options[ingredient.id] = ingredient.ingredient;
      }
      return options;
    }
  }
})
