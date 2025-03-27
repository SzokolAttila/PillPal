import { createRouter, createWebHistory } from 'vue-router'
import { routes } from 'vue-router/auto-routes'
import { useAdminStore } from '@stores/AdminStore.mjs';

export const router = createRouter({
  history: createWebHistory(),
  linkActiveClass: 'active',
  routes
})

router.beforeEach((to, from) => {
  if (to.name != 'login' && useAdminStore().token == null){
    alert("Ehhez a művelethez be kell jelentkeznie!");
    return { name: 'login' };
  }
});