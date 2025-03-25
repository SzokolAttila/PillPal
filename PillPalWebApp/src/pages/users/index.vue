<template>
<BaseLayout>
    <div v-if="loaded">
        <h1 class="text-center text-4xl my-5 text-textColor-light dark:text-textColor-dark">Felhasználók</h1>
        <hr class="bg-component-light dark:bg-component-dark p-1 my-5 w-[70%] mx-auto">
        <input type="text" v-model="searchText" class="mx-auto block rounded w-[70%] bg-formBackground-light dark:bg-formBackground-dark text-formTextColor-light dark:text-formTextColor-dark placeholder-formTextColor-light dark:placeholder-formTextColor-dark" placeholder="Keresett felhasználó..."/>
        <UserRow v-for="user of userArray" :user="user" class="my-5"/>
    </div>
    <BaseSpinner v-else class="mx-auto mt-10"/>
</BaseLayout>
</template>

<script>
import { useUserStore } from '@stores/UserStore';
import { mapActions, mapState } from 'pinia';
import BaseLayout from '@layouts/BaseLayout.vue';
import BaseSpinner from '@components/layout/BaseSpinner.vue';
import UserRow from '@components/layout/UserRow.vue';

export default {
    data(){
        return {
            loaded: false,
            searchText: "",
            userArray: []
        }
    },
    components: {
        BaseLayout,
        BaseSpinner,
        UserRow
    },
    methods: {
        ...mapActions(useUserStore, ['getUsers'])
    },
    async mounted() {
        await this.getUsers();
        this.userArray = this.users;
        this.loaded = true;
    },
    computed: {
        ...mapState(useUserStore, ['users'])
    },
    watch:{
        async searchText(oldtext, newtext){
            this.userArray = this.users.filter(user => user.userName.includes(oldtext));
        }
    }
}
</script>