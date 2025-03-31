<template>
<div class="p-2 m-2 rounded-xl border-8 border-component-light dark:border-component-dark">
    <h3 class="m-2 text-2xl text-textColor-light dark:text-textColor-dark">Betegségek</h3>
    <div v-if="loaded">
        <div class="h-16 overflow-y-auto">
            <p v-if="medicineRemedyFors.length<1" class="mt-2 text-center text-textColor-light dark:text-textColor-dark">A lista még üres</p>
            <RemedyForRow v-else v-for="remedyFor in medicineRemedyFors" :key="remedyFor.id" 
            :remedyForOptions="remedyForOptions"
            :remedyFor="remedyFor"
            @deleteRemedyFor="deleteRemedyFor"
            @updateRemedyFor="updateRemedyFor"/>
        </div>
        <FormKit type="button" label="Betegség hozzáadása" @click="addRemedyFor"/>
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</div>
</template>

<script>
import RemedyForRow from '@components/layout/MedicineRemedyFor/RemedyForRow.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import { useRemedyForStore } from '@stores/RemedyForStore.mjs'
import { useMedicineRemedyForStore } from '@stores/MedicineRemedyForStore.mjs'
import { mapActions, mapState } from 'pinia'

export default{
    data(){
        return{
            loaded: false
        }
    },
    components: {
        RemedyForRow,
        BaseSpinner
    },
    props: ["medicine"],
    computed: {
        ...mapState(useRemedyForStore, ['remedyForOptions']),
        ...mapState(useMedicineRemedyForStore, ['medicineRemedyFors'])
    },
    methods: {
        ...mapActions(useRemedyForStore, ['getRemedyFors']),
        ...mapActions(useMedicineRemedyForStore, ['getMedicineRemedyFors', 'addMedicineRemedyFor',
         'deleteMedicineRemedyFor', 'updateMedicineRemedyFor']),
        async deleteRemedyFor(id){
            let success = await this.deleteMedicineRemedyFor(id);
            if(!success){
                alert("Hiba történt betegség törlésekor!");
            }
        },
        async updateRemedyFor(medicineRemedyFor){
            let data = {
                medicineId: this.medicine.id,
                remedyForId: medicineRemedyFor.remedyForId
            };
            let success = await this.updateMedicineRemedyFor(medicineRemedyFor.id, data);
            if(!success){
                alert(`Hiba történt ${medicineRemedyFor.ailment} frissítésekor!`);
            }
        },
        async addRemedyFor(){
            let data = {
                medicineId: this.medicine.id,
                remedyForId: 1
            };
            let success = await this.addMedicineRemedyFor(data);
            if(!success){
                alert("Hiba történt új betegség hozzáadásakor!")
            }
        }
    },
    async mounted(){
        await this.getRemedyFors();
        await this.getMedicineRemedyFors(this.medicine.id);
        this.loaded = true;
    },
    watch:{
        async medicine(){
            this.loaded = false;
            await this.getMedicineRemedyFors(this.medicine.id);
            this.loaded = true;
        }
    }
}
</script>