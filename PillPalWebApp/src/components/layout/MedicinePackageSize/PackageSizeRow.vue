<template>
<div class="grid grid-cols-3 w-[70%] mx-auto items-center">
    <div class="col-span-2">
        <FormKit type="number" name="size" v-model="packageSizeValue" number="integer" step="1" min="1" @blur="packageSizeChanged"/>
    </div>
    <div>
        <FormKit type="button" name="removeSizeBtn" label="Töröl" @click="deletePackageSize"/>
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
            if(this.packageSizeValue < 1){
                this.packageSizeValue = this.packageSize.size;
            }else{
                let current = {...this.packageSize}
                current.size = this.packageSizeValue;

                this.$emit('updatePackageSize', current);
                
                // Refresh the binded value if updatePackageSize changed it
                // Due to duplicated values
                this.packageSizeValue = this.packageSize.size;
            }
        }
    },
    mounted(){
        this.packageSizeValue = this.packageSize.size;
    }
}
</script>