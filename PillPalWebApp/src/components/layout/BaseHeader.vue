<template>
  <nav class="bg-navbar-light dark:bg-navbar-dark flex flex-col flex-nowrap justify-between">
    <div class="flex flex-col flex-nowrap justify-start">
      
      <RouterLink :to="{name: 'users'}" class="bg-component-light dark:bg-component-dark p-4 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Felhasználók
      </RouterLink>
      <RouterLink :to="{name: 'new-medicine'}" class="bg-component-light dark:bg-component-dark p-4 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Új gyógyszer
      </RouterLink>
      <RouterLink :to="{name: 'new-data'}" class="bg-component-light dark:bg-component-dark p-4 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Új gyógyszeradatok
      </RouterLink>
      <RouterLink :to="{name: 'update-medicine'}" class="bg-component-light dark:bg-component-dark p-4 hover:bg-activeBackground-light dark:hover:bg-activeBackground-dark text-black hover:text-white dark:text-white rounded-md">
        Gyógyszer szerkesztése
      </RouterLink>
    </div>
    <div>
      <button type="button">  
        Kijelentkezés
      </button>
    </div>  
  </nav>
</template>

<script>
export default{
  data(){
    return{
      menuOpen: false
    }
  },
  methods:{
    toggleMenu(){
      this.menuOpen = !this.menuOpen
    }
  }
}
</script>