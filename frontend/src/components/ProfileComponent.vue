<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="main">

    <div class="container mb-4 mt-2">
      <div class="form-outline text-center">
        <label class="form-label"><span class="txtTrabajador">Datos del trabajador</span></label>
      </div>
      <div class="formularioPersona">
        <form class="ml-3 mr-3 mt-2">
          <div class="row mb-4">
            <div class="col">
              <div class="form-outline">
                <label class="sobretexto" for="">Nombre</label>
                <input readonly type="text" name="nombrePersona" class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-6">
              <div class="form-outline">
                <label class="sobretexto" for="">Apellidos</label>
                <input readonly type="text" name="apellidosPersona" class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-3">
              <div class="form-outline">
                <label class="sobretexto" for="">DNI</label>
                <input readonly type="text" name="dniPersona" class="form-control bg-dark text-white" />
              </div>
            </div>
          </div>
          <div class="row mb-4">
            <div class="col-3">
              <div class="form-outline">
                <label class="sobretexto" for="">Telefono</label>
                <input readonly type="number" name="telefonoPersona" id="passUserForm"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col">
              <div class="form-outline">
                <label class="sobretexto" for="">Localidad</label>
                <input readonly type="text" name="localidadPersona" class="form-control bg-dark text-white" />
              </div>
            </div>
          </div>
          <div class="form-outline mb-4">
            <label class="sobretexto" for="">Descripcion</label>
            <textarea readonly name="descripcionPersona" class="form-control bg-dark text-white" rows="3"></textarea>
          </div>
        </form>
      </div>

    </div>

    <div class="container" v-if="(dataReceived.length > 0)">
      <div class="form-outline text-center">
        <label class="form-label"><span class="txtTrabajador">Usuarios asociados</span></label>
      </div>
    </div>

    <template v-for="data in dataReceived">
      <div class="container mt-1 mb-2 singleUser pt-2">
        <form class="formularioUsuario" id="formularioUsuario">
          <div class="row mb-4">
            <div class="col">
              <div class="form-outline">
                <label class="sobretexto" for="">Nombre Usuario</label>
                <input type="text" :value=data.username readonly name="nombreUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-2">
              <div class="form-outline">
                <label class="sobretexto" for="">Identificador</label>
                <input type="number" :value=data.idUsuario readonly name="idUsuario"
                  class="form-control bg-dark text-white" />
              </div>
            </div>
            <div class="col-3">
              <div class="form-outline">
                <label class="sobretexto" for="">Permisos</label>
                <input type="text" v-if="(data.isAdmin == 1)" value="Administrador" readonly name="permisosUsuario"
                  class="form-control bg-dark text-white" />
                <input type="text" v-else value="Trabajador" readonly name="permisosUsuario"
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
      userId: 0,
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
    getProfileData: function () {
      let url = "https://83.33.245.0:8081/Profile/GetOwner/" + this.userId;
      this.recuperarDatoUnicoBack(url).then(() => {
        let newUrl = "https://83.33.245.0:8081/Profile/GetPerson/" + this.uniqueDataReceived.dniPersona;
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
      let url = "https://83.33.245.0:8081/Profile/GetUsersOwned/" + dniPersona;
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
    this.getUserId(this.$route.params.username).then(() => {
      this.getProfileData();
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
  display: fixed;
  height: 100%;
  width: 1500px;
  top: 0;
}

.mainSubs {
  display: flex;
  flex-direction: column;
  top: 80px;
  height: 100%;
  width: 1500px;
  padding-bottom: 100px;
  margin-left: 160px;
  z-index: 0;
}

.txtTrabajador {
  color: rgb(180, 54, 0);
  font-size: 25px;
  font-weight: bold;
  letter-spacing: 2px;
  text-align: center;
}

.sobretexto {
  color: rgb(215, 89, 0);
  font-weight: bold;
}

.singleUser {
  background-color: rgb(20, 43, 44);
  border-radius: 20px;
  border: 2px solid rgb(180, 54, 0);
}

.formularioPersona {
  background-color: rgb(20, 43, 44);
  border-radius: 20px;
  border: 2px solid rgb(180, 54, 0);
}
</style>
