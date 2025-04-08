import { defineStore } from 'pinia'
import {http} from '@utils/http'

export const useUserStore = defineStore('user-store', {
  state() {
    return {
      users: []
    }
  },
  actions: {
    async getUsers() {
      const response = await http.get('User')
      this.users = response.data;
    },
    async getUser(id){
      try {
        const response = await http.get(`User/${id}`);
        return response.data;
      }
      catch (error)
      {
        return null;
      }
    },
    async deleteUser(id){
      try{
        await http.delete(`User/${id}`);
        this.users = this.users.filter(u => u.id !== id);
        return true;
      }catch{
        return false;
      }
    }
  }
})
