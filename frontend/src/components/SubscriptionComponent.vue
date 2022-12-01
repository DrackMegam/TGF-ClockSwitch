<template>
  <div class="main container">
    <div class="container justify-content-end manageSubscriptions" name="manageSubscriptions">

      <div class="container list-group availableSubscriptions" name="availableSubscriptions">
        <p class="h2 mb-3">Suscripciones disponibles</p>
        <template v-for="data in availableSubs">
          <div class="row mb-2 pl-3 pb-2 listItemPersonalizado">
            <div class="col">
              <input class="form-check-input checkUser subCheck" type="checkbox" name="subCheck" :value=data.idUsuario>
              <div class="form-outline">
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
              <div class="form-outline">
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

    <div class="container" name="subscriptionsData">data</div>
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
      this.recuperarCurrentSubs(url).then(() => { });
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

.alredySubscriptions {
  margin-left: 100px;
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
</style>
