<template>
  <div class="mainSidebar">
    <nav class="navbar topSidebar">
      <a @click="main()" class="navbar-brand"> <img src="../assets/ClockSwitchLogo.png" class="logoCS"
          alt="ClockSwitch"></a>
      <span class="welcome mt-4">Bienvenido <span class="userWelcome">{{ this.username }}</span></span>

    </nav>
    <div class="leftSidebar">
      <ul class="navbar-nav navbar-sidenav">
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Mi resumen semanal">
          <a class="nav-link">
            <i class="fa fa-fw fa-table"></i>
            <span @click="weekTime()" class="nav-link-text">Mi resumen semanal</span>
          </a>
        </li>
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Horas de Todos">
          <a class="nav-link">
            <i class="fa fa-fw fa-table"></i>
            <span @click="goEveryone()" class="nav-link-text">Horas de todos</span>
          </a>
        </li>
        <li v-if="(isAdmin == 1)" class="nav-item" data-toggle="tooltip" data-placement="right"
          title="Panel de administrador">
          <a class="nav-link">
            <i class="fa fa-user-plus"></i>
            <span @click="goAdmin()" class="nav-link-text">Panel de administrador</span>
          </a>
        </li>
        <li v-if="(isAdmin == 1)" class="nav-item" data-toggle="tooltip" data-placement="right"
          title="Mis Subscripciones">
          <a class="nav-link">
            <i class="fa fa-users"></i>
            <span @click="goSubs()" class="nav-link-text">Mis Subscripciones</span>
          </a>
        </li>
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Mi perfil">
          <a class="nav-link">
            <i class="fa fa-users"></i>
            <span @click="goProfile()" class="nav-link-text">Mi perfil</span>
          </a>
        </li>
      </ul>

      <div class="disconnect">
        <button type="button" id="btnDisconnect" @click="volver()" class="btn btn-block mb-4 btnDisconnect">
          Desconectar
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';



export default defineComponent({
  data() {
    return {
      isAdmin: 0
    };
  },
  props: {
    username: String
  },
  created() {

  },
  /*
  components: {
      LoginComponent
  },*/
  watch: {

  },
  methods: {
    showAdminOptions: function () {
      try {
        return fetch("https://83.33.245.0:8081/Login/AmIAdmin/" + this.username)
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            this.isAdmin = data; // Me devuelve un 1 o un 0.
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    },
    volver() {
      this.$router.push('/');
    },
    main() {
      this.$router.push('/main/' + this.username);
    },
    weekTime() {
      this.$router.push('/weekTime/' + this.username);
    },
    goExport() {
      this.$router.push('/export/' + this.username);
    },
    goEveryone() {
      this.$router.push('/everyone/' + this.username);
    },
    goAdmin() {
      this.$router.push('/admin/' + this.username);
    },
    goSubs() {
      this.$router.push('/subscription/' + this.username);
    },
    goProfile() {
      this.$router.push('/profile/' + this.username);
    },
  },
  beforeMount() {
    this.showAdminOptions();
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

.mainSidebar {
  z-index: 10;
}

.topSidebar {
  position: fixed;
  top: 0;
  left: 0;
  height: 70px;
  width: 100%;
  background-color: rgb(10, 33, 34);
  color: rgb(215, 89, 0);
  /*
    rgb(30, 53, 54)
    rgb(30, 89, 54)
  */
}

.leftSidebar {
  position: fixed;
  top: 70px;
  left: 0;
  height: 100%;
  width: 300px;
  background-color: rgb(20, 43, 44);
}

.nav-link-text {
  font-size: 20px;
  padding-left: 10px;
  padding-top: 5px;
}

.nav-item {
  margin-top: 20px;
  margin-left: 10px;
}

.disconnect {
  position: fixed;
  margin-left: 20px;
  bottom: 0px;
}

.btnDisconnect {
  width: 250px;
  background-color: rgb(10, 33, 34);
  border: 1px solid rgb(215, 89, 0);
  border-radius: 9px;
  color: rgb(215, 89, 0);
  letter-spacing: 5px;
  font-weight: bold;
}

.welcome {
  font-size: 20px;
  letter-spacing: 2px;
  font-weight: bold;
  color: rgb(180, 54, 0);
}

.userWelcome {
  letter-spacing: 0px;
  font-weight: bold;
  color: rgb(180, 54, 0);
}

.logoCS {
  position: fixed;
  top:-10px;
  width: 250px;
  height: 95px;
}
</style>
