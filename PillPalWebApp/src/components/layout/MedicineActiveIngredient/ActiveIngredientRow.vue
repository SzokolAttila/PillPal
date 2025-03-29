<template>
<div class="grid grid-cols-3 w-[70%] mx-auto items-center">
    <div class="col-span-2 w-[70%]">
        <FormKit type="select" :options="activeIngredientOptions" v-model="selectedId"/>
    </div>
    <div>
        <FormKit type="button" label="Töröl" @click="deleteActiveIngredient"/>
    </div>
</div>
</template>

<script>
export default{
    props: ['activeIngredientOptions', 'activeIngredient'],
    data(){
        return {
            selectedId: 1
        }
    },
    methods:{
        deleteActiveIngredient(){
            this.$emit('deleteActiveIngredient', this.activeIngredient.id)
        }
    },
    mounted(){
        this.selectedId = this.activeIngredient.activeIngredientId;
    },
    watch:{
        selectedId(){
            this.activeIngredient.activeIngredientId = this.selectedId;
            this.$emit('updateActiveIngredient', this.activeIngredient)
        }
    }
}
</script>