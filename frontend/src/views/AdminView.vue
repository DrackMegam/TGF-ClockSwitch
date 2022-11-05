<script setup>
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
import AdminComponent from '../components/AdminComponent.vue'

</script>

<template>
  <main>
    <SidebarComponent />
    <AdminComponent />
    <FooterComponent />
  </main>
</template>

<style scoped>
main{
  background-color:gray;
}
</style>