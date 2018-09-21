<template>
<div>
    <router-view />
</div>
</template><template>
  <q-layout view="hHh Lpr lFf">
    <q-layout-header>
      <q-toolbar
        color="primary"
        :glossy="!$q.theme === 'mat'"
        :inverted="$q.theme === 'ios'"
      >
        <q-btn
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen"
          aria-label="Menu"
          icon="menu"
        />

        <q-toolbar-title >
          <div>Quasar App</div>
          <div slot="subtitle">Running on Quasar v{{ $q.version }}</div>
          
        </q-toolbar-title>
        
        <div>123</div>
        <login></login>
      </q-toolbar>
    </q-layout-header>

  

    <q-page-container>
     <router-link to="/" >Home</router-link>&nbsp;
     <router-link to="/about" >About</router-link>
      <transition  name="fade" mode="out-in">
      <router-view />
      </transition>

    </q-page-container>
  </q-layout>
</template>

<script>
import { mapGetters } from "vuex";
import { adminService } from "../services/admin.service";
import Login from "../components/LoginComponent";
export default {
  name: "LayoutAdmin",
  data() {
    return {
      //leftDrawerOpen: this.$q.platform.is.desktop,
      //search:''
    };
  },
  created() {
       adminService.profile()
      .subscribe((result) => {
          this.$store.dispatch('user/set', result)
          //commit('userSuccess', result);
        },
      (errors) => {
          //console.log(errors)
        //commit('userError');
        //dispatch('auth/authLogout', null, { root: true });
      }); 
  },
  components: {
    Login
  }
}
</script>