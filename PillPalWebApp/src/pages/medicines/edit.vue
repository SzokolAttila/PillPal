<template>
<BaseLayout>
    <h1 class="text-center text-4xl my-5 text-textColor-light dark:text-textColor-dark">Gyógyszer szerkesztése</h1>
    <hr class="bg-component-light dark:bg-component-dark p-1 my-5 w-[70%] mx-auto">
    <div v-if="loaded" class="grid grid-cols-1 md:grid-cols-2">
        <div class="w-[80%] mx-auto">
            <FormKit type="text" placeholder="Keresett gyógyszer..." v-model="searchText"/>
            <div class="h-10 overflow-y-auto mb-3">
                <div v-for="med of showedMedicines" @click="selectMed(med)"
                 class="text-textColor-light dark:text-textColor-dark cursor-pointer
                  hover:bg-component-light dark:hover:bg-component-dark p-2 rounded-lg">
                    {{ med.name }}
                </div>
            </div>
            <FormKit type="form" :actions="false" @submit="editMedicine">
                <FormKit type="text" label="Gyógyszer neve" v-model="medicine.name" :validation-messages="{required: 'A gyógyszer nevének megadása kötelező.', length: 'A gyógyszer nevének hossza 5 és 30 karakter között kell legyen.'}" validation="required|length:5,30"/>
                <FormKit type="text" label="Gyártója" v-model="medicine.manufacturer" :validation-messages="{required: 'A gyógyszer gyártójának megadása kötelező.', length: 'A gyógyszer gyártójának hossza 5 és 30 karakter között kell legyen.'}" validation="required|length:5,30"/>
                <FormKit type="textarea" label="Leírása" v-model="medicine.description" :validation-messages="{required: 'A gyógyszer leírásának megadása kötelező.', length: 'A gyógyszer leírásának hossza 5 és 255 karakter között kell legyen.'}" validation="required|length:5,255"
                :help="`${medicine.description.length} / 255`"/>
                <FormKit type="select" label="Kiszerelés egysége" v-model="medicine.packageUnitId" :options="packageUnitOptions"/>
                <div class="grid grid-cols-2">
                    <FormKit type="button" label="Töröl" @click="removeMedicine"/>
                    <FormKit type="submit" label="Módosít"/>
                </div>
            </FormKit>
        </div>
        <div class="grid grid-cols-1 lg:grid-cols-2 items-center">
            <SideEffects :medicine="medicine"/>
            <ActiveIngredients :medicine="medicine"/>
            <RemedyFors :medicine="medicine"/>
            <PackageSizes :medicine="medicine"/>
        </div>
    </div>
    <BaseSpinner class="mx-auto mt-10" v-else/>
</BaseLayout>
</template>

<script>
import BaseLayout from '@layouts/BaseLayout.vue'
import SideEffects from '@components/layout/MedicineSideEffect/SideEffects.vue'
import ActiveIngredients from '@components/layout/MedicineActiveIngredient/ActiveIngredients.vue'
import RemedyFors from '@components/layout/MedicineRemedyFor/RemedyFors.vue'
import PackageSizes from '@components/layout/MedicinePackageSize/PackageSizes.vue'
import BaseSpinner from '@components/layout/BaseSpinner.vue'
import {useMedicineStore} from '@stores/MedicineStore'
import {usePackageUnitStore} from '@stores/PackageUnitStore'
import {mapActions, mapState} from 'pinia'

export default{
    components: {
        BaseLayout,
        BaseSpinner,
        SideEffects,
        ActiveIngredients,
        RemedyFors,
        PackageSizes
    },
    data(){
        return {
            medicine: { id: 1, description: ""},
            showedMedicines: [],
            searchText: "",
            loaded: false
        }
    },
    methods: {
        ...mapActions(useMedicineStore, ["getMedicines", "updateMedicine", "deleteMedicine"]),
        ...mapActions(usePackageUnitStore, ["getPackageUnits"]),  
        selectMed(medicine){
            this.medicine = { ...medicine };
        },
        async editMedicine(){
            let success = await this.updateMedicine(this.medicine.id, this.medicine);
            if(success){
                alert("Sikeresen módosult a gyógyszer");
            }else{
                alert("Hiba történt a gyógyszer módosítása során");
            }
        },
        async removeMedicine(){
            if(confirm("Tényleg szeretné törölni a gyógyszert?")){
                let success = await this.deleteMedicine(this.medicine.id);
                if(success){
                    alert("Sikeresen törlődött a gyógyszer");
                }else{
                    alert("Hiba történt a gyógyszer törlése során");
                }
            }
        }
    },
    async mounted(){
        await this.getMedicines();
        await this.getPackageUnits();
        this.medicine = { ...this.medicines[0] };
        this.showedMedicines = this.medicines;
        this.loaded = true;
    },
    computed: {
        ...mapState(useMedicineStore, ["medicines"]),
        ...mapState(usePackageUnitStore, ["packageUnitOptions"])
    },
    watch: {
        searchText(){
            this.showedMedicines = this.medicines
            .filter(medicine => medicine.name.toLowerCase().includes(this.searchText.toLowerCase()));
        },
        async medicines(){
            this.loaded = false;
            this.showedMedicines = this.medicines;
            this.medicine = { ...this.medicines[0] };
            this.loaded = true;
        }
    }
}
</script>

<route lang="json">
{
    "name": "edit-medicine"
}
</route>