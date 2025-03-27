<template>
<BaseLayout>
    <SideEffects :medicine="medicine"/>
</BaseLayout>
</template>

<script>
import BaseLayout from '@layouts/BaseLayout.vue'
import SideEffects from '@components/layout/MedicineSideEffect/SideEffects.vue'
import {useMedicineStore} from '@stores/MedicineStore'
import {mapActions, mapState} from 'pinia'

export default{
    components: {
        BaseLayout,
        SideEffects
    },
    data(){
        return {
            medicine: { id: 1},
            showedMedicines: [],
            searchText: ""
        }
    },
    methods: {
        ...mapActions(useMedicineStore, ["getMedicines"])
    },
    async mounted(){
        await this.getMedicines();
    },
    computed: {
        ...mapState(useMedicineStore, ["medicines"])
    }
}
</script>

<route lang="json">
{
    "name": "edit-medicine"
}
</route>