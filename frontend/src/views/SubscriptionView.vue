<script setup>
import FooterComponent from "../components/FooterComponent.vue";
import SidebarComponent from "../components/SidebarComponent.vue";
import SubscriptionComponent from "../components/SubscriptionComponent.vue";

</script>

<template>
  <main>
    <SidebarComponent />
    <SubscriptionComponent /> 
    <FooterComponent />
  </main>
</template>

<style scoped>
main {
  background-color: gray;
}
</style>
