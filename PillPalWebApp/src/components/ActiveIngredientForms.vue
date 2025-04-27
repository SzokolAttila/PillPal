<template>
    <BaseSpinner v-if="loading" class="mt-16 mx-auto" />
    <div v-else>
        <FormKit type="text" id="searchActiveIngredient" placeholder="Keresett hatóanyag..." v-model="searchActiveIngredient" />
        <div class="h-16 overflow-y-auto text-textColor-light dark:text-textColor-dark">
            <div class="activeIngredient rounded-md p-2 hover:bg-component-light dark:hover:bg-component-dark" v-for="ingredient in filteredActiveIngredients" @click="selectActiveIngredient(ingredient)">{{ ingredient.ingredient }}</div>
        </div>
        <div id="activeIngredientForm" class="border-2 border-component-light dark:border-component-dark p-2 rounded-md my-4">
            <FormKit type="form" v-if="activeIngredientId != null" :actions="false" @submit="editActiveIngredient">
                <FormKit id="existingActiveIngredient" validation="required|length:3,80" :validation-messages="{required: 'A hatóanyag megadása kötelező.', length: 'A hatóanyag hossza 3 és 80 karakter között kell legyen.'}" type="text" name="ingredient" label="Hatóanyag szerkesztése" v-model="activeIngredient" />
                <div class="flex flex-row flex-nowrap justify-between">
                    <FormKit id="editActiveIngredient" type="submit" label="Módosít" />
                    <button id="deleteActiveIngredient" type="button" @click="deleteActiveIngredient" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Töröl
                    </button>
                    <button type="button" @click="cancelEdit" class="p-2 rounded-md text-textColor-light dark:text-textColor-dark bg-component-light my-2 h-fit dark:bg-component-dark">
                        Mégse
                    </button>
                </div>
            </FormKit>
            <FormKit v-else type="form" :actions="false" @submit="createActiveIngredient">
                <FormKit id="newActiveIngredient" :validation-messages="{required: 'A hatóanyag megadása kötelező.', length: 'A hatóanyag hossza 3 és 80 karakter között kell legyen.'}" label="Új hatóanyag" type="text" name="ingredient" validation="required|length:3,80" placeholder="Hatóanyag"  />
                <FormKit id="submitNewActiveIngredient" type="submit" label="Hozzáad"/>
            </FormKit>
        </div>
    </div>
</template>

<script>
import BaseSpinner from '@components/layout/BaseSpinner.vue';
import { mapActions, mapState } from 'pinia';
import { useActiveIngredientStore } from '@stores/ActiveIngredientStore.mjs';
export default {
    data(){
        return {
            filteredActiveIngredients: [],
            activeIngredientId: null,
            activeIngredient: "",
            searchActiveIngredient: "",
            loading: true
        }
    },
    components: {
        BaseSpinner
    },
    watch: {
        async searchActiveIngredient(){
            this.filteredActiveIngredients = this.activeIngredients
            .filter(x => x.ingredient.toLowerCase()
                .includes(this.searchActiveIngredient.toLocaleLowerCase()))
            .sort((a, b) => a.ingredient.localeCompare(b.ingredient));
        }
    },
    computed: {
        ...mapState(useActiveIngredientStore, ["activeIngredients"]),
    },
    methods: {
        ...mapActions(useActiveIngredientStore, ["postActiveIngredient", "getActiveIngredients", "updateActiveIngredient", "destroyActiveIngredient"]),
        selectActiveIngredient(activeIngredient){
            this.activeIngredientId = activeIngredient.id;
            this.activeIngredient = activeIngredient.ingredient;
        },
        cancelEdit(){
            this.activeIngredientId = null;
        },
        async editActiveIngredient(data){
            if (data.ingredient != this.activeIngredients.find(x => x.id == this.activeIngredientId).ingredient)
            {
                if (!this.activeIngredients.map(x => x.ingredient).includes(data.ingredient)){
                    await this.updateActiveIngredient(this.activeIngredientId, data);
                    alert("Sikeres módosítás.");
                    this.$emit('redirect');  
                }
                else alert("Már létezik ez a hatóanyag.");
            }
            else alert("A hatóanyagot nem változtatta.");
        },
        async createActiveIngredient(data){
            if (!this.activeIngredients.map(x => x.ingredient).includes(data.ingredient)){
                await this.postActiveIngredient(data);
                alert("Sikeres létrehozás.");
                this.$emit('redirect'); 
            }
            else alert("Már létezik ez a hatóanyag.")
        },
        async deleteActiveIngredient(){
            if(confirm("Biztosan törölni akarja a hatóanyagot?"))
            {
                await this.destroyActiveIngredient(this.activeIngredientId);
                alert("Sikeres törlés.");
                this.$emit('redirect');
            }
        },
    },
    emits: ['redirect'],
    async mounted(){
        await this.getActiveIngredients();
        this.filteredActiveIngredients = this.activeIngredients.sort((a, b) => a.ingredient.localeCompare(b.ingredient));
        this.loading = false;
    }
}
</script>