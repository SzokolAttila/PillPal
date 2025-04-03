<template>
    <BaseHeader />
    <main class="container mx-auto sm:pl-80 mt-20 sm:mt-0">
      <slot />
    </main>
</template>
<script>
import BaseHeader from '@components/layout/BaseHeader.vue'
export default {
  components: {
    BaseHeader
  }
}
</script>
