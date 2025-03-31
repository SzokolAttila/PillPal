<template>
    <BaseSpinner v-if="loading" class="mt-16 mx-auto" />
    <div v-else>
        <FormKit type="text" placeholder="Keresett mellékhatás..." v-model="searchSideEffect" />
        <div class="h-16 overflow-y-auto text-textColor-light dark:text-textColor-dark">
            <div class="rounded-md p-2 hover:bg-component-light dark:hover:bg-component-dark" v-for="effect in filteredSideEffects" @click="selectSideEffect(effect)">{{ effect.effect }}</div>
        </div>
        <div class="border-2 border-component-light dark:border-component-dark p-2 rounded-md my-4">
            <FormKit type="form" v-if="sideEffectId != null" :actions="false" @submit="editSideEffect">
                <FormKit type="text" name="effect" label="Mellékhatás szerkesztése" v-model="sideEffect" />
                <div class="flex flex-row flex-nowrap justify-between">
                    <FormKit type="submit" label="Módosít" />
                    <button type="button" @click="deleteSideEffect" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Töröl
                    </button>
                    <button type="button" @click="cancelEdit" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Mégse
                    </button>
                </div>
            </FormKit>
            <FormKit v-else type="form" :actions="false" @submit="createSideEffect">
                <FormKit label="Új mellékhatás" type="text" name="effect" validation="required|length:3,80" placeholder="Mellékhatás"  />
                <FormKit type="submit" label="Hozzáad"/>
            </FormKit>
        </div>
    </div>
</template>

<script>
import BaseSpinner from '@components/layout/BaseSpinner.vue';
import { mapActions, mapState } from 'pinia';
import { useSideEffectStore } from '@stores/SideEffectStore.mjs';
export default {
    data(){
        return {
            filteredSideEffects: [],
            sideEffectId: null,
            sideEffect: "",
            searchSideEffect: "",
            loading: true
        }
    },
    components: {
        BaseSpinner
    },
    watch: {
        async searchSideEffect(){
            this.filteredSideEffects = this.sideEffects
            .filter(x => x.effect.toLowerCase()
                .includes(this.searchSideEffect.toLocaleLowerCase()))
            .sort((a, b) => a.effect.localeCompare(b.effect));
        }
    },
    computed: {
        ...mapState(useSideEffectStore, ["sideEffects"]),
    },
    methods: {
        ...mapActions(useSideEffectStore, ["postSideEffect", "getSideEffects", "updateSideEffect", "destroySideEffect"]),
        selectSideEffect(sideEffect){
            this.sideEffectId = sideEffect.id;
            this.sideEffect = sideEffect.effect;
        },
        cancelEdit(){
            this.sideEffectId = null;
        },
        async editSideEffect(data){
            if (data.effect != this.sideEffects.find(x => x.id == this.sideEffectId).effect)
            {
                if (!this.sideEffects.map(x => x.effect).includes(data.effect)){
                    await this.updateSideEffect(this.sideEffectId, data);
                    alert("Sikeres módosítás.");
                    this.$emit('redirect');  
                }
                else alert("Már létezik ez a mellékhatás.");
            }
            else alert("A mellékhatást nem változtatta.");
        },
        async createSideEffect(data){
            if (!this.sideEffects.map(x => x.effect).includes(data.effect)){
                await this.postSideEffect(data);
                alert("Sikeres létrehozás.");
                this.$emit('redirect'); 
            }
            else alert("Már létezik ez a mellékhatás.")
        },
        async deleteSideEffect(){
            if(confirm("Biztosan törölni akarja a mellékhatást?"))
            {
                await this.destroySideEffect(this.sideEffectId);
                alert("Sikeres törlés.");
                this.$emit('redirect');
            }
        },
    },
    emits: ['redirect'],
    async mounted(){
        await this.getSideEffects();
        this.filteredSideEffects = this.sideEffects.sort((a, b) => a.effect.localeCompare(b.effect));
        this.loading = false;
    }
}
</script>