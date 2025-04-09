<template>
<div class="grid grid-cols-3 w-[70%] mx-auto items-center">
    <div class="col-span-2">
        <FormKit type="select" :options="sideEffectOptions" v-model="selectedId"/>
    </div>
    <div>
        <FormKit type="button" name="removeEffectBtn" label="Töröl" @click="deleteSideEffect"/>
    </div>
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