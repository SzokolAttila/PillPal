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
      await http.delete(`User/${id}`);
      this.users.splice(this.users.findIndex((user) => user.id === id), 1);
    }
  }
})
