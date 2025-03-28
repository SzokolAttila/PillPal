<template>
<BaseLayout>
    <div v-if="loaded">
        <FormKit type="text" placeholder="Keresett gyógyszer..." v-model="searchText"/>
        <div class="h-10 overflow-y-auto">
            <div v-for="med of showedMedicines" @click="selectMed(med)">{{ med.name }}</div>
        </div>
        <SideEffects :medicine="medicine"/>
        <ActiveIngredients :medicine="medicine"/>
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</BaseLayout>
</template>

<script>
import BaseLayout from '@layouts/BaseLayout.vue'
import SideEffects from '@components/layout/MedicineSideEffect/SideEffects.vue'
import ActiveIngredients from '@components/layout/MedicineActiveIngredient/ActiveIngredients.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import {useMedicineStore} from '@stores/MedicineStore'
import {mapActions, mapState} from 'pinia'

export default{
    components: {
        BaseLayout,
        SideEffects,
        BaseSpinner,
        ActiveIngredients
    },
    data(){
        return {
            medicine: { id: 1},
            showedMedicines: [],
            searchText: "",
            loaded: false
        }
    },
    methods: {
        ...mapActions(useMedicineStore, ["getMedicines"]),
        selectMed(medicine){
            this.medicine = { ...medicine };
        }
    },
    async mounted(){
        await this.getMedicines();
        this.loaded = true;
    },
    computed: {
        ...mapState(useMedicineStore, ["medicines"])
    },
    watch: {
        searchText(){
            this.showedMedicines = this.medicines
            .filter(medicine => medicine.name.toLowerCase().includes(this.searchText.toLowerCase()));
        }
    }
}
</script>

<route lang="json">
{
    "name": "edit-medicine"
}
</route>