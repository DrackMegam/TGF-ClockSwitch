<template>
  <div class="principal">
    <form @submit.prevent="login" class="form">
      <div class="top-header">
        <img src="" />
        <span class="title">ClockSwitch</span>
      </div>
      <label class="form-label" for="#email">Usuario:</label>
      <input v-model="user" class="form-input" id="user" required placeholder="Nombre del usuario" />
      <label class="form-label" for="#password">Contraseña:</label>
      <input v-model="password" class="form-input" type="password" id="password" placeholder="Contraseña" />
      <input class="form-submit" type="submit" value="Login" />
      <p v-if="error" class="error">El usuario o la contraseña son incorrectos.</p>
    </form>
  </div>
</template>

<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
  data() {
    return {
      user: "",
      password: "",
      error: false,
      loggedIn: false,
    };
  },
  created() {
  },
  components: {
  },
  watch: {

  },
  methods: {
    funciono() {
      this.$router.push("/main");

      //console.log(this.user + this.password);
    },
    async login() {
      console.log("Intentando loggear al usuario " + user.value + " con password: " + password.value);
      try {
        fetch("https://localhost:44368/Login/" + user.value + "/" + password.value)
          .then((response) => response.json())
          .then((data) => {
            this.loggedIn = data;
            console.log("Respuesta: " + this.loggedIn);
            this.loggedIn ? this.$router.push("/main/"+user.value) : this.error = true;
          })
          .catch(e => {
            console.error(e);
          });
      }
      catch (e) { console.log(e) }
    },
    emitData() {
      this.$emit('getUser', this.user);
    }

  },
});
</script>

<style scoped>
.login {
  padding: 2rem;
}

.title {
  text-align: center;
  padding-left: 10px;
  padding-top: 2px;
  font-size: 30px;
  color: white;


}

.form {
  margin: 3rem auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  width: 20%;
  min-width: 350px;
  max-width: 100%;
  background: rgba(19, 35, 47, 0.9);
  border-radius: 5px;
  padding: 40px;
  box-shadow: 0 4px 10px 4px rgba(0, 0, 0, 0.3);
}

.form-label {
  margin-top: 2rem;
  color: white;
  margin-bottom: 0.5rem;


}

img {
  max-width: 50px;
  max-height: 50px;
}

.error {
  padding-top: 35px;
  color: red;
  font-weight: bold;
}

.form-input {
  padding: 10px 15px;
  background: none;
  background-image: none;
  border: 1px solid white;
  color: white;


}

.form-submit {
  background: rgb(180, 54, 0);
  border: none;
  color: white;
  margin-top: 3rem;
  padding: 1rem 0;
  cursor: pointer;
  transition: background 0.2s;


}

.principal {
  margin: auto;
}

.top-header {
  text-align: center;
}
</style>
