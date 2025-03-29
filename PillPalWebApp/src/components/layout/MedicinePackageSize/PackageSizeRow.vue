<template>
<div class="grid grid-cols-3 w-[70%] mx-auto items-center">
    <div class="col-span-2 w-[70%]">
        <FormKit type="number" v-model="packageSizeValue" min="1" @blur="packageSizeChanged"/>
    </div>
    <div>
        <FormKit type="button" label="Töröl" @click="deletePackageSize"/>
    </div>
</div>
</template>

<script>
export default{
    props: ['packageSize'],
    data(){
        return {
            packageSizeValue: null
        }
    },
    methods:{
        deletePackageSize(){
            this.$emit('deletePackageSize', this.packageSize.id)
        },
        packageSizeChanged(){
            this.packageSize.size = this.packageSizeValue;
            this.$emit('updatePackageSize', this.packageSize);
        }
    },
    mounted(){
        this.packageSizeValue = this.packageSize.size;
    }
    // watch:{
    //     packageSizeValue(newValue, oldValue){
    //         if(oldValue !== null){
    //             this.packageSize.size = this.packageSizeValue;
    //             this.$emit('updatePackageSize', this.packageSize);
    //         }
    //     }
    // }
}
</script>