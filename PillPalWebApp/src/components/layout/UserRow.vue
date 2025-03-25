<template>
<div class="grid grid-cols-3 p-2 rounded-md bg-formBackground-light items-center dark:bg-formBackground-dark">
<p class="text-formTextColor-light dark:text-formTextColor-dark">{{ user.userName }}</p>
<p class="text-formTextColor-light dark:text-formTextColor-dark">{{ reminderCount }}</p>
<input type="button" @click="removeUser" class="rounded-md p-2 w-[70%] font-bold text-center bg-component-light dark:bg-component-dark text-textColor-light dark:text-textColor-dark" value="Töröl"></input>
</div>
</template>

<script>
import {useUserStore} from '@stores/UserStore';
import { mapActions } from 'pinia';

export default{
    props: ["user"],
    methods:{
        ...mapActions(useUserStore, ['deleteUser']),
        removeUser(){
            this.deleteUser(this.user.id);
        }   
    },
    computed:{
        reminderCount(){
            return `${this.user.reminders.length} db emlékeztető`;
        }
    }
}
</script>