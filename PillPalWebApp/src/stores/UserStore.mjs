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
    async deleteUser(id){
      let resp = await http.delete(`User/${id}`);
      if(resp.status === 204){
        this.users = this.users.filter(u => u.id !== id);
        return true;
      }
      return false;
    }
  }
})
