<template>
  <div class="h-screen flex flex-row flex-nowrap justify-start">
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
