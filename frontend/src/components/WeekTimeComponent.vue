<template>
  <div class="main">
    <div id="weekSummary" class="container weekSummary mt-4">
      <table id="datatable" class="table table-striped datatable uniqueTable"></table>
      <button type="button" id="btnAddTask" v-on:click="addNewTask()" class="btn btn-block mb-4 btnAddTask">
        Añadir nueva tarea
      </button>
    </div>
    <div id="firstTask" class="container firstTask mt-4">
      <form class="formularioPrimeraTarea" id="formularioPrimeraTarea">
        <div class="form-outline" id="">
          <label class="form-label h4 txtFirstTask" id="txtFirstTask" for="">Parece que aún no has empezado ninguna
            tarea esta semana. Selecciona una
            tarea para comenzar: </label>
        </div>
        <div class="form-outline mt-4">
          <label class="form-label" for="">Tarea</label>
          <br>
          <select id="availableTasksCombo" @change="updateAvailableTasksInfo()"
            class="form-select comboPersonalizado form-select-lg mb-3" aria-label=".form-select-lg example">
            <option selected value="0">- Vacío -</option>
          </select>
        </div>
        <div class="row mb-4 mt-1">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Descripción</label>
              <input readonly type="text" placeholder="Descripcion de la tarea" id="descripcionPrimera"
                class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col-lg3">
            <div class="form-outline">
              <label class="form-label" for="">Estado</label>
              <input readonly type="text" placeholder="Estado actual de la tarea" id="estadoPrimera"
                class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <button type="button" id="btnFirstTask" v-on:click="updateFirstTaskAvailable()"
          class="btn btn-block mb-4 btnSubmitPersonalizado">
          Empezar por esta tarea
        </button>
        <div class="form-outline mb-4 errorUsuario" id="errorUsuario">
          <label class="form-label text-danger" for=""></label>
        </div>
      </form>
    </div>

    <div id="addTaskToWeek" class="container addTaskToWeek mt-4">
      <form class="formularioPrimeraTarea" id="formularioPrimeraTarea">

        <div class="form-outline mt-4">
          <label class="form-label" for="">Tarea</label>
          <br>
          <select id="newTasksCombo" @change="updateNewTasksInfo()"
            class="form-select comboPersonalizado form-select-lg mb-3" aria-label=".form-select-lg example">
            <option selected value="0">- Vacío -</option>
          </select>
        </div>
        <div class="row mb-4 mt-1">
          <div class="col">
            <div class="form-outline">
              <label class="form-label" for="">Descripción</label>
              <input readonly type="text" placeholder="Descripcion de la tarea" id="descripcionNueva"
                class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col-lg3">
            <div class="form-outline">
              <label class="form-label" for="">Estado</label>
              <input readonly type="text" placeholder="Estado actual de la tarea" id="estadoNueva"
                class="form-control bg-dark text-white" />
            </div>
          </div>
        </div>
        <button type="button" id="btnNewTask" v-on:click="updateFirstTaskAvailable()"
          class="btn btn-block mb-4 btnSubmitPersonalizado">
          Añadir nueva tarea
        </button>
        <div class="form-outline mb-4 errorUsuario" id="errorUsuario">
          <label class="form-label text-danger" for=""></label>
        </div>
      </form>
    </div>

  </div>
</template>

<script lang="js">
import { defineComponent, isProxy, toRaw } from 'vue';
import jQuery from 'jquery';
let $ = jQuery;

