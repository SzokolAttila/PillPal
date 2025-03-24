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
      this.users = response.data.data;
    },
    async deleteUser(){

    }
  },
  getters: {
    counter10X() {
      return this.counter * 10
    }
  }
})
