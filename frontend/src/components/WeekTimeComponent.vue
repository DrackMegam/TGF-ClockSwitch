<template>
  <div class="main">
    <div id="firstTask" class="container firstTask mt-4">
      <form class="formularioPrimeraTarea" id="formularioPrimeraTarea">
        <div class="form-outline">
          <label class="form-label h4" for="">Parece que aún no has empezado ninguna tarea esta semana. Selecciona una
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
    <div id="weekSummary" class="container weekSummary">
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
      availableDescription: "",
      availableStatus: "",
      availableTaskId: 0,
      userId:0,
      deletedFirstSelectOption: false,
    };
  },
  methods: {
    funciono: function () {
      console.log("funciono");
    },
    getThisWeekSummary: function () {
      let user = 10; // mehamius
      // https://javascript.plainenglish.io/how-to-get-the-week-of-year-of-a-given-date-in-javascript-c8fe0d764b5d
      let thisYear = new Date().getFullYear();
      let now = new Date();
      let onejan = new Date(now.getFullYear(), 0, 1);
      let thisWeek = Math.ceil((((now.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7) - 1; // Para ajustarlo al horario ibérico
      let url = "https://localhost:44368/Semana/WeekSummary/" + user + "/" + thisYear + "/" + thisWeek;
      console.log(url);
      this.recuperarDatosBack(url).then(() => {
        //console.log(this.dataReceived);
        if (this.dataReceived[0].ano == 1928) {
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
        } else {
          console.log("Historial encontrado.");
          this.dataReceived.forEach(e => {
            $("#weekSummary").append("<spam>Algo hay por ahí</spam>");
          })
        }
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
    updateFirstTaskAvailable: function () {
      let user = 10;
      let url = "https://localhost:44368/Semana/AddFirstHistory/" + this.availableTaskId + "/" + user;
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
  background-color: rgb(215, 89, 0);
  font-weight: bolder;
  font-size: 18px;
  color: black;
}
.firstTask{
  display:none;
}

.weekSummary{
  display:none;
}
</style>
