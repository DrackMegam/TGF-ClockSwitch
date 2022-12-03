<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="container main mt-4">
    <table id="datatable" class="table table-striped tableUpdate uniqueTable datatable"></table>
  </div>
  <FooterComponent />
</template>

<script lang="js">
import { defineComponent } from 'vue';
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
import jQuery from 'jquery';
let $ = jQuery;


export default defineComponent({
  data() {
    return {
      dataReceived: [],
      userId: 10, // mehamius = 10 | felix.roncero = 3
    };
  },
  components: {
    SidebarComponent,
    FooterComponent
  },
  created() {

  },
  watch: {

  },
  methods: {
    getAllHours: function () {
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 1; // Para ajustarlo al horario ibérico
      let url = "https://localhost:44368/Everyone/AllHours/" + thisYear + "/" + thisWeek;
      console.log(url);
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        this.showData();
      });
    },
    showData: function () {
      console.log(this.dataReceived);
      console.log("Pintando la tabla...");
      // Cabezera de la tabla.
      let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:26px;color:rgb(215, 89, 0);'";
      let styleHeaderRow = "style='text-align: left;padding-left:7px;padding-right:7px;'";
      let styleHeaderRowNumber = "style='text-align: right;padding-left:7px;padding-right:7px;font-size:24px;'";
      let html = "<thead>" +
        "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>ID</th>" +
        "<th " + styleHeaderRow + " scope='col'>Usuario</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Lunes</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Martes</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Miercoles</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Jueves</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Viernes</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Sabado</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Domingo</th>" +
        "<th " + styleHeaderRowNumber + " scope='col'>Total</th>" +
        "<th></th>" +
        "</tr></thead>";

      // Estilos de las filas y mostrar los datos recuperados.
      let styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
      let evenRow = true;
      this.dataReceived.forEach(e => {
        if (evenRow) {
          styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(20, 43, 44);font-size:24px;'";
          evenRow = false;
        } else {
          styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(27, 58, 59);font-size:24px;'";
          evenRow = true;
        }
        let styleText = "style='text-align: left;font-size:20px;padding-left:15px;padding-right:15px;'";
        html += "<tbody>" +
          "<tr " + styleRow + "><th scope='row'>" + e.idUsuario + "</th>" +
          "<td " + styleText + ">" + e.nombreUsuario + "</td>" +
          "<td>" + e.sumatorioLunes + "</td>" +
          "<td>" + e.sumatorioMartes + "</td>" +
          "<td>" + e.sumatorioMiercoles + "</td>" +
          "<td>" + e.sumatorioJueves + "</td>" +
          "<td>" + e.sumatorioViernes + "</td>" +
          "<td>" + e.sumatorioSabado + "</td>" +
          "<td>" + e.sumatorioDomingo + "</td>" +
          "<td style='color:rgb(215, 89, 0);font-weight:bold;'>" + e.total + "</td>" +
          "<td></td>" +
          "</tr>";
      });

      $("#datatable").append(html);
    },
    recuperarDatosBack: async function (url) {
      try {
        return fetch(url)
          .then((response) => response.json())
          .then((data) => {
            data.forEach((element) => {
              this.dataReceived.push(element);
            });
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
  },
  beforeMount() {
    this.getAllHours();
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

.main {
  position: fixed;
  top: 80px;
  left: 500px;
  width: 100%;
  margin: auto;
}

.uniqueTable {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
  height: 100%;

}
</style>
