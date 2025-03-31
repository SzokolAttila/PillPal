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
    },
    async destroyActiveIngredient(id){
      await http.delete(`ActiveIngredient/${id}`);
      const idx = this.activeIngredients.findIndex(x => x.id == id);
      this.activeIngredients.splice(idx, 1);
    },
    async postActiveIngredient(data){
      const response = await http.post('ActiveIngredient', data);
      this.activeIngredients.push(response.data);
    },
    async updateActiveIngredient(id, data){
      const response = await http.put(`ActiveIngredient/${id}`, data);
      const idx = this.activeIngredients.findIndex(x => x.id == id);
      this.activeIngredients.splice(idx, 1, response.data);
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