export default defineComponent({
  data() {
    return {
      dataReceived: [],
      uniqueDataReceived: {},
      dataBoolean: false,
      availableDescription: "",
      availableStatus: "",
      availableTaskId: 0,
      userId: 10, // mehamius
      deletedFirstSelectOption: false,
      historyItem: [],
      historyItemFull: [],
    };
  },
  methods: {
    funciono: function () {
      console.log("funciono");
    },
    addNewTask: function () {
      // Relleno el combo con datos nuevos.
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 2; // Para ajustarlo al horario ibérico
      let url = "https://localhost:44368/Semana/GetMoreTasks/" + this.userId + "/" + thisYear + "/" + thisWeek;
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        let html = "";
        this.dataReceived.forEach(element => {
          html += "<option value='" + element.idTarea + "'>" + element.nombre + "</option>";
        });
        $("#newTasksCombo").append(html);
        $("#btnAddTask").css("display", "none");
        $("#addTaskToWeek").css("display", "block");
      });



    },
    getThisWeekSummary: function () {
      // https://javascript.plainenglish.io/how-to-get-the-week-of-year-of-a-given-date-in-javascript-c8fe0d764b5d
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 2; // Para ajustarlo al horario ibérico
      let url = "https://localhost:44368/Semana/WeekSummary/" + this.userId + "/" + thisYear + "/" + thisWeek;
      console.log(url);
      this.recuperarDatosBack(url).then(() => {
        if (this.dataReceived[0].ano == 1928) {
          this.showFirstTaskForm();
        } else {
          this.showThisWeekSummary(thisYear, thisWeek);
        }
      });
    },
    showFirstTaskForm: function () {
      console.log("Esta semana no tiene historial.");
      // Recupero las tareas que NO estén canceladas.
      let url = "https://localhost:44368/Semana/GetAvailableTask";
      this.dataReceived.length = 0;
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        let html = "";
        this.dataReceived.forEach(element => {
          html += "<option value='" + element.idTarea + "'>" + element.nombre + "</option>";
        });
        $("#availableTasksCombo").append(html);
      });
      $("#btnFirstTask").attr("disabled", true); // Deshabilito el botón hasta seleccionar Tarea.
      $("#btnFirstTask").css("background-color", "rgb(20, 43, 44)");
      $("#btnFirstTask").css("color", "white");
      $("#firstTask").css("display", "block");
    },
    showThisWeekSummary: async function (year, weekOfYear) {
      console.log("Historial encontrado.");
      // Hago otra petición, pero esta vez para que me devuelva también los nombres y estados de las tareas.
      let url = "https://localhost:44368/Semana/WeekFullSummary/" + this.userId + "/" + year + "/" + weekOfYear;
      this.dataReceived = [];
      this.recuperarDatosBack(url).then(() => {
        console.log(this.dataReceived);
        // Cabezera de la tabla.
        let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:25px;color:rgb(215, 89, 0);'";
        let styleHeaderRow = "style='padding-left:7px;padding-right:7px;'";
        let html = "<thead>" +
          "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>ID</th>" +
          "<th " + styleHeaderRow + " scope='col'>Nombre</th>" +
          "<th " + styleHeaderRow + " scope='col'>Estado</th>" +
          "<th " + styleHeaderRow + " scope='col'>Lunes</th>" +
          "<th " + styleHeaderRow + " scope='col'>Martes</th>" +
          "<th " + styleHeaderRow + " scope='col'>Miercoles</th>" +
          "<th " + styleHeaderRow + " scope='col'>Jueves</th>" +
          "<th " + styleHeaderRow + " scope='col'>Viernes</th>" +
          "<th " + styleHeaderRow + " scope='col'>Sabado</th>" +
          "<th " + styleHeaderRow + " scope='col'>Domingo</th>" +
          "<th " + styleHeaderRow + " scope='col'>Total</th>" +
          "<th></th>" +
          "</tr></thead>";
        let sumatorioTask = 0;
        let sumatorioTotal = 0;
        let sumatorioDay = {
          lunes: 0,
          martes: 0,
          miercoles: 0,
          jueves: 0,
          viernes: 0,
          sabado: 0,
          domingo: 0,
        }

        // Estilos de las filas y mostrar los datos recuperados.
        let styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
        let evenRow = true;
        this.dataReceived.forEach(e => {
          sumatorioTask = e.horasLunes + e.horasMartes + e.horasMiercoles
            + e.horasJueves + e.horasViernes
            + e.horasSabado + e.horasDomingo;
          if (evenRow) {
            styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";
            evenRow = false;
          } else {
            styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(27, 58, 59);font-size:20px;'";
            evenRow = true;
          }
          let styleText = "style='font-size:15px;padding-left:15px;padding-right:15px;'";
          html += "<tbody>" +
            "<tr " + styleRow + "><th scope='row'>" + e.idTarea + "</th>" +
            "<td " + styleText + ">" + e.nombreTarea + "</td>" +
            "<td " + styleText + ">" + e.estadoTarea + "</td>" +
            "<td>" + e.horasLunes + "</td>" +
            "<td>" + e.horasMartes + "</td>" +
            "<td>" + e.horasMiercoles + "</td>" +
            "<td>" + e.horasJueves + "</td>" +
            "<td>" + e.horasViernes + "</td>" +
            "<td>" + e.horasSabado + "</td>" +
            "<td>" + e.horasDomingo + "</td>" +
            "<td>" + sumatorioTask + "</td>" +
            "</tr>";
          sumatorioTotal += sumatorioTask;
          sumatorioDay.lunes += e.horasLunes;
          sumatorioDay.martes += e.horasMartes;
          sumatorioDay.miercoles += e.horasMiercoles;
          sumatorioDay.jueves += e.horasJueves;
          sumatorioDay.viernes += e.horasViernes;
          sumatorioDay.sabado += e.horasSabado;
          sumatorioDay.domingo += e.horasDomingo;
        });

        // Pie de la tabla con los sumatorios.
        let styleSubHeader = "style='text-align: right;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;color:rgb(215, 89, 0);font-weight:bold;'";
        html += "<tr " + styleSubHeader + " ><th scope='row'></th>" +
          "<td></td>" +
          "<td></td>" +
          "<td>" + sumatorioDay.lunes + "</td>" +
          "<td>" + sumatorioDay.martes + "</td>" +
          "<td>" + sumatorioDay.miercoles + "</td>" +
          "<td>" + sumatorioDay.jueves + "</td>" +
          "<td>" + sumatorioDay.viernes + "</td>" +
          "<td>" + sumatorioDay.sabado + "</td>" +
          "<td>" + sumatorioDay.domingo + "</td>" +
          "<td>" + sumatorioTotal + "</td>" +
          "</tr></tbody>";
        $("#weekSummary").css("display", "block");
        $("#datatable").append(html);
      });

    },
    updateAvailableTasksInfo: function () {
      var selectedAvailableTask = $('#availableTasksCombo').find(":selected").val();
      console.log(selectedAvailableTask);
      let url = "https://localhost:44368/Semana/GetSingleTask/" + selectedAvailableTask;
      this.updateCurrentAvailableTask(url).then(() => {
        $("#descripcionPrimera").val(this.availableDescription);
        $("#estadoPrimera").val(this.availableStatus);
        if (!this.deletedFirstSelectOption) { // Para evitar bugs al escoger una opción.
          $("#availableTasksCombo").find('option').get(0).remove();
          this.deletedFirstSelectOption = true;
        }
        $("#btnFirstTask").attr("disabled", false); // Habilito el botón tras obtener la información.
        $("#btnFirstTask").css("background-color", "rgb(215, 89, 0)");
        $("#btnFirstTask").css("color", "rgb(20, 43, 44)");
      });

    },
    updateNewTasksInfo: function () {
      var selectedNewTask = $('#newTasksCombo').find(":selected").val();
      console.log(selectedNewTask);
      let url = "https://localhost:44368/Semana/GetSingleTask/" + selectedNewTask;
      this.updateCurrentAvailableTask(url).then(() => {
        $("#descripcionNueva").val(this.availableDescription);
        $("#estadoNueva").val(this.availableStatus);
        if (!this.deletedFirstSelectOption) { // Para evitar bugs al escoger una opción.
          $("#newTasksCombo").find('option').get(0).remove();
          this.deletedFirstSelectOption = true;
        }
        $("#btnNewTask").attr("disabled", false); // Habilito el botón tras obtener la información.
        $("#btnNewTask").css("background-color", "rgb(215, 89, 0)");
        $("#btnNewTask").css("color", "rgb(20, 43, 44)");
      });

    },
    updateFirstTaskAvailable: function () {
      let url = "https://localhost:44368/Semana/AddFirstHistory/" + this.availableTaskId + "/" + this.userId;
      this.contactarBack(url).then(() => {
        this.$router.go(this.$router.currentRoute);
      });
    },
    contactarBack: async function (url) {
      try {
        return fetch(url)
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
    updateCurrentAvailableTask: async function (url) {
      try {
        return fetch(url)
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            this.availableDescription = data.descripcion;
            this.availableStatus = data.estado;
            this.availableTaskId = data.idTarea;
          })
          .catch(error => {
            console.error(error);
          });
      }
      catch (e) { console.log(e) }
    }
  },
  beforeMount() {
    this.getThisWeekSummary();
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
}

