<template>
  <div class="mainFooter">
      <footer class="">
        <div class="footerText">
          <small> 2022 - David Véliz Fernández - TFG DAW</small>
        </div>
      </footer>
  </div>
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

    },
});
</script>

<style scoped>
.mainFooter {
  background-color: rgb(10, 33, 34);
  position:fixed;
  bottom:0;
  left:300px;
  width:100%;
  height:50px;
}
.footerText{
  margin-top:10px;
  text-align:center;
  letter-spacing:3px;
}
</style>
