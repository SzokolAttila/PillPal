<template>
  <div class="flex flex-row flex-nowrap justify-between">
    <BaseHeader />
    <main class="container mx-auto">
      <slot />
    </main>
  </div>
</template>
<script>
import BaseHeader from '@components/layout/BaseHeader.vue'
export default {
  components: {
    BaseHeader
  }
}
</script>
