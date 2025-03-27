<template>
<div class="grid grid-cols-3">
    <FormKit type="select" class="col-span-2" :options="sideEffectOptions" v-model="selectedId"/>
    <FormKit type="button" label="Töröl" @click="deleteSideEffect"/>
</div>
</template>

<script>
export default{
    props: ['sideEffectOptions', 'sideEffect'],
    data(){
        return {
            selectedId: 1
        }
    },
    methods:{
        deleteSideEffect(){
            this.$emit('deleteSideEffect', this.sideEffect.id)
        }
    },
    mounted(){
        this.selectedId = this.sideEffect.sideEffectId;
    },
    watch:{
        selectedId(){
            this.sideEffect.sideEffectId = this.selectedId;
            this.$emit('updateSideEffect', this.sideEffect)
        }
    }
}
</script>