.firstTask {
  display: none;
}

.comboPersonalizado {
  width: 100%;
  background-color: rgb(20, 43, 44);
  color: rgb(215, 89, 0);
  font-weight: bold;
  font-size: 20px;
}

.formularioPrimeraTarea {
  color: white;
  font-size: 18px;
}

.btnSubmitPersonalizado {
  background-color: rgb(20, 43, 44);
  font-weight: bolder;
  font-size: 18px;
  color: black;
}

.btnAddTask {
  font-weight: bolder;
  font-size: 18px;
  color: white;
  background-color: rgb(215, 89, 0);
}

.firstTask {
  display: none;
}

.weekSummary {
  display: none;
  width: 100%;

}

.datatable {
  display: block;
  width: 100%;
}

/* https://www.w3schools.com/css/tryit.asp?filename=trycss_table_fancy */
.uniqueTable {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

.uniqueTable td,
.uniqueTable th {
  border: 1px solid #ddd;
  padding: 8px;
}

.uniqueTable tr:nth-child(even) {
  background-color: #f2f2f2;
}

.uniqueTable tr:hover {
  background-color: #ddd;
}

.uniqueTable th {
  padding-top: 12px;
  padding-bottom: 12px;
  text-align: left;
  background-color: #04AA6D;
  color: white;
  font-size: 20px;
}

.addTaskToWeek {
  display: none;
}
</style>
