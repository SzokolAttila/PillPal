<template>
  <BaseLayout>
    <h1 class="text-center text-4xl my-8 text-textColor-light dark:text-textColor-dark">Új gyógyszeradatok felvétele</h1>
    <hr class="bg-component-light dark:bg-component-dark p-1 my-5 w-[70%] mx-auto">
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-4 w-[85%] mx-auto">
        <SideEffectForms @redirect="redirect"/>
        <ActiveIngredientForms @redirect="redirect" />
        <RemedyForForms class="md:col-span-2 lg:col-span-1" @redirect="redirect" />
    </div>
  </BaseLayout>
</template>

<script>
import BaseSpinner from '@components/layout/BaseSpinner.vue';
import BaseLayout from '@layouts/BaseLayout.vue';
import SideEffectForms from '@components/SideEffectForms.vue';
import ActiveIngredientForms from '@components/ActiveIngredientForms.vue';
import RemedyForForms from '@components/RemedyForForms.vue';
export default {
    components: {
        BaseLayout,
        BaseSpinner,
        SideEffectForms,
        ActiveIngredientForms,
        RemedyForForms
    },
    methods: {
        redirect(){
            this.$router.push({name: 'login'});
        }
    },
}
</script>

<route lang="json">
    {
        "name": "new-data"
    }
</route>