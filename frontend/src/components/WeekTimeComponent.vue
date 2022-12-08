<template>
  <SidebarComponent :username=this.$route.params.username />
  <div class="main">

    <div id="weekSummary" class="container  weekSummary  mt-4">

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
      <form class="formularioNormal" id="formularioNormal">

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
              <input readonly type="text" placeholder="Descripcion de la tarea" name="descripcionNueva"
                id="descripcionNueva" class="form-control bg-dark text-white" />
            </div>
          </div>
          <div class="col-lg3">
            <div class="form-outline">
              <label class="form-label" for="">Estado</label>
              <input readonly type="text" placeholder="Estado actual de la tarea" name="estadoNueva" id="estadoNueva"
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

    <div id="updateHistory" class="container updateHistory mt-4">
      <table id="tableUpdate" class="table table-striped tableUpdate uniqueTable"></table>
    </div>

  </div>
  <FooterComponent />

</template>

<script lang="js">
import { defineComponent, isProxy, toRaw } from 'vue';
import FooterComponent from '../components/FooterComponent.vue'
import SidebarComponent from '../components/SidebarComponent.vue'
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
      userId: 0,
      deletedFirstSelectOption: false,
      historyItem: [],
      historyItemFull: [],
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
  methods: {
    funciono: function () {
      console.log("funciono");
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
        this.moveInTime();
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
      this.moveInTime();
      this.getDateOfWeek(this.actualWeekOfYear, this.actualYear);
    },
    moveInTime: function () {
      this.dataReceived = [];
      this.uniqueDataReceived = {};
      this.dataBoolean = false;
      this.availableDescription = "";
      this.availableStatus = "";
      this.availableTaskId = 0;
      this.deletedFirstSelectOption = false;
      this.historyItem = [];
      this.historyItemFull = [];
      $("#datatable").empty();
      $("#updateHistory").empty();
      $("#addTaskToWeek").css("display", "none");
      $("#btnAddTask").css("display", "block");
      $("#firstTask").css("display", "none");
      this.getThisWeekSummary();
    },
    reloadSummary: function () {
      this.dataReceived = [];
      this.uniqueDataReceived = {};
      this.dataBoolean = false;
      this.availableDescription = "";
      this.availableStatus = "";
      this.availableTaskId = 0;
      this.deletedFirstSelectOption = false;
      this.historyItem = [];
      this.historyItemFull = [];
      this.dateTimeString = "Semana actual";
      $("#datatable").empty();
      $("#updateHistory").empty();
      $("#addTaskToWeek").css("display", "none");
      $("#btnAddTask").css("display", "block");
      $("#firstTask").css("display", "none");
      this.getTimeData().then(() => {
        this.getThisWeekSummary();
      });
    },
    getTimeData: async function () {
      try {
        return fetch("https://localhost:44368/Semana/GetTimeData")
          .then((response) => response.json())
          .then((data) => {
            console.log(data);
            // Se de seguro lo que devuelve. Un array de 2 valores.
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
    addNewTask: function () {
      // Relleno el combo con datos nuevos.
      /*
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 1; // Para ajustarlo al horario ibérico
      */
      let url = "https://localhost:44368/Semana/GetMoreTasks/" + this.userId + "/" + this.actualYear + "/" + this.actualWeekOfYear;
      this.dataReceived.length = 0;
      $("#newTasksCombo").empty();
      $("#newTasksCombo").append('<option selected value="0">- Vacío -</option>');
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
      /*
      // https://javascript.plainenglish.io/how-to-get-the-week-of-year-of-a-given-date-in-javascript-c8fe0d764b5d
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 1; // Para ajustarlo al horario ibérico
      */
      let url = "https://localhost:44368/Semana/WeekSummary/" + this.userId + "/" + this.actualYear + "/" + this.actualWeekOfYear;
      console.log(url);
      this.recuperarDatosBack(url).then(() => {
        if (this.dataReceived[0].ano == 1928) {
          this.showFirstTaskForm();
        } else {
          this.showThisWeekSummary(this.actualYear, this.actualWeekOfYear);
        }
      });
    },
    showFirstTaskForm: function () {
      console.log("Esta semana no tiene historial.");
      // Recupero las tareas que NO estén canceladas.
      let url = "https://localhost:44368/Semana/GetAvailableTask";
      this.dataReceived.length = 0;
      $("#availableTasksCombo").empty();
      $("#availableTasksCombo").append('<option selected value="0">- Vacío -</option>');
      $("#descripcionPrimera").val('');
      $("#estadoPrimera").val('');

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
      $("#btnAddTask").css("display", "none");
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
        let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:22px;color:rgb(215, 89, 0);'";
        let styleHeaderRow = "style='text-align: left;padding-left:7px;padding-right:7px;'";
        let styleHeaderRowNumber = "style='text-align: right;padding-left:7px;padding-right:7px;font-size:18px;'";
        let html = "<thead>" +
          "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>ID</th>" +
          "<th " + styleHeaderRow + " scope='col'>Nombre</th>" +
          "<th " + styleHeaderRow + " scope='col'>Estado</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Lunes</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Martes</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Miercoles</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Jueves</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Viernes</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Sabado</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Domingo</th>" +
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
          let styleText = "style='text-align: left;font-size:15px;padding-left:10px;padding-right:15px;'";
          let styleEditButton = "style='padding-left:25px;padding-right:15px;'";
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
            "<td " + styleEditButton + " name='editHistory' id='" + e.id + "'> Editar </td>" +
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
          "<td></td>" +
          "</tr></tbody>";
        $("#weekSummary").css("display", "block");
        $("#datatable").append(html);
        // https://www.digitalocean.com/community/tutorials/js-object-reference-context-scope-instantiation
        let vueContext = this;
        $("td[name='editHistory']").click(function (event) {
          vueContext.updateHistory(event.currentTarget.id);
          console.log("Actualizando historico " + event.currentTarget.id);
        });
      });
    },
    updateHistory: function (idHistory) {
      $("#weekSummary").css("display", "none");
      $("#addTaskToWeek").css("display", "none");
      let url = "https://localhost:44368/Semana/GetSingleHistoryToUpdate/" + idHistory;
      console.log(url);

      this.recuperarDatoUnicoBack(url).then(() => {
        console.log(this.uniqueDataReceived);
        // Cabezera de la tabla.
        let styleHeader = "style='border:2px solid black;font-weight:bolder;background-color:rgb(10, 33, 34);font-size:22px;color:rgb(215, 89, 0);'";
        let styleHeaderRow = "style='text-align: left;padding-left:7px;padding-right:7px;'";
        let styleHeaderRowNumber = "style='text-align: right;padding-left:7px;padding-right:7px;font-size:18px;'";
        let html = "<thead>" +
          "<tr " + styleHeader + "><th " + styleHeaderRow + " scope='col'>ID</th>" +
          "<th " + styleHeaderRow + " scope='col'>Nombre</th>" +
          "<th " + styleHeaderRow + " scope='col'>Estado</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Lunes</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Martes</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Miercoles</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Jueves</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Viernes</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Sabado</th>" +
          "<th " + styleHeaderRowNumber + " scope='col'>Domingo</th>" +
          "<th></th>" +
          "</tr></thead>";

        // Estilos de las filas y mostrar los datos recuperados.
        let styleRow = "style='text-align: right;border:2px solid black;background-color:rgb(20, 43, 44);font-size:20px;'";

        // Atención, una cosa es el idTarea, y otra id, que es de la entrada al historial.
        let styleText = "style='text-align: left;font-size:15px;padding-left:10px;padding-right:15px;'";
        let styleInputNumber = "style='width:30px;font-size:20px;background-color:rgb(20, 43, 44);color:rgb(215, 89, 0);'";
        html += "<tbody>" +
          "<tr " + styleRow + "><th scope='row'>" + this.uniqueDataReceived.idTarea + "</th>" +
          "<td " + styleText + ">" + this.uniqueDataReceived.nombreTarea + "</td>" +
          "<td " + styleText + ">" + this.uniqueDataReceived.estadoTarea + "</td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='lunes' value='" + this.uniqueDataReceived.horasLunes + "'/></td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='martes' value='" + this.uniqueDataReceived.horasMartes + "'/></td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='miercoles' value='" + this.uniqueDataReceived.horasMiercoles + "'/></td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='jueves' value='" + this.uniqueDataReceived.horasJueves + "'/></td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='viernes' value='" + this.uniqueDataReceived.horasViernes + "'/></td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='sabado' value='" + this.uniqueDataReceived.horasSabado + "'/></td>" +
          "<td><input " + styleInputNumber + " type='number' min='1' max='14' name='domingo' value='" + this.uniqueDataReceived.horasDomingo + "'/></td>" +
          "<td style='padding-left:25px;padding-right:15px;color:rgb(215, 89, 0);' name='saveHistory' id='" + this.uniqueDataReceived.id + "'>Actualizar</td>" +
          "</tr>";

        $("#updateHistory").css("display", "block");
        $("#updateHistory").append(html);
        let vueContext = this;
        $("td[name='saveHistory']").click(function (event) {
          let updatedHours = {
            lunes: $("input[name=lunes]").val(),
            martes: $("input[name=martes]").val(),
            miercoles: $("input[name=miercoles]").val(),
            jueves: $("input[name=jueves]").val(),
            viernes: $("input[name=viernes]").val(),
            sabado: $("input[name=sabado]").val(),
            domingo: $("input[name=domingo]").val(),
          }
          let urlUpdate = "https://localhost:44368/Semana/UpdateHistory/" + idHistory + "/" + updatedHours.lunes + "/" + updatedHours.martes + "/" + updatedHours.miercoles + "/" + updatedHours.jueves + "/" + updatedHours.viernes + "/" + updatedHours.sabado + "/" + updatedHours.domingo;
          console.log(urlUpdate);
          vueContext.recuperarDatoUnicoBack(urlUpdate).then(() => {
            console.log("Actualizado");
            // TODO: recarga activa
            vueContext.reloadSummary();
          })
        });
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
      let url = "https://localhost:44368/Semana/AddTaskHistory/" + this.availableTaskId + "/" + this.userId + "/" + this.actualWeekOfYear + "/" + this.actualYear;
      this.contactarBack(url).then(() => {
        this.reloadSummary();
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
    this.getUserId(this.$route.params.username).then(() => {
      this.getTimeData().then(() => {
        this.getThisWeekSummary();
      });
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

.formularioNormal {
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
  text-justify: auto;
}

/* https://www.w3schools.com/css/tryit.asp?filename=trycss_table_fancy */
.uniqueTable {
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;
  width: 100%;
  height: 100%;
  margin: 0 auto;
  text-justify: auto;
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

.updateHistory {
  display: none;
}

.btnSubmitCancelar {
  width: 200px;
  color: rgb(215, 89, 0);
}

.btnSuperiores {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.flecha{
  font-weight: bold;
  font-size: 45px;
  letter-spacing: -10px;
  color: rgb(180, 54, 0);
}

.weekInfo{
  font-weight: bold;
  font-size: 25px;
  letter-spacing: 2px;
  color: rgb(215, 89, 0);
}
</style>
