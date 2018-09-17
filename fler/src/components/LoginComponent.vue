<template>
<div>
    <div  v-if="isAuthenticated">
    <q-item>
        <q-item-main :label="profile.firstName" />
        <q-btn flat round dense icon="exit_to_app"  v-on:click="logoff" />
    </q-item>
    
    </div>
    <q-btn flat label="Login"  v-on:click="login"  v-else/>
</div>
</template>

<script>
import { EventBus } from ".././event-bus";
import { mapGetters } from "vuex";

export default {
  name: "Login",
  data() {
    return {};
  },
  computed: {
    ...mapGetters({
      isAuthenticated: "auth/isAuthenticated",
      profile: "user/profile"
    })
  },
  methods: {
    login() {
      this.$router.push("/login");
    },
    logoff() {
      this.$store.dispatch("auth/authLogout").then(() => {
        this.$router.push("/");
      });
    }
  }
};
</script>