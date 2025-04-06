<template>
    <BaseSpinner v-if="loading" class="mt-16 mx-auto" />
    <div v-else>
        <FormKit type="text" placeholder="Keresett mértékegység..." v-model="searchPackageUnit" />
        <div class="h-16 overflow-y-auto text-textColor-light dark:text-textColor-dark">
            <div class="rounded-md p-2 hover:bg-component-light dark:hover:bg-component-dark" v-for="unit in filteredPackageUnits" @click="selectPackageUnit(unit)">{{ unit.name }}</div>
        </div>
        <div class="border-2 border-component-light dark:border-component-dark p-2 rounded-md my-4">
            <FormKit type="form" v-if="packageUnitId != null" :actions="false" @submit="editPackageUnit">
                <FormKit validation="required|length:1,20" :validation-messages="{required: 'A mértékegység megadása kötelező.', length: 'A mértékegység hossza 1 és 20 karakter között kell legyen.'}" type="text" name="name" label="Mértékegység szerkesztése" v-model="packageUnit" />
                <div class="flex flex-row flex-nowrap justify-between">
                    <FormKit type="submit" label="Módosít" />
                    <button type="button" @click="deletePackageUnit" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Töröl
                    </button>
                    <button type="button" @click="cancelEdit" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Mégse
                    </button>
                </div>
            </FormKit>
            <FormKit v-else type="form" :actions="false" @submit="createPackageUnit">
                <FormKit :validation-messages="{required: 'A mértékegység megadása kötelező.', length: 'A mértékegység hossza 1 és 20 karakter között kell legyen.'}" label="Új mértékegység" type="text" name="name" validation="required|length:1,20" placeholder="Mértékegység"  />
                <FormKit type="submit" label="Hozzáad"/>
            </FormKit>
        </div>
    </div>
</template>

<script>
import BaseSpinner from '@components/layout/BaseSpinner.vue';
import { mapActions, mapState } from 'pinia';
import { usePackageUnitStore } from '@stores/PackageUnitStore.mjs';
export default {
    data(){
        return {
            filteredPackageUnits: [],
            packageUnitId: null,
            packageUnit: "",
            searchPackageUnit: "",
            loading: true
        }
    },
    components: {
        BaseSpinner
    },
    watch: {
        async searchPackageUnit(){
            this.filteredPackageUnits = this.packageUnits
            .filter(x => x.name.toLowerCase()
                .includes(this.searchPackageUnit.toLocaleLowerCase()))
            .sort((a, b) => a.name.localeCompare(b.name));
        }
    },
    computed: {
        ...mapState(usePackageUnitStore, ["packageUnits"]),
    },
    methods: {
        ...mapActions(usePackageUnitStore, ["postPackageUnit", "getPackageUnits", "updatePackageUnit", "destroyPackageUnit"]),
        selectPackageUnit(packageUnit){
            this.packageUnitId = packageUnit.id;
            this.packageUnit = packageUnit.name;
        },
        cancelEdit(){
            this.packageUnitId = null;
        },
        async editPackageUnit(data){
            if (data.name != this.packageUnits.find(x => x.id == this.packageUnitId).name)
            {
                if (!this.packageUnits.map(x => x.name).includes(data.name)){
                    await this.updatePackageUnit(this.packageUnitId, data);
                    alert("Sikeres módosítás.");
                    this.$emit('redirect');  
                }
                else alert("Már létezik ez a mértékegység.");
            }
            else alert("A mértékegységet nem változtatta.");
        },
        async createPackageUnit(data){
            if (!this.packageUnits.map(x => x.name).includes(data.name)){
                await this.postPackageUnit(data);
                alert("Sikeres létrehozás.");
                this.$emit('redirect'); 
            }
            else alert("Már létezik ez a mértékegység.")
        },
        async deletePackageUnit(){
            if(confirm("Biztosan törölni akarja a mértékegységet?"))
            {
                await this.destroyPackageUnit(this.packageUnitId);
                alert("Sikeres törlés.");
                this.$emit('redirect');
            }
        },
    },
    emits: ['redirect'],
    async mounted(){
        await this.getPackageUnits();
        this.filteredPackageUnits = this.packageUnits.sort((a, b) => a.name.localeCompare(b.name));
        this.loading = false;
    }
}
</script>