<template>
<div class="p-2 m-2 rounded-xl border-8 border-component-light dark:border-component-dark">
    <h3 class="m-2 text-2xl text-textColor-light dark:text-textColor-dark">Hatóanyagok</h3>
    <div v-if="loaded">
        <div v-for="activeIngredient in medicineActiveIngredients" :key="activeIngredient.id">
            <ActiveIngredientRow :activeIngredientOptions="activeIngredientOptions" :activeIngredient="activeIngredient" @deleteActiveIngredient="deleteActiveIngredient" @updateActiveIngredient="updateActiveIngredient"/>
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
            await this.deleteMedicineActiveIngredient(id);
        },
        async updateActiveIngredient(medicineActiveIngredient){
            let data = {
                medicineId: this.medicine.id,
                activeIngredientId: medicineActiveIngredient.activeIngredientId
            };
            await this.updateMedicineActiveIngredient(medicineActiveIngredient.id, data);
        },
        async addActiveIngredient(){
            let data = {
                medicineId: this.medicine.id,
                activeIngredientId: 1
            };
            await this.addMedicineActiveIngredient(data);
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