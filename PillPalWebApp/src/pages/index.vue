<template>
  <BaseLayout>
    <h1 class="text-textColor-light dark:text-textColor-dark text-center text-5xl my-10">Bejelentkezés</h1>
    <div class="w-[75%] mx-auto">
      <FormKit type="form" :actions="false" @submit="submit">
        <FormKit label="Felhasználónév" type="text" name="username" validation="required" />
        <FormKit label="Jelszó" type="password" name="password" validation="required" />
        <FormKit label="Bejelentkezés" type="submit" />
      </FormKit>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from '@layouts/BaseLayout.vue'
import { mapState, mapActions } from 'pinia'
import { useAdminStore } from '@stores/AdminStore';
export default {
  components: {
    BaseLayout
  },
  computed: {

  },
  methods: {
    ...mapActions(useAdminStore, ["login"]),
    async submit(event){
      try {
        await this.login(event);
        this.$router.push({name: 'users'});
      }
      catch (error){
        alert(error);
      }
    }
  }
}
</script>

<route lang="json">
  {
    "name": "login"
  }
</route>