<template>
<div class="p-2 m-2 rounded-xl border-8 border-component-light dark:border-component-dark">
    <h3 class="m-2 text-2xl text-textColor-light dark:text-textColor-dark">Hatóanyagok</h3>
    <div v-if="loaded">
        <div class="h-16 overflow-y-auto">
            <p v-if="medicineActiveIngredients.length<1" class="mt-2 text-center text-textColor-light dark:text-textColor-dark">A lista még üres</p>
            <ActiveIngredientRow v-else v-for="activeIngredient in medicineActiveIngredients" :key="activeIngredient.id"
            :activeIngredientOptions="activeIngredientOptions"
            :activeIngredient="activeIngredient"
            @deleteActiveIngredient="deleteActiveIngredient"
            @updateActiveIngredient="updateActiveIngredient"/>
        </div>
        <FormKit type="button" label="Hatóanyag hozzáadása" @click="addActiveIngredient"/>
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</div>
</template>

<script>
import ActiveIngredientRow from '@components/layout/MedicineActiveIngredient/ActiveIngredientRow.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import { useActiveIngredientStore } from '@stores/ActiveIngredientStore.mjs'
import { useMedicineActiveIngredientStore } from '@stores/MedicineActiveIngredientStore.mjs'
import { mapActions, mapState } from 'pinia'

export default{
    data(){
        return{
            loaded: false
        }
    },
    components: {
        ActiveIngredientRow,
        BaseSpinner
    },
    props: ["medicine"],
    computed: {
        ...mapState(useActiveIngredientStore, ['activeIngredientOptions']),
        ...mapState(useMedicineActiveIngredientStore, ['medicineActiveIngredients'])
    },
    methods: {
        ...mapActions(useActiveIngredientStore, ['getActiveIngredients']),
        ...mapActions(useMedicineActiveIngredientStore, ['getMedicineActiveIngredients',
         'addMedicineActiveIngredient',
         'deleteMedicineActiveIngredient',
         'updateMedicineActiveIngredient']),
        async deleteActiveIngredient(id){
            let success = await this.deleteMedicineActiveIngredient(id);
            if(!success){
                alert('Hiba történt hatóanyag törlésekor!');
            }
        },
        async updateActiveIngredient(medicineActiveIngredient){
            let data = {
                medicineId: this.medicine.id,
                activeIngredientId: medicineActiveIngredient.activeIngredientId
            };
            let success = await this.updateMedicineActiveIngredient(medicineActiveIngredient.id, data);
            if(!success){
                alert(`Hiba történt hatóanyag (${this.activeIngredientOptions[medicineActiveIngredient.activeIngredientId]}) frissítésekor!`);
            }
        },
        async addActiveIngredient(){
            let data = {
                medicineId: this.medicine.id,
                activeIngredientId: Object.keys(this.activeIngredientOptions)[0]
            };
            let success = await this.addMedicineActiveIngredient(data);
            if(!success){
                alert(`Hiba történt új hatóanyag hozzáadásakor!`);
            }
        }
    },
    async mounted(){
        await this.getActiveIngredients();
        await this.getMedicineActiveIngredients(this.medicine.id);
        this.loaded = true;
    },
    watch:{
        async medicine(){
            this.loaded = false;
            await this.getMedicineActiveIngredients(this.medicine.id);
            this.loaded = true;
        }
    }
}
</script>