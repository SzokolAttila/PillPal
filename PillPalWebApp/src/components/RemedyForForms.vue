<template>
    <BaseSpinner v-if="loading" class="mt-16 mx-auto" />
    <div v-else>
        <FormKit type="text" placeholder="Keresett betegség..." v-model="searchRemedyFor" />
        <div class="h-16 overflow-y-auto text-textColor-light dark:text-textColor-dark">
            <div class="rounded-md p-2 hover:bg-component-light dark:hover:bg-component-dark" v-for="remedyFor in filteredRemedyFors" @click="selectRemedyFor(remedyFor)">{{ remedyFor.ailment }}</div>
        </div>
        <div class="border-2 border-component-light dark:border-component-dark p-2 rounded-md my-4">
            <FormKit type="form" v-if="remedyForId != null" :actions="false" @submit="editRemedyFor">
                <FormKit :validation-messages="{required: 'A betegség megadása kötelező.', length: 'A betegség hossza 3 és 80 karakter között kell legyen.'}" validation="required|length:3,80" type="text" name="ailment" label="Betegség szerkesztése" v-model="remedyFor" />
                <div class="flex flex-row flex-nowrap justify-between">
                    <FormKit type="submit" label="Módosít" />
                    <button type="button" @click="deleteRemedyFor" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Töröl
                    </button>
                    <button type="button" @click="cancelEdit" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Mégse
                    </button>
                </div>
            </FormKit>
            <FormKit v-else type="form" :actions="false" @submit="createRemedyFor">
                <FormKit :validation-messages="{required: 'A betegség megadása kötelező.', length: 'A betegség hossza 3 és 80 karakter között kell legyen.'}" label="Új betegség" type="text" name="ailment" validation="required|length:3,80" placeholder="Betegség"  />
                <FormKit type="submit" label="Hozzáad"/>
            </FormKit>
        </div>
    </div>
</template>

<script>
import BaseSpinner from '@components/layout/BaseSpinner.vue';
import { mapActions, mapState } from 'pinia';
import { useRemedyForStore } from '@stores/RemedyForStore.mjs';
export default {
    data(){
        return {
            filteredRemedyFors: [],
            remedyForId: null,
            remedyFor: "",
            searchRemedyFor: "",
            loading: true
        }
    },
    components: {
        BaseSpinner
    },
    watch: {
        async searchRemedyFor(){
            this.filteredRemedyFors = this.remedyFors
            .filter(x => x.ailment.toLowerCase()
                .includes(this.searchRemedyFor.toLocaleLowerCase()))
            .sort((a, b) => a.ailment.localeCompare(b.ailment));
        }
    },
    computed: {
        ...mapState(useRemedyForStore, ["remedyFors"]),
    },
    methods: {
        ...mapActions(useRemedyForStore, ["postRemedyFor", "getRemedyFors", "updateRemedyFor", "destroyRemedyFor"]),
        selectRemedyFor(remedyFor){
            this.remedyForId = remedyFor.id;
            this.remedyFor = remedyFor.ailment;
        },
        cancelEdit(){
            this.remedyForId = null;
        },
        async editRemedyFor(data){
            if (data.ailment != this.remedyFors.find(x => x.id == this.remedyForId).ailment)
            {
                if (!this.remedyFors.map(x => x.ailment).includes(data.ailment)){
                    await this.updateRemedyFor(this.remedyForId, data);
                    alert("Sikeres módosítás.");
                    this.$emit('redirect');  
                }
                else alert("Már létezik ez a betegség.");
            }
            else alert("A betegséget nem változtatta.");
        },
        async createRemedyFor(data){
            if (!this.remedyFors.map(x => x.ailment).includes(data.ailment)){
                await this.postRemedyFor(data);
                alert("Sikeres létrehozás.");
                this.$emit('redirect'); 
            }
            else alert("Már létezik ez a betegség.")
        },
        async deleteRemedyFor(){
            if(confirm("Biztosan törölni akarja a betegséget?"))
            {
                await this.destroyRemedyFor(this.remedyForId);
                alert("Sikeres törlés.");
                this.$emit('redirect');
            }
        },
    },
    emits: ['redirect'],
    async mounted(){
        await this.getRemedyFors();
        this.filteredRemedyFors = this.remedyFors.sort((a, b) => a.ailment.localeCompare(b.ailment));
        this.loading = false;
    }
}
</script>