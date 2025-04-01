<template>
<div class="p-2 m-2 rounded-xl border-8 border-component-light dark:border-component-dark">
    <h3 class="m-2 text-2xl text-textColor-light dark:text-textColor-dark">Kiszerelések</h3>
    <div v-if="loaded">
        <div class="h-16 overflow-y-auto">
            <p v-if="packageSizes.length<1" class="mt-2 text-center text-textColor-light dark:text-textColor-dark">A lista még üres</p>
            <PackageSizeRow v-else v-for="packageSize in packageSizes" :key="packageSize.id"
            :packageSize="packageSize"
            @deletePackageSize="removePackageSize"
            @updatePackageSize="editPackageSize"/>
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
            let success = await this.deletePackageSize(id);
            if(!success){
                alert("Hiba történt kiszerelés törlésekor!");
            }
        },
        async editPackageSize(packageSize){
            if(this.packageSizes.filter(p => p.size === packageSize.size && p.id !== packageSize.id).length >= 1){
                alert("A kiszerelés már létezik!");
                return;
            }

            let data = {
                medicineId: this.medicine.id,
                size: packageSize.size
            };
            let success = await this.updatePackageSize(packageSize.id, data);
            if(!success){
                alert(`Hiba történt kiszerelés (${packageSize.size}) frissítésekor!`);
            }
        },
        async newPackageSize(){
            let data = {
                medicineId: this.medicine.id,
                size: this.packageSizes.length>0 ? (Math.max(...this.packageSizes.map(x => x.size))+1) : 1
            };
            let success = await this.addPackageSize(data);
            if(!success){
                alert("Hiba történt új kiszerelés hozzáadásakor!");
            }
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