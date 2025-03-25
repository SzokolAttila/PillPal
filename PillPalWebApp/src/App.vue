<script>
import { useUserStore } from '@stores/UserStore.mjs';
import {useMedicineStore} from '@stores/MedicineStore';
import {usePackageUnitStore} from '@stores/PackageUnitStore';
import { mapActions } from 'pinia';

export default{
  methods:{
    ...mapActions(useUserStore, ["getUsers"]),
    ...mapActions(useMedicineStore, ["getMedicines"]),
    ...mapActions(usePackageUnitStore, ["getPackageUnits"])
  },
  async mounted(){
    await this.getUsers();
    await this.getMedicines();
    await this.getPackageUnits();
  }
}
</script>

<template>
  <RouterView />
</template>
