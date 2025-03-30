<template>
<BaseLayout>
    <h1 class="text-center text-4xl my-5 text-textColor-light dark:text-textColor-dark">Új gyógyszer felvétele</h1>
    <hr class="bg-component-light dark:bg-component-dark p-1 my-5 w-[70%] mx-auto">
    <div v-if="loaded" class="mx-auto w-[70%]">
        <FormKit type="form" :actions="false" @submit="newMedicine">
            <FormKit type="text" label="Gyógyszer neve" name="name" validation="required|length:5,30"/>
            <FormKit type="text" label="Gyártója" name="manufacturer" validation="required|length:5,30"/>
            <FormKit type="textarea" label="Leírása" name="description" validation="required|length:5,255"
             v-model="desc" :help="`${desc.length} / 255`"/>
            <FormKit type="select" label="Kiszerelés egysége" name="packageUnitId" :options="packageUnitOptions"/>
            <FormKit type="submit" label="Hozzáad"/>
        </FormKit>    
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</BaseLayout>
</template>

<script>
import BaseLayout from '@layouts/BaseLayout.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import {useMedicineStore} from '@stores/MedicineStore'
import {usePackageUnitStore} from '@stores/PackageUnitStore'
import {mapActions, mapState} from 'pinia'

export default{
    components:{
        BaseLayout,
        BaseSpinner
    },
    data(){
        return {
            loaded: false,
            desc: ""
        }
    },
    methods: {
        ...mapActions(useMedicineStore, ['addMedicine']),
        ...mapActions(usePackageUnitStore, ['getPackageUnits']),
        async newMedicine(data){
            let success = await this.addMedicine(data);
            if(success){
                alert("Új gyógyszer sikeresen hozzáadva")
            }else{
                alert("Hiba történt a gyógyszer hozzáadása során")
            }
        }
    },
    async mounted(){
        await this.getPackageUnits();
        this.loaded = true;
    },
    computed: {
        ...mapState(usePackageUnitStore, ['packageUnitOptions'])
    }
}
</script>

<route lang="json">
{
    "name": "create-medicine"
}
</route>