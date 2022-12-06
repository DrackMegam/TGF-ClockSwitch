<template>
  <main>
    <SidebarComponent :username=this.$route.params.username />
    <FooterComponent />
  </main>
</template>

<script lang="js">
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
import { defineComponent } from 'vue';
import jQuery from 'jquery';
let $ = jQuery;

export default defineComponent({
  props:{
    mehamius:{type: String, required: true}
  },
  components:{
    SidebarComponent,
    FooterComponent
  },
  data() {
    return {
      dataReceived: [],
      dataBoolean: false,
      dniDelete: "",
    };
  },
  created() {

  },
  watch: {
  },
  methods: {

  },
  beforeMount() {
    console.log(this.$route.params);
  }
});
</script>

<style scoped>
/* Imports */
@import url("../lib/bootstrap.css");
@import url("../lib/bootstrap.min.css");
@import url("../lib/bootstrap-grid.css");
@import url("../lib/bootstrap-grid.min.css");
@import url("../lib/bootstrap-reboot.css");
@import url("../lib/bootstrap-reboot.min.css");
@import url("../lib/startbootstrap-sb-admin/vendor/font-awesome/css/font-awesome.min.css");
@import url("../lib/startbootstrap-sb-admin/vendor/datatables/dataTables.bootstrap4.min.css");
@import url("../lib/toastr.js/toastr.min.css");

</style>
