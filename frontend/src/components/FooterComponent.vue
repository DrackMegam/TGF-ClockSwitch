<template>
  <div class="mainFooter">
    <footer class="">
      <div class="footerText">
        <span>vue@3.2.41</span>
        <span>Bootstrap@4.6.2</span>
        <span>2022 - David Véliz Fernández - TFG DAW</span>
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
  position: fixed;
  bottom: 0;
  left: 300px;
  width: 100%;
  height: 50px;
}

.footerText {
  margin-top: 15px;
  margin-right: 25px;
  text-align: center;
  letter-spacing: 3px;
  display:flex;
  flex-direction: row;
  justify-content: space-evenly;
  font-size: 13px;
}
</style>
