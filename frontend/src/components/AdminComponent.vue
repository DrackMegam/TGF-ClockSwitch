<template>
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

      <form class="formularioUsuario" id="formularioUsuario">
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
        <button type="button" v-on:click="addPersonSubmit()" class="btn btn-block mb-4 btnRegistro">Registrar usuario</button>
        <div class="form-outline mb-4 errorPersona" id="errorPersona">
          <label class="form-label text-danger" for="">El nombre y el apellido no pueden estar vacíos.</label>
        </div>
      </form>


      <form class="formularioTarea" id="formularioTarea">
        <div class="form-outline">
          <label class="form-label" for="">Tarea</label>
          <input type="text" id="nombreForm" class="form-control bg-dark text-white" />
        </div>
        <div class="form-outline mb-4 mt-3">
          <label class="form-label" for="">Descripcion</label>
          <textarea class="form-control bg-dark text-white" id="descripcionForm" rows="3"></textarea>
        </div>
        <div class="form-outline mb-4">
          <select class="custom-select bg-dark text-white" size="4">
            <option selected value="Pendiente">Pendiente</option>
            <option value="En progreso">En progreso</option>
            <option value="Bloqueada">Bloqueada</option>
            <option value="Completada">Completada</option>
          </select>
        </div>
        <button type="button" v-on:click="funciono()" class="btn btn-block mb-4 btnRegistro">Registrar usuario</button>
      </form>
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
      dataBoolean: false,
    };
  },
  created() {

  },
  watch: {

  },
  methods: {
    funciono: function () {
      console.log("funciono");
    },
    ocultarTodo: function(){
      $("#formularioUsuario").css("display", "none");
      $("#formularioTarea").css("display", "none");
      $("#errorPersona").css("display","none");
      $("#datatable").empty();
    },
    addPerson: function () {
      this.ocultarTodo();
      $("#formularioUsuario").css("display", "block");
    },
    addPersonSubmit: async function () {
      let nombre,apellidos,descripcion,dni,localidad,telefono;
      dni = document.getElementById("dniForm").value;
      nombre = document.getElementById("nombreForm").value;
      apellidos = document.getElementById("apellidosForm").value;
      descripcion = document.getElementById("descripcionForm").value;
      telefono = document.getElementById("telefonoForm").value;
      localidad = document.getElementById("localidadForm").value;

      // TODO: Falta la comprobación del DNI
      if(dni.length == 0){
        $("#errorPersona").css("display","block");
        $("#errorPersona").text("El DNI no es válido");
        $("#errorPersona").css("color","red");
      }else if(nombre.length == 0 || apellidos.length == 0){
        $("#errorPersona").css("display","block");
        $("#errorPersona").text("El nombre y los apellidos no pueden estar vacíos");
        $("#errorPersona").css("color","red");
      }else{
        let url="https://localhost:44368/AdminPanel/AddPerson/"+dni+"/"+nombre+"/"+apellidos+"/"+descripcion+"/"+telefono+"/"+localidad;
        this.dataReceived.length=0;
        this.contactarBack(url).then(() => {
          console.log(this.dataReceived);
          if(this.dataReceived[0] == true){
            // TODO: No se calca bien la respuesta del método en this.dataRecieved
            console.log("TODO UN EXITO JEFE MAESTRO MAKINA")
          }else{
            console.log("FUK")
          }
        })
      }
    },
    addUser: function () {
      console.log("addUser");
    },
    addUserSubmit: function () {
      this.contactarBack("aaa");
    },
    addTask: function () {
      this.ocultarTodo();
      $("#formularioTarea").css("display", "block");

    },
    addTaskSubmit: function () {
      this.contactarBack("aaa");
    },
    getPerson: function () {
      this.ocultarTodo();
      let html = "<tr><th scope='col'>ID</th>" +
        "<th scope='col'>Nombre</th>" +
        "<th scope='col'>Apellidos</th>" +
        "<th scope='col'>Descripcion</th>" +
        "<th scope='col'>Telefono</th>" +
        "<th scope='col'>Localización</th>" +
        "<th scope='col'>Eliminar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetPerson";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        this.dataReceived.forEach(element => {
          html += "<tr><td scope='row'>" + element.dni + "</td>" +
            "<td>" + element.nombre + "</td>" +
            "<td>" + element.apellidos + "</td>" +
            "<td>" + element.descripcion + "</td>" +
            "<td>" + element.localidad + "</td>" +
            "<td>" + element.telefono + "</td>" +
            "<td><button id='" + element.dni + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
      });
    },
    getUser: function () {
      this.ocultarTodo();
      let html = "<tr><th scope='col'>IdUsuario</th>" +
        "<th scope='col'>Dni</th>" +
        "<th scope='col'>Usuario</th>" +
        "<th scope='col'>Administrador</th>" +
        "<th scope='col'>Eliminar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetUser/noAdmins";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        this.dataReceived.forEach(element => {
          html += "<tr><td scope='row'>" + element.idUsuario + "</td>" +
            "<td>" + element.dniPersona + "</td>" +
            "<td>" + element.username + "</td>" +
            "<td>" + element.isAdmin + "</td>" +
            "<td><button id='" + element.dni + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
      });
    },
    getAdmins: function () {
      this.ocultarTodo();
      let html = "<tr><th scope='col'>IdUsuario</th>" +
        "<th scope='col'>Dni</th>" +
        "<th scope='col'>Usuario</th>" +
        "<th scope='col'>Administrador</th>" +
        "<th scope='col'>Eliminar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetUser/admins";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        this.dataReceived.forEach(element => {
          html += "<tr><td scope='row'>" + element.idUsuario + "</td>" +
            "<td>" + element.dniPersona + "</td>" +
            "<td>" + element.username + "</td>" +
            "<td>" + element.isAdmin + "</td>" +
            "<td><button id='" + element.dni + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
      });
    },
    getTask: function () {
      this.ocultarTodo();
      let html = "<tr><th scope='col'>ID</th>" +
        "<th scope='col'>Nombre</th>" +
        "<th scope='col'>Descripcion</th>" +
        "<th scope='col'>Estado</th>" +
        "<th scope='col'>Cancelar</th>" +
        "</tr>";
      let url = "https://localhost:44368/AdminPanel/GetTask/all";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        this.dataReceived.forEach(element => {
          html += "<tr><td scope='row'>" + element.idTarea + "</td>" +
            "<td>" + element.nombre + "</td>" +
            "<td>" + element.descripcion + "</td>" +
            "<td>" + element.estado + "</td>" +
            "<td><button id='" + element.idTarea + "'>X</button></td>" +
            "</tr>";
        });
        $("#datatable").append(html);
      });
    },
    contactarBack: async function (url) {
      try {
        fetch(url)
          .then((response) => response.json())
          .then((data) => {
            if(data){
              console.log(data+" NO SOY FUK");
              this.dataReceived[0](true)
            }else{
              console.log(data+" FUK Y TU PUTA MADRE");
              this.dataReceived[0](false)
            }
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
    removePerson: function () {
      this.contactarBack("aaa");
    },
    removeUser: function () {
      this.contactarBack("aaa");
    },
    removeTask: function () {
      this.contactarBack("aaa");
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
  background-color: rgb(215, 89, 0);
  color: black;
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

.errorPersona{
  display:none;
}

</style>
