<script>
import { useUserStore } from '@stores/UserStore.mjs';
import { mapActions } from 'pinia';

export default{
  methods:{
    ...mapActions(useUserStore, ["getUsers"])
  },
  async mounted(){
    await this.getUsers();
  }
}
</script>

<template>
  <RouterView />
</template>
