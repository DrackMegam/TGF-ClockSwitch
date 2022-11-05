<script setup>
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
import EveryoneComponent from '../components/EveryoneComponent.vue'

</script>

<template>
  <main>
    <SidebarComponent />
<EveryoneComponent />
    <FooterComponent />
  </main>
</template>

<style scoped>
main{
  background-color:gray;
}
</style>