<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="mainSubs container">
    <div class="container justify-content-end manageSubscriptions" name="manageSubscriptions">

      <div class="container list-group availableSubscriptions" name="availableSubscriptions">
        <p class="h2 mb-3">Suscripciones disponibles</p>
        <template v-for="data in availableSubs">
          <div class="row mb-2 pl-3 pb-2 listItemPersonalizado">
            <div class="col">
              <input class="form-check-input checkUser subCheck" type="checkbox" name="subCheck" :value=data.idUsuario>
              <div class="form-outline ml-2">
                <label class="form-label" for="">Nombre Usuario</label>
                <input type="text" :value=data.username readonly name="nombreUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-3">
              <div class="form-outline">
                <label class="form-label" for="">Identificador</label>
                <input type="number" :value=data.idUsuario readonly name="idUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
          </div>
        </template>
        <button v-if="(availableSubs.length != 0)" type="button" class="btn btn-lg btnSubmitPersonalizado mt-3 ml-5"
          v-on:click="suscribe()">Suscribirse</button>
        <p v-if="(availableSubs.length == 0)" class="h4 mb-3">¡Estás suscrito a todos!</p>

      </div>

      <div class="container list-group currentSubscriptions" name="currentSubscriptions">
        <p class="h2 mb-3">Actualmente suscrito</p>
        <template v-for="data in currentSubs">
          <div class="row mb-2 pl-3 pb-2 listItemPersonalizado">
            <div class="col">
              <input class="form-check-input checkUser" type="checkbox" name="unsubCheck" :value=data.idUsuario>
              <div class="form-outline ml-3">
                <label class="form-label" for="">Nombre Usuario</label>
                <input type="text" :value=data.username readonly name="nombreUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-3">
              <div class="form-outline">
                <label class="form-label" for="">Identificador</label>
                <input type="number" :value=data.idUsuario readonly name="idUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
          </div>
        </template>
        <button v-if="(currentSubs.length != 0)" type="button" class="btn btn-lg btnSubmitPersonalizado mt-3 ml-5"
          v-on:click="unsuscribe()">Eliminar
          suscripción</button>
        <p v-if="(currentSubs.length == 0)" class="h4 mb-3">¡No estás suscrito a nadie!</p>
      </div>

    </div>

    <div class="container subscriptionsData" name="subscriptionsData" v-if="(currentSubs.length > 0)">

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

      <table id="datatable" class="table table-striped tableUpdate uniqueTable datatable" name="datatable"></table>
    </div>

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
      currentSubs: [],
      availableSubs: [],
      uniqueDataReceived: {},
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
        return fetch("https://localhost:44368/Login/GimmeId/" + username)
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
    getTimeData: async function () {
      try {
        return fetch("https://localhost:44368/Semana/GetTimeData")
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
        this.refreshPage();
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
      this.refreshPage();
      this.getDateOfWeek(this.actualWeekOfYear, this.actualYear);
    },
    getSubscriptions: function () {
      let url = "https://localhost:44368/Subscription/GetAvailableSubscriptions/" + this.userId;
      this.recuperarAvailableSubs(url).then(() => { });
      url = "https://localhost:44368/Subscription/GetCurrentSubscriptions/" + this.userId;
      this.recuperarCurrentSubs(url).then(() => { this.loadSubsData(); });
    },
    loadSubsData: function () {
      let url = "https://localhost:44368/Subscription/GetSubsData/" + this.userId + "/" + this.actualYear + "/" + this.actualWeekOfYear;
      let html = "";
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:26px;color:rgb(215, 89, 0);'";
        let styleHeaderRow = "style='text-align: left;padding-left:7px;padding-right:7px;'";
        let styleHeaderRowNumber = "style='text-align: right;padding-left:7px;padding-right:7px;font-size:24px;'";
        html = "<thead>" +
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
          console.log(e);
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
      });



    },
    suscribe: function () {
      let idChecked = [];
      var checkboxes = document.querySelectorAll('input[type=checkbox][name=subCheck]:checked')

      for (var i = 0; i < checkboxes.length; i++) {
        idChecked.push(checkboxes[i].value)
      }

      this.suscribeDone(idChecked).then(() => {
        setTimeout(() => this.refreshPage(), 500);
      });
    },
    suscribeDone: async function (ids) {
      ids.forEach((id) => {
        let url = "https://localhost:44368/Subscription/Suscribe/" + this.userId + "/" + id;
        this.recuperarDatoUnicoBack(url).then(() => { });
      });
    },
    unsuscribe: function () {
      let idChecked = [];
      var checkboxes = document.querySelectorAll('input[type=checkbox][name=unsubCheck]:checked')

      for (var i = 0; i < checkboxes.length; i++) {
        idChecked.push(checkboxes[i].value)
      }

      this.unsuscribeDone(idChecked).then(() => {
        setTimeout(() => this.refreshPage(), 500);
      });
    },
    unsuscribeDone: async function (ids) {
      ids.forEach((id) => {
        let url = "https://localhost:44368/Subscription/Unsuscribe/" + this.userId + "/" + id;
        this.recuperarDatoUnicoBack(url).then(() => { });
      });
    },
    recuperarCurrentSubs: async function (url) {
      try {
        await fetch(url)
          .then((response) => response.json())
          .then((data) => {
            data.forEach((element) => {
              this.currentSubs.push(element);
            });
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
    recuperarAvailableSubs: async function (url) {
      try {
        await fetch(url)
          .then((response) => response.json())
          .then((data) => {
            data.forEach((element) => {
              this.availableSubs.push(element);
            });
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
    recuperarDatosBack: async function (url) {
      try {
        await fetch(url)
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
    recuperarDatoUnicoBack: async function (url) {
      try {
        return fetch(url)
          .then((response) => response.json())
          .then((data) => {
            this.uniqueDataReceived = data;
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
    refreshPage: async function () {
      this.dataReceived.length = 0;
      this.currentSubs.length = 0;
      this.availableSubs.length = 0;
      this.uniqueDataReceived = {};
      //$("table{name='datatable'}").empty();
      this.getSubscriptions();
    },
  },
  beforeMount() {
    this.getUserId(this.$route.params.username).then(() => {
      this.getTimeData().then(() => {
        this.getSubscriptions();
      })
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

.mainSubs {
  display: flex;
  flex-direction: column;
  height: 900px;
  width: 1500px;
  margin-left: 160px;
  z-index: 0;
}

.subscriptionsData {
  width: 100%;
  height: 100%;
  margin-top: 20px;
  padding-bottom: 300px;
}

.checkUser {
  margin-top: 40px;
  padding-right: 20px;
}

.listItemPersonalizado {
  background-color: rgb(20, 43, 44);
  border-radius: 20px;
  border: 2px solid rgb(180, 54, 0);
}

.availableSubscriptions {
  margin-right: 20px;
}

.checkUser {
  width: 15px;
  height: 15px;
}

.manageSubscriptions {
  display: flex;
  flex-direction: row;
  justify-content: end;
}

.btnSubmitPersonalizado {
  font-weight: bolder;
  font-size: 18px;
  width: 75%;
  color: white;
  background-color: rgb(215, 89, 0);
}

.uniqueTable {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
  height: 100%;

  padding-bottom: 100px;
}

.btnSuperiores {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
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
</style>
