<template>
<div class="flex justify-around rounded-md ">
<p>{{ user.UserName }}</p>
<p>{{ reminderCount }}</p>
<FormKit type="button" @click="deleteUser">Töröl</FormKit>
</div>
</template>

<script>
import {useUserStore} from '@stores/UserStore';
import { mapActions } from 'pinia';

export default{
    props: ["user"],
    methods:{
        ...mapActions(useUserStore, ['deleteUser']),
        deleteUser(){
            this.deleteUser(this.user.id);
        }   
    },
    computed:{
        reminderCount(){
            return `${this.user.Reminders.length} db emlékeztető`;
        }
    }
}
</script>