<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="container main">

    <div class="container mb-4 mt-2">
      <form class="formularioPersona" id="formularioPersona">
        <div class="form-outline mb-4">
          <label class="form-label" for="">Trabajador</label>
        </div>
        <div class="row mb-4">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Nombre</label>
              <input readonly type="text" name="nombrePersona" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col-6">
            <div class="form-outline">
              <label class="form-label" for="">Apellidos</label>
              <input readonly type="text" name="apellidosPersona" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col-3">
            <div class="form-outline">
              <label class="form-label" for="">DNI</label>
              <input readonly type="text" name="dniPersona" class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <div class="row mb-4">
          <div class="col-3">
            <div class="form-outline">
              <label class="form-label" for="">Telefono</label>
              <input readonly type="number" name="telefonoPersona" id="passUserForm"
                class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Localidad</label>
              <input readonly type="text" name="localidadPersona" class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <div class="form-outline mb-4">
          <label class="form-label" for="">Descripcion</label>
          <textarea name="descripcionPersona" class="form-control bg-dark text-white" rows="3"></textarea>
        </div>
      </form>
    </div>

    <template v-for="data in dataReceived">
      <div class="container mt-1">
        <form class="formularioUsuario" id="formularioUsuario">
          <div class="row mb-4">
            <div class="col">
              <div class="form-outline">
                <label class="form-label" for="">Nombre Usuario</label>
                <input type="text" :value=data.username readonly name="nombreUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-2">
              <div class="form-outline">
                <label class="form-label" for="">Identificador</label>
                <input type="number" :value=data.idUsuario readonly name="idUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-3">
              <div class="form-outline">
                <label class="form-label" for="">Permisos</label>
                <input type="text" :value=data.isAdmin readonly name="permisosUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
          </div>
        </form>
      </div>
    </template>

    <div class="test" name="test"></div>

  </div>
  <FooterComponent />

</template>

<script lang="js">
import { defineComponent } from 'vue';
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
import jQuery from 'jquery';
let $ = jQuery;
// https://stackoverflow.com/questions/60238731/vue-router-showing-blank-page-when-built/60239175#60239175

export default defineComponent({
  data() {
    return {
      dataReceived: [],
      userId: 10, // mehamius = 10 | felix.roncero = 3
      uniqueDataReceived: {},
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
    getProfileData: function () {
      let url = "https://localhost:44368/Profile/GetOwner/" + this.userId;
      this.recuperarDatoUnicoBack(url).then(() => {
        let newUrl = "https://localhost:44368/Profile/GetPerson/" + this.uniqueDataReceived.dniPersona;
        this.loadPersonData(newUrl);
      });
    },
    loadPersonData: function (url) {
      this.recuperarDatoUnicoBack(url).then(() => {
        console.log(this.uniqueDataReceived);
        $("input[name='nombrePersona']").val(this.uniqueDataReceived.nombre);
        $("input[name='apellidosPersona']").val(this.uniqueDataReceived.apellidos);
        $("input[name='telefonoPersona']").val(this.uniqueDataReceived.telefono);
        $("input[name='dniPersona']").val(this.uniqueDataReceived.dni);
        $("input[name='localidadPersona']").val(this.uniqueDataReceived.localidad);
        $("textarea[name='descripcionPersona']").val(this.uniqueDataReceived.descripcion);
        this.loadUserData(this.uniqueDataReceived.dni);
      });
    },
    loadUserData: function (dniPersona) {
      let url = "https://localhost:44368/Profile/GetUsersOwned/" + dniPersona;
      this.recuperarDatosBack(url).then(() => {
        let contador = 1;

      });
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
    this.getProfileData();
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
  margin-left: 150px;
  top: 0px;
  bottom: 200px;
  border: 1px solid red;
}
</style>
