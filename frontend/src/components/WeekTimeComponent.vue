<template>
  <div class="">weektime</div>
  <button v-on:click="meha()">Funcionar</button>
</template>

<script lang="js">
import { defineComponent } from 'vue';



export default defineComponent({
    data() {
        return {

        };
    },
    created() {

    },
    watch: {

    },
    methods: {
      async meha() {
          console.log("Utilidad: Recuperar meha");
          try{
            fetch("https://localhost:44368/Meha")
            .then((response) => response.json())
            .then((data) => {
              data.forEach((element) => {
                console.log(element);
              });
            })
            .catch(error => {
              console.error(error);
            });
          }
          catch(e){console.log(e)}
          console.log("Utilidad finalizada.");
        },
    },
});
/*
async meha() {
          console.log("Utilidad: Recuperar meha");
          try{
            fetch("http://localhost:9635/Subscription/vue")
            .then(async response => {
              const data = await response;
              if (!response.ok) {
                const error = (data && data.message) || response.statusText;
                return Promise.reject(error);
              }
              console.log(data);
            })
            .catch(error => {
              console.error(error);
            });
          }
          catch(e){console.log(e)}
          console.log("Utilidad finalizada.");
        },
        /*fetch("https://localhost:44329/meha/damedatos")
            .then((response) => response.json())
            .then((data) => {
              data.forEach((element) => {
                console.log(element);
              });
            })
            .catch(e => console.error(e));*/
            
</script>

<style scoped></style>
