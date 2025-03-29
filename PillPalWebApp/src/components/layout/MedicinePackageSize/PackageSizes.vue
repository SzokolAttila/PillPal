<template>
<div class="p-2 m-2 rounded-xl border-8 border-component-light dark:border-component-dark">
    <h3 class="m-2 text-2xl text-textColor-light dark:text-textColor-dark">Kiszerelések</h3>
    <div v-if="loaded">
        <div v-for="packageSize in packageSizes" :key="packageSize.id">
            <PackageSizeRow :packageSize="packageSize" @deletePackageSize="removePackageSize" @updatePackageSize="editPackageSize"/>
        </div>
        <FormKit type="button" label="Kiszerelés hozzáadása" @click="newPackageSize"/>
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</div>
</template>

<script>
import PackageSizeRow from '@components/layout/MedicinePackageSize/PackageSizeRow.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import { usePackageSizeStore } from '@stores/PackageSizeStore.mjs'
import { mapActions, mapState } from 'pinia'

export default{
    data(){
        return{
            loaded: false
        }
    },
    components: {
        PackageSizeRow,
        BaseSpinner
    },
    props: ["medicine"],
    computed: {
        ...mapState(usePackageSizeStore, ['packageSizes']),
    },
    methods: {
        ...mapActions(usePackageSizeStore, ['getPackageSizes', 'addPackageSize',
         'deletePackageSize', 'updatePackageSize']),
        async removePackageSize(id){
            await this.deletePackageSize(id);
        },
        async editPackageSize(packageSize){
            let data = {
                medicineId: this.medicine.id,
                size: packageSize.size
            };
            await this.updatePackageSize(packageSize.id, data);
        },
        async newPackageSize(){
            let data = {
                medicineId: this.medicine.id,
                size: (Math.max(...this.packageSizes.map(x => x.size))+1)
            };
            await this.addPackageSize(data);
        }
    },
    async mounted(){
        await this.getPackageSizes(this.medicine.id);
        this.loaded = true;
    },
    watch:{
        async medicine(){
            this.loaded = false;
            await this.getPackageSizes(this.medicine.id);
            this.loaded = true;
        }
    }
}
</script>