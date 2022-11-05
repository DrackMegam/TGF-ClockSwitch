<template>
  <div class="mainSidebar">
    <nav class="navbar" id="mainNav">
      <a @click="volver()" class="navbar-brand"> ClockSwitch</a>



    </nav>
  </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';
//import LoginComponent from './LoginComponent.vue'



export default defineComponent({
    data() {
        return {
          user: String,
          password: String,
        };
    },
    created() {

    },
    /*
    components: {
        LoginComponent
    },*/
    watch: {

    },
    methods: {
      volver(){
            this.$router.push('/');
        },
        weekTime(){
            this.$router.push('/weekTime');
        },
        goExport(){
            this.$router.push('/export');
        },
        goEveryone(){
            this.$router.push('/everyone');
        },
        goAdmin(){
            this.$router.push('/admin');
        },
        goSubs(){
            this.$router.push('/subscription');
        }
    },
});
</script>

