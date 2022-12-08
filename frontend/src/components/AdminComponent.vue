<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="main">
    <div class="top container">
      <div class="bigButton AddPerson" v-on:click="addPerson()">Añadir Persona</div>
      <div class="bigButton AddUser" v-on:click="addUser()">Añadir Usuario</div>
      <div class="bigButton AddTask" v-on:click="addTask()">Añadir Tarea</div>
      <div class="bigButton GetPerson" v-on:click="getPerson()">Mostrar Personas</div>
      <div class="bigButton GetUser" v-on:click="getUser()">Mostrar Usuarios</div>
      <div class="bigButton GetUser" v-on:click="getAdmins()">Mostrar Administradores</div>
      <div class="bigButton GetTask" v-on:click="getTask()">Mostrar Tareas</div>
    </div>
    <div id="contenido" class="bottom container">
      <table id="datatable" class="table datatable"></table>

      <form class="formularioPersona" id="formularioPersona">
        <div class="row mb-4">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">DNI</label>
              <input type="text" id="dniForm" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Nombre</label>
              <input type="text" id="nombreForm" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Apellidos</label>
              <input type="text" id="apellidosForm" class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <div class="row mb-4">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Telefono</label>
              <input type="number" id="telefonoForm" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Localidad</label>
              <input type="text" id="localidadForm" class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <div class="form-outline mb-4">
          <label class="form-label" for="">Descripcion</label>
          <textarea class="form-control bg-dark text-white" id="descripcionForm" rows="3"></textarea>
        </div>
        <button type="button" v-on:click="addPersonSubmit()" class="btn btn-block mb-4 btnRegistro">Registrar
          usuario</button>
        <div class="form-outline mb-4 errorPersona" id="errorPersona">
          <label class="form-label text-danger" for="">El nombre y el apellido no pueden estar vacíos.</label>
        </div>
      </form>

      <form class="formularioUsuario" id="formularioUsuario">
        <div class="row mb-4">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Nombre Usuario</label>
              <input type="text" id="nombreUserForm" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col">
            <div class="form-outline mt-1 ml-4">
              <label class="form-label" for="">DNI Vinculado</label>
              <br>
              <select id="comboDnis" class="form-select form-select-lg mb-2 comboPersonalizado"
                aria-label=".form-select-lg example">
              </select>
            </div>
          </div>
          <div class="col">
            <div class="align-middle form-check blockquote mt-4">
              <input class="form-check-input mt-3" type="checkbox" value="soyAdmin" id="isAdminUserForm">
              <label class="form-check-label ml-1 mt-2" for="">Administrador</label>
            </div>
          </div>
        </div>
        <div class="row mb-4">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Contraseña</label>
              <input type="password" id="passUserForm" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Repetir contraseña</label>
              <input type="password" id="passUserForm2" class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <button type="button" v-on:click="addUserSubmit()" class="btn btn-block mb-4 btnRegistro">
          Registrar usuario
        </button>
        <div class="form-outline mb-4 errorUsuario" id="errorUsuario">
          <label class="form-label text-danger" for=""></label>
        </div>
      </form>

      <form class="formularioTarea" id="formularioTarea">
        <div class="form-outline">
          <label class="form-label" for="">Tarea</label>
          <input type="text" id="tareaForm" class="form-control bg-dark text-white" />
        </div>
        <div class="form-outline mb-4 mt-3">
          <label class="form-label" for="">Descripcion</label>
          <textarea class="form-control bg-dark text-white" id="descripcionTareaForm" rows="3"></textarea>
        </div>
        <div class="form-outline mb-4">
          <select id='estadoForm' class="custom-select bg-dark text-white" size="4">
            <option selected value="Pendiente">Pendiente</option>
            <option value="En progreso">En progreso</option>
            <option value="Bloqueada">Bloqueada</option>
            <option value="Completada">Completada</option>
          </select>
        </div>
        <button type="button" v-on:click="addTaskSubmit()" class="btn btn-block mb-4 btnRegistro">Añadir tarea</button>
        <div class="form-outline mb-4 errorTarea" id="errorTarea">
          <label class="form-label text-danger" for="">El nombre de la tarea no puede estar vacio.</label>
        </div>
      </form>
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
      dataBoolean: false,
      dniDelete: "",
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
    funciono: function () {
      console.log("funciono");
    },
    ocultarTodo: function () {
      $("#formularioPersona").css("display", "none");
      $("#formularioTarea").css("display", "none");
      $("#formularioUsuario").css("display", "none");
      $("#errorPersona").css("display", "none");
      $("#datatable").empty();
    },
    addPerson: function () {
      this.ocultarTodo();
      $("#dniForm").val("");
      $("#nombreForm").val("");
      $("#apellidosForm").val("");
      $("#telefonoForm").val("");
      $("#localidadForm").val("");
      $("#descripcionForm").val("");
      $("#formularioPersona").css("display", "block");
    },
    addPersonSubmit: async function () {
      let nombre, apellidos, descripcion, dni, localidad, telefono;
      dni = document.getElementById("dniForm").value;
      nombre = document.getElementById("nombreForm").value;
      apellidos = document.getElementById("apellidosForm").value;
      descripcion = document.getElementById("descripcionForm").value;
      telefono = document.getElementById("telefonoForm").value;
      localidad = document.getElementById("localidadForm").value;

      // TODO: Falta la comprobación del DNI
      if (dni.length == 0) {
        $("#errorPersona").css("display", "block");
        $("#errorPersona").text("El DNI no es válido");
        $("#errorPersona").css("color", "red");
      } else if (nombre.length == 0 || apellidos.length == 0) {
        $("#errorPersona").css("display", "block");
        $("#errorPersona").text("El nombre y los apellidos no pueden estar vacíos");
        $("#errorPersona").css("color", "red");
      } else {
        let url = "https://localhost:44368/AdminPanel/AddPerson/" + dni + "/" + nombre + "/" + apellidos + "/" + descripcion + "/" + telefono + "/" + localidad;
        this.dataReceived.length = 0;
        this.contactarBack(url).then(() => {
          this.ocultarTodo();
        })
      }
    },
    addUser: function () {
      this.ocultarTodo();

      let html = "";
      let url = "https://localhost:44368/AdminPanel/GetPerson";
      this.dataReceived.length = 0;

      $("#comboDnis").empty();
      this.recuperarDatosBack(url).then(() => {
        this.dataReceived.forEach(element => {
          html += "<option value='" + element.dni + "'>" + element.nombre + " " + element.apellidos + "</option>";
        });
        $("#comboDnis").append(html);
      });

      $("#nombreUserForm").val("");
      $("#passUserForm").val("");
      $("#passUserForm2").val("");
      $("#formularioUsuario").css("display", "block");
    },
    addUserSubmit: function () {
      let user, dniVinculado, pass1, pass2, admin;
      user = document.getElementById("nombreUserForm").value;
      dniVinculado = document.getElementById("comboDnis").value;
      pass1 = document.getElementById("passUserForm").value;
      pass2 = document.getElementById("passUserForm2").value;
      admin = document.getElementById('isAdminUserForm').checked;

      if (user.length == 0) {
        $("#errorUsuario").css("display", "block");
        $("#errorUsuario").text("El usuario debe de poseer un nombre.");
        $("#errorUsuario").css("color", "red");
      } else if (pass1.localeCompare(pass2) || pass1.length == 0) {
        $("#errorUsuario").css("display", "block");
        $("#errorUsuario").text("Las contraseñas no coinciden.");
        $("#errorUsuario").css("color", "red");
      } else {
        $("#errorUsuario").css("display", "none");
        let url = "https://localhost:44368/AdminPanel/AddUser/" + dniVinculado + "/" + user + "/" + pass1 + "/" + admin;
        this.dataReceived.length = 0;
        this.contactarBack(url).then(() => {
          this.ocultarTodo();
        })
      }
    },
    addTask: function () {
      this.ocultarTodo();
      $("#descripcionTareaForm").val("");
      $("#tareaForm").val("");
      $("#formularioTarea").css("display", "block");
    },
    addTaskSubmit: function () {
      let tarea, descripcion, estado;
      tarea = document.getElementById("tareaForm").value;
      descripcion = document.getElementById("descripcionTareaForm").value;
      estado = document.getElementById("estadoForm").value;

      // TODO: Falta la comprobación del DNI
      if (tarea.length == 0) {
        $("#errorTarea").css("display", "block");
        $("#errorTarea").text("El nombre de la tarea no puede estar vacío");
        $("#errorTarea").css("color", "red");
      } else {
        let url = "https://localhost:44368/AdminPanel/AddTask/" + tarea + "/" + descripcion + "/" + estado;
        this.dataReceived.length = 0;
        console.log(url);
        this.contactarBack(url).then(() => {
          this.ocultarTodo();
        })
      }
    },
    getPerson: function () {
      this.ocultarTodo();
      let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:22px;color:rgb(215, 89, 0);'";
      let styleHeaderRow = "style='font-weight:bold;text-align: left;padding-left:0px;padding-right:10px;'";
      let html = "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>DNI</th>" +
        "<th " + styleHeaderRow + " scope='col'>Nombre</th>" +
        "<th " + styleHeaderRow + " scope='col'>Apellidos</th>" +
        "<th " + styleHeaderRow + " scope='col'>Descripcion</th>" +
        "<th " + styleHeaderRow + " scope='col'>Telefono</th>" +
        "<th " + styleHeaderRow + " scope='col'>Localización</th>" +
        "<th " + styleHeaderRow + " scope='col'>Eliminar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetPerson";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        let styleRow = "";
        let evenRow = true;
        this.dataReceived.forEach(element => {
          if (evenRow) {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
            evenRow = false;
          } else {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(27, 58, 59);font-size:20px;'";
            evenRow = true;
          }

          html += "<tr " + styleRow + "><td scope='row'>" + element.dni + "</td>" +
            "<td style='font-size:18px'>" + element.nombre + "</td>" +
            "<td style='font-size:18px'>" + element.apellidos + "</td>" +
            "<td style='font-size:15px'>" + element.descripcion + "</td>" +
            "<td style='font-size:15px'>" + element.telefono + "</td>" +
            "<td style='font-size:15px'>" + element.localidad + "</td>" +
            "<td style='text-align: center'><button value='" + element.dni + "' name='botonBorrarPersona'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
        let vueContext = this; // Para entrar en el contexto de VUE, no el de JQUERY con sus eventos de Click.
        $('button[name="botonBorrarPersona"]').click(function (event) {
          if (confirm('¿Estás seguro de que quieres eliminar a la persona ' + event.currentTarget.value + '?')) {
            vueContext.removePerson(event.currentTarget.value);
          }
        });
      });
    },
    getUser: function () {
      this.ocultarTodo();
      let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:22px;color:rgb(215, 89, 0);'";
      let styleHeaderRow = "style='font-weight:bold;text-align: left;padding-left:0px;padding-right:10px;'";
      let html = "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>ID</th>" +
        "<th " + styleHeaderRow + " scope='col'>Dni</th>" +
        "<th " + styleHeaderRow + " scope='col'>Usuario</th>" +
        "<th " + styleHeaderRow + " scope='col'>Eliminar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetUser/noAdmins";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        let styleRow = "";
        let evenRow = true;
        this.dataReceived.forEach(element => {
          if (evenRow) {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
            evenRow = false;
          } else {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(27, 58, 59);font-size:20px;'";
            evenRow = true;
          }
          html += "<tr " + styleRow + "><td scope='row'>" + element.idUsuario + "</td>" +
            "<td>" + element.dniPersona + "</td>" +
            "<td>" + element.username + "</td>" +
            "<td><button name='botonBorrarUsuario' value='" + element.idUsuario + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
        let vueContext = this; // Para entrar en el contexto de VUE, no el de JQUERY con sus eventos de Click.
        $('button[name="botonBorrarUsuario"]').click(function (event) {
          if (confirm('¿Estás seguro de que quieres eliminar al usuario ' + event.currentTarget.value + '?')) {
            vueContext.removeUser(event.currentTarget.value);
          }
        });
      });
    },
    getAdmins: function () {
      this.ocultarTodo();
      let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:22px;color:rgb(215, 89, 0);'";
      let styleHeaderRow = "style='font-weight:bold;text-align: left;padding-left:0px;padding-right:10px;'";
      let html = "<tr " + styleHeader + "><th  " + styleHeaderRow + " scope='col'>ID</th>" +
        "<th  " + styleHeaderRow + " scope='col'>Dni</th>" +
        "<th  " + styleHeaderRow + " scope='col'>Usuario</th>" +
        "<th  " + styleHeaderRow + " scope='col'>Eliminar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetUser/admins";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        let styleRow = "";
        let evenRow = true;
        this.dataReceived.forEach(element => {
          if (evenRow) {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
            evenRow = false;
          } else {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(27, 58, 59);font-size:20px;'";
            evenRow = true;
          }
          html += "<tr " + styleRow + "><td scope='row'>" + element.idUsuario + "</td>" +
            "<td>" + element.dniPersona + "</td>" +
            "<td>" + element.username + "</td>" +
            "<td><button name='botonBorrarUsuario' value='" + element.idUsuario + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
        let vueContext = this; // Para entrar en el contexto de VUE, no el de JQUERY con sus eventos de Click.
        $('button[name="botonBorrarUsuario"]').click(function (event) {
          if (confirm('¿Estás seguro de que quieres eliminar al administrador ' + event.currentTarget.value + '?')) {
            vueContext.removeUser(event.currentTarget.value);
          }
        });
      });
    },
    getTask: function () {
      this.ocultarTodo();
      let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:22px;color:rgb(215, 89, 0);'";
      let styleHeaderRow = "style='font-weight:bold;text-align: left;padding-left:0px;padding-right:10px;'";
      let html = "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>ID</th>" +
        "<th " + styleHeaderRow + " scope='col'>Nombre</th>" +
        "<th " + styleHeaderRow + " scope='col'>Descripcion</th>" +
        "<th " + styleHeaderRow + " scope='col'>Estado</th>" +
        "<th " + styleHeaderRow + " scope='col'>Cancelar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetTask/all";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        let styleRow = "";
        let evenRow = true;
        this.dataReceived.forEach(element => {
          if (evenRow) {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
            evenRow = false;
          } else {
            styleRow = "style='margin-bottom:4px;text-align: left;border:2px solid black;background-color:rgb(27, 58, 59);font-size:20px;'";
            evenRow = true;
          }
          html += "<tr " + styleRow + "><td scope='row'>" + element.idTarea + "</td>" +
            "<td>" + element.nombre + "</td>" +
            "<td style='font-size:15px'>" + element.descripcion + "</td>" +
            "<td  style='font-size:15px'>" + element.estado + "</td>" +
            "<td style='text-align:center'><button name='botonBorrarTask' value='" + element.idTarea + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
        let vueContext = this; // Para entrar en el contexto de VUE, no el de JQUERY con sus eventos de Click.
        $('button[name="botonBorrarTask"]').click(function (event) {
          if (confirm('¿Deseas cancelar la tarea Nº' + event.currentTarget.value + '?')) {
            vueContext.removeTask(event.currentTarget.value);
          }
        });
      });
    },
    contactarBack: async function (url) {
      try {
        fetch(url)
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            this.dataBoolean = data
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
    removePerson: async function (dniSeleccionado) {
      let url = "https://localhost:44368/AdminPanel/RemovePerson/" + dniSeleccionado;
      this.contactarBack(url).then(() => {
        this.ocultarTodo();
      });
    },
    removeUser: function (idUsuario) {
      let url = "https://localhost:44368/AdminPanel/RemoveUser/" + idUsuario;
      this.contactarBack(url).then(() => {
        this.ocultarTodo();
      });
    },
    removeTask: function (tareaId) {
      let url = "https://localhost:44368/AdminPanel/RemoveTask/" + tareaId;
      this.contactarBack(url).then(() => {
        this.ocultarTodo();
      });
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

/*
Verde principal: rgb(10, 33, 34)
Verde secundario: rgb(20, 43, 44)

Naranja principal: rgb(215, 89, 0)
Naranja secundario: rgb(180, 54, 0)
*/

.main {
  display: flex;
  flex-direction: column;
  height: 900px;
  width: 1500px;
}

.top {
  margin-left: auto;
  display: flex;
  flex-direction: row;
  flex-flow: row wrap;
  justify-content: center;
  width: 100%;
}

.bottom {
  margin-top: 100px;
}

.bigButton {
  padding-top: 10px;
  padding-bottom: 10px;
  padding-left: 20px;
  padding-right: 20px;
  margin: 10px;
  border-style: solid;
  border-width: 5px;
  border-radius: 20px;
  border-color: rgb(153, 46, 0);
  font-weight: bolder;
  font-size: 30px;
  background-color: rgb(20, 43, 44);
  color: rgb(215, 89, 0);
  text-justify: distribute;
}

.datatable {
  text-justify: auto;
}

.btnRegistro {
  background-color: rgb(215, 89, 0);
  font-weight: bolder;
  font-size: 18px;
  color: black;
}


.formularioPersona {
  color: white;
  font-size: 15px;
  display: none;
}

.formularioUsuario {
  color: white;
  font-size: 15px;
  display: none;
}

.formularioTarea {
  color: white;
  font-size: 15px;
  display: none;
}

.errorPersona {
  display: none;
}

.errorTarea {
  display: none;
}

.errorUsuario {
  display: none;
}

.comboPersonalizado {
  width: max-content;
  background-color: rgb(20, 43, 44);
  color: rgb(215, 89, 0);
  font-weight: bold;
  font-size: 20px;
}
</style>
