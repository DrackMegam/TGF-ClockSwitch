<template>
  <div class="mainSidebar">
    <nav class="navbar topSidebar">
      <a @click="volver()" class="navbar-brand"> ClockSwitch</a>
      <span>Bienvenido usuario</span>
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
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Panel de administrador">
          <a class="nav-link">
            <i class="fa fa-user-plus"></i>
            <span @click="goAdmin()" class="nav-link-text">Panel de administrador</span>
          </a>
        </li>
        <li class="nav-item" data-toggle="tooltip" data-placement="right" title="Mis Subscripciones">
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

    </div>
  </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';



export default defineComponent({
  data() {
    return {
      user: String,
      password: String,
    };
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
    volver() {
      this.$router.push('/');
    },
    weekTime() {
      this.$router.push('/weekTime');
    },
    goExport() {
      this.$router.push('/export');
    },
    goEveryone() {
      this.$router.push('/everyone');
    },
    goAdmin() {
      this.$router.push('/admin');
    },
    goSubs() {
      this.$router.push('/subscription');
    },
    goProfile() {
      this.$router.push('/profile');
    },
  },
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
</style>
