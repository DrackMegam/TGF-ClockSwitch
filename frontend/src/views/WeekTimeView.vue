<script setup>
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
import WeekTimeComponent from '../components/WeekTimeComponent.vue'

</script>

<template>
  <main>
    <SidebarComponent />
    <WeekTimeComponent /> 
    <FooterComponent />
  </main>
</template>

<style scoped>
main{
  background-color:gray;
}
</style>