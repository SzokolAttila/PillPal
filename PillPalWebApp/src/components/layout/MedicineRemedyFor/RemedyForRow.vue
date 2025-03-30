<template>
<div class="grid grid-cols-3 w-[70%] mx-auto items-center">
    <div class="col-span-2">
        <FormKit type="select" :options="remedyForOptions" v-model="selectedId"/>
    </div>
    <div>
        <FormKit type="button" label="Töröl" @click="deleteRemedyFor"/>
    </div>
</div>
</template>

<script>
export default{
    props: ['remedyForOptions', 'remedyFor'],
    data(){
        return {
            selectedId: 1
        }
    },
    methods:{
        deleteRemedyFor(){
            this.$emit('deleteRemedyFor', this.remedyFor.id)
        }
    },
    mounted(){
        this.selectedId = this.remedyFor.remedyForId;
    },
    watch:{
        selectedId(){
            this.remedyFor.remedyForId = this.selectedId;
            this.$emit('updateRemedyFor', this.remedyFor)
        }
    }
}
</script>