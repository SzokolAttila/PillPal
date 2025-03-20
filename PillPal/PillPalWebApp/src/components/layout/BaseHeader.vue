<template>
  <nav class="bg-white">
    <div class="flex justify-between p-3 border-b-2 flex-wrap">
      <RouterLink to="/" class="flex items-center space-x-3">
        <span class="self-center text-2xl font-semibold">Feladat</span>
      </RouterLink>
      <button class="block md:hidden" @click="toggleMenu">
        <svg
          class="w-5 h-5"
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 17 14"
        >
          <path
            stroke="currentColor"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M1 1h15M1 7h15M1 13h15"
          />
        </svg>
      </button>
      <div class="w-full md:block md:w-auto" :class="{hidden: !menuOpen}">
        <ul class="flex flex-col p-4 md:flex-row md:p-0 md:space-x-8">
          <li class="block py-2 px-3 text-gray-500 hover:bg-blue-400 hover:text-white rounded p-2 has-[.active]:text-blue-500 has-[.active]:hover:text-white"><RouterLink to="#">1. oldal</RouterLink></li>
        </ul>
      </div>
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