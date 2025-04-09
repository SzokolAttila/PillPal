<template>
<div class="p-2 m-2 rounded-xl border-8 border-component-light dark:border-component-dark">
    <h3 class="m-2 text-2xl text-textColor-light dark:text-textColor-dark">Mellékhatások</h3>
    <div v-if="loaded">
        <div class="h-16 overflow-y-auto">
            <p v-if="medicineSideEffects.length<1" class="mt-2 text-center text-textColor-light dark:text-textColor-dark">A lista még üres</p>
            <SideEffectRow v-else v-for="sideEffect in medicineSideEffects" :key="sideEffect.id"
            :sideEffectOptions="sideEffectOptions"
            :sideEffect="sideEffect"
            @deleteSideEffect="deleteSideEffect"
            @updateSideEffect="updateSideEffect"/>
        </div>
        <FormKit type="button" id="addEffectBtn" label="Mellékhatás hozzáadása" @click="addSideEffect"/>
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</div>
</template>

<script>
import SideEffectRow from '@components/layout/MedicineSideEffect/SideEffectRow.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import { useSideEffectStore } from '@stores/SideEffectStore.mjs'
import { useMedicineSideEffectStore } from '@stores/MedicineSideEffectStore.mjs'
import { mapActions, mapState } from 'pinia'

export default{
    data(){
        return{
            loaded: false
        }
    },
    components: {
        SideEffectRow,
        BaseSpinner
    },
    props: ["medicine"],
    computed: {
        ...mapState(useSideEffectStore, ['sideEffectOptions']),
        ...mapState(useMedicineSideEffectStore, ['medicineSideEffects'])
    },
    methods: {
        ...mapActions(useSideEffectStore, ['getSideEffects']),
        ...mapActions(useMedicineSideEffectStore, ['getMedicineSideEffects', 'addMedicineSideEffect',
         'deleteMedicineSideEffect', 'updateMedicineSideEffect']),
        async deleteSideEffect(id){
            let success = await this.deleteMedicineSideEffect(id);
            if(!success){
                alert("Hiba történt mellékhatás törlésekor!");
            }
        },
        async updateSideEffect(medicineSideEffect){
            let data = {
                medicineId: this.medicine.id,
                sideEffectId: medicineSideEffect.sideEffectId
            };
            let success = await this.updateMedicineSideEffect(medicineSideEffect.id, data);
            if(!success){
                alert(`Hiba történt mellékhatás (${this.sideEffectOptions[medicineSideEffect.sideEffectId]}) frissítésekor!`);
            }
        },
        async addSideEffect(){
            let data = {
                medicineId: this.medicine.id,
                sideEffectId: Object.keys(this.sideEffectOptions)[0]
            };
            let success = await this.addMedicineSideEffect(data);
            if(!success){
                alert("Hiba történt új mellékhatás hozzáadásakor!");
            }
        }
    },
    async mounted(){
        await this.getSideEffects();
        await this.getMedicineSideEffects(this.medicine.id);
        this.loaded = true;
    },
    watch:{
        async medicine(){
            this.loaded = false;
            await this.getMedicineSideEffects(this.medicine.id);
            this.loaded = true;
        }
    }
}
</script>