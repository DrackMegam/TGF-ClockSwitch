<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="container main mt-4">

    <div class="container btnSuperiores mb-4" name="botonesSuperiores">
      <div name="btnSemanaAtras" class="flecha" v-on:click="timeBackward()">
        &#60;&#60;
      </div>
      <div name="weekInfo" class="weekInfo mt-3">
        {{ dateTimeString }}
      </div>
      <div name="btnSemanaAdelante" class="flecha" v-on:click="timeFoward()">
        &#62;&#62;
      </div>
    </div>

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
      userId: 0,
      startingWeekOfYear: 0,
      actualYear: 0,
      actualWeekOfYear: 0,
      dateTimeString: "Semana actual"
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
    getUserId: async function (username) {
      try {
        return fetch("https://83.33.245.0:8081/Login/GimmeId/" + username)
          .then((response) => response.json())
          .then((data) => {
            this.userId = data;
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
    getDateOfWeek: function (w, y) {
      // https://stackoverflow.com/questions/16590500/calculate-date-from-week-number-in-javascript
      var d = (1 + w * 7); // 1st of January + 7 days for each week
      let date = new Date(y, 0, d);

      // https://stackoverflow.com/questions/4156434/javascript-get-the-first-day-of-the-week-from-current-date
      let day = date.getDay();
      let diff = date.getDate() - day + (day === 0 ? -6 : 1);
      let epochFirstDay = date.setDate(diff);

      let months = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
      let month = (months[date.getMonth()]);
      let dayOfMonth = date.getDate();
      this.dateTimeString = "Semana del " + dayOfMonth + " de " + month;
    },
    timeFoward: function () {
      if (this.actualWeekOfYear != this.startingWeekOfYear) {
        console.log("Yendo hacia delante...");
        if (this.actualWeekOfYear == 52) {
          this.actualWeekOfYear == 1; // Cambio de año.
          this.actualYear++;
        } else {
          this.actualWeekOfYear++;
        }
        this.getAllHours(this.actualWeekOfYear, this.actualYear);
        this.getDateOfWeek(this.actualWeekOfYear, this.actualYear);
      }
    },
    timeBackward: function () {
      console.log("Yendo hacia atrás...");
      console.log("Yendo hacia delante...");
      if (this.actualWeekOfYear == 1) {
        this.actualWeekOfYear == 52; // Cambio de año.
        this.actualYear--;
      } else {
        this.actualWeekOfYear--;
      }
      this.getAllHours(this.actualWeekOfYear, this.actualYear);
      this.getDateOfWeek(this.actualWeekOfYear, this.actualYear);
    },
    getTimeData: async function () {
      try {
        return fetch("https://83.33.245.0:8081/Semana/GetTimeData")
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            this.actualYear = data[0];
            this.startingWeekOfYear = data[1];
            this.actualWeekOfYear = data[1];
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
    getAllHours: function () {
      let url = "https://83.33.245.0:8081/Everyone/AllHours/" + this.actualYear + "/" + this.actualWeekOfYear;
      console.log(url);
      this.dataReceived.length=0;
      this.recuperarDatosBack(url).then(() => {
        this.showData();
      });
    },
    showData: function () {
      $("#datatable").empty();
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
        "<th " + styleHeaderRow + " scope='col'>Total</th>" +
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
    // Primero asigno el ID, después que ejecute el resto de consultas.
    this.getUserId(this.$route.params.username).then(() => {
      this.getTimeData().then(() => {
        this.getAllHours();
      });
    });
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

.flecha {
  font-weight: bold;
  font-size: 45px;
  letter-spacing: -10px;
  color: rgb(180, 54, 0);
}

.weekInfo {
  font-weight: bold;
  font-size: 25px;
  letter-spacing: 2px;
  color: rgb(215, 89, 0);
}

.btnSuperiores {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
</style>
