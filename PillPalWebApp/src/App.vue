<script>
import { mapState } from 'pinia';
import {useUserStore} from '@stores/UserStore'
import { useAdminStore } from '@stores/AdminStore.mjs';
export default {
  watch: {
    async users(){
      if(useAdminStore().token != null)
      {
        if ((await useUserStore().getUser(useAdminStore().id)) == null)
        {
          useAdminStore().logout();
          this.$router.push({name: 'login'});
          alert("Szép munka! Törölted a saját admin felhasználódat. :D");
        }
      }
    }
  },
  computed: {
    ...mapState(useUserStore, ["users"])
  }
}
</script>

<template>
  <RouterView />
</template>
