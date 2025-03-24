<template>
<BaseLayout>
    <div v-if="loaded">
        <FormKit type="text" v-model="searchText"/>
        <UserRow v-for="user in users" :user="user" />
    </div>
    <BaseSpinner v-else/>
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
            searchText: ""
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
        this.loaded = true;
    },
    computed: {
        ...mapState(useUserStore, ['users'])
    },
    watch:{
        searchText(oldtext, newtext){
            this.users = this.getUsers();
            this.users = this.users.filter(user => user.UserName.includes(newtext));
        }
    }
}
</script>