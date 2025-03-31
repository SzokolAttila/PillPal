<template>
  <nav class="px-4 pb-4 h-full bg-navbar-light w-[300px] dark:bg-navbar-dark flex flex-col flex-nowrap justify-between">
    <div class="flex flex-col flex-nowrap justify-start">
      <div class="flex flex-row flex-nowrap justify-around align-middle my-2">
        <div class="bg-component-light dark:bg-component-dark rounded-full p-1">
          <img src="/pillpal_logo.png" alt="PillPal" class="h-[64px]">
        </div>
        <h5 class="text-3xl text-white my-auto">
          PillPal
        </h5>
      </div>
      <RouterLink :to="{name: 'users'}" class="bg-component-light border-component-light dark:border-component-dark dark:bg-component-dark p-4 m-1 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Felhasználók
      </RouterLink>
      <RouterLink :to="{name: 'create-medicine'}" class="bg-component-light border-component-light dark:border-component-dark dark:bg-component-dark p-4 m-1 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Új gyógyszer
      </RouterLink>
      <RouterLink :to="{name: 'new-data'}" class="bg-component-light border-component-light dark:border-component-dark dark:bg-component-dark p-4 m-1 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Új gyógyszeradatok
      </RouterLink>
      <RouterLink :to="{name: 'edit-medicine'}" class="bg-component-light border-component-light dark:border-component-dark dark:bg-component-dark p-4 m-1 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Gyógyszer szerkesztése
      </RouterLink>
    </div>
    <div>
      <button @click="signOut" class="p-4 m-1 w-full text-left text-white bg-red-600 hover:bg-red-800 dark:bg-red-800 dark:hover:bg-red-900 rounded-md" type="button">  
        Kijelentkezés
      </button>
    </div>  
  </nav>
</template>

<script>
import { useAdminStore } from '@stores/AdminStore';
import { mapActions, mapState } from 'pinia';
export default{
  data(){
    return{
      menuOpen: false
    }
  },
  computed: {
    ...mapState(useAdminStore, ["token"])
  },
  methods:{
    toggleMenu(){
      this.menuOpen = !this.menuOpen
    },
    ...mapActions(useAdminStore, ['logout']),
    signOut(){
      if (this.token == null)  
        alert("Ehhez a művelethez be kell jelentkeznie!");
      else if (confirm("Biztosan ki akar jelentkezni?"))
      {
        this.logout();
        this.$router.push({name: 'login'});
      }
    }
  }
}
</script>