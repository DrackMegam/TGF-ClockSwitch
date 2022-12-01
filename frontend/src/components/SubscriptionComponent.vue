<template>
  <div class="main container">
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

    <div class="container" name="subscriptionsData">
      <table id="datatable" class="table table-striped tableUpdate uniqueTable datatable"></table>
    </div>

  </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';
import jQuery from 'jquery';
let $ = jQuery;

export default defineComponent({
  data() {
    return {
      dataReceived: [],
      currentSubs: [],
      availableSubs: [],
      uniqueDataReceived: {},
      userId: 10, // mehamius = 10 | felix.roncero = 3
    };
  },
  created() {

  },
  watch: {

  },
  methods: {
    getSubscriptions: function () {
      let url = "https://localhost:44368/Subscription/GetAvailableSubscriptions/" + this.userId;
      this.recuperarAvailableSubs(url).then(() => { });
      url = "https://localhost:44368/Subscription/GetCurrentSubscriptions/" + this.userId;
      this.recuperarCurrentSubs(url).then(() => { this.loadSubsData(); });
    },
    loadSubsData: function () {
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 1; // Para ajustarlo al horario ibérico
      let url = "https://localhost:44368/Subscription/GetSubsData/" + this.userId + "/" + thisYear + "/" + thisWeek;

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
        this.$router.go(this.$router.currentRoute);
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
        this.$router.go(this.$router.currentRoute);
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
  },
  beforeMount() {
    this.getSubscriptions();
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
  display: flex;
  flex-direction: column;
  height: 900px;
  width: 1500px;
  margin-left: 160px;
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
  background-color: rgb(20, 43, 44);
  font-weight: bolder;
  font-size: 18px;
  color: black;
  width: 75%;
}

.uniqueTable {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
  height: 100%;
  margin-top:50px;
}
</style>
