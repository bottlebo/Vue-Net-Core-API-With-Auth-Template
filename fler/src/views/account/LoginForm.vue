<template>
    <section class="section">
      <!-- <article class="message is-success" v-if="$route.query.new">
        <div class="message-body">
            <strong>You're all set {{$route.query.firstName}}!</strong> Login with your password to continue.
        </div>
      </article> -->
      <div class="box">    
        {{$route.query.redirect}}
        <form @submit.prevent="handleSubmit">
        <q-card class="shadow-6">
          <q-card-title>
            Login
            <span slot="subtitle">Please login to proceed</span>
          </q-card-title>
          <q-card-separator />
          <q-card-main>

              <q-input float-label="Login"  v-model="credentials.userName" type="text" />
              <q-input float-label="Password" type="password"  v-model="credentials.password"/>
              
              
              <!-- <Spinner v-bind:show="isBusy" /> -->
              
              <div class="errors-container">
              <div v-show="errors">
                 {{errors}}
              </div>
            </div>
            </q-card-main>
            <q-card-separator />
            <q-card-actions align="end">
                <q-btn label="Login" type="submit" color="primary"/>
                <q-btn label="Sign Up" @click="$router.push('/register')" flat  color="primary"/>
            </q-card-actions>
          </q-card>      
           </form>
          </div>
         <!--  <p class="has-text-grey">
             <router-link to="/register">Sign Up</router-link>
          </p> -->
        
  </section> 
</template>
<script>
export default {
  data() {
    return {
      isBusy: false,
      errors: "",
      credentials: {}
    };
  },
  methods: {
    handleSubmit() {
      this.isBusy = true;
      this.$store
        .dispatch("auth/authRequest", this.credentials)
        .then(result => {
          //console.log(this.$route.query.redirect)
          this.$router.push(this.$route.query.redirect?this.$route.query.redirect : "/");
        })
        .catch(err => {
          this.errors = err;
        })
        .then(() => {
          this.isBusy = false;
        });
    }
  },
  created() {
    if (this.$route.query.new) {
      this.credentials.userName = this.$route.query.email;
    }
  }
};
</script>
<style>
.box {
  width: 400px;
  margin: 100px auto;
}
.errors-container{
  line-height: 30px;
  height: 30px;
  padding-top: 7px;
}
</style>