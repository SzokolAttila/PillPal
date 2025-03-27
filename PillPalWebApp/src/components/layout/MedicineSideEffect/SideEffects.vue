<template>
<div>
    <h3>Mellékhatások</h3>
    <div v-if="loaded">
        <div v-for="sideEffect of sideEffects">
            <SideEffectRow :sideEffectOptions="sideEffectOptions" :sideEffect="sideEffect" @deleteSideEffect="deleteSideEffect" @updateSideEffect="updateSideEffect"/>
        </div>
        <FormKit type="button" label="Mellékhatás hozzáadása" @click="addSideEffect"/>
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
            sideEffects: [],
            loaded: false
        }
    },
    components: {
        SideEffectRow,
        BaseSpinner
    },
    props: ["medicine"],
    computed: {
        ...mapState(useSideEffectStore, ['sideEffectOptions'])
    },
    methods: {
        ...mapActions(useSideEffectStore, ['getSideEffects']),
        ...mapActions(useMedicineSideEffectStore, ['getMedicineSideEffects', 'addMedicineSideEffect',
         'deleteMedicineSideEffect', 'updateMedicineSideEffect']),
        async deleteSideEffect(id){
            await this.deleteMedicineSideEffect(id);
            this.sideEffects = await this.getMedicineSideEffects(this.medicine.id);
        },
        async updateSideEffect(medicineSideEffect){
            let data = {
                medicineId: this.medicine.id,
                sideEffectId: medicineSideEffect.sideEffectId
            };
            await this.updateMedicineSideEffect(medicineSideEffect.id, data);
        },
        async addSideEffect(){
            let data = {
                medicineId: this.medicine.id,
                sideEffectId: 1
            };
            await this.addMedicineSideEffect(data);
            this.sideEffects = await this.getMedicineSideEffects(this.medicine.id);
        }
    },
    async mounted(){
        await this.getSideEffects();
        this.sideEffects = await this.getMedicineSideEffects(this.medicine.id);
        this.loaded = true;
    }
}
</script>