<template>
 <!--  <q-page class="flex flex-center">
    <img alt="Quasar logo" src="../assets/logo.png">
  </q-page> -->
  <q-page padding>
     <div class="row">
        <div class="col-5">
        <q-search v-model="search" />
        </div>
        <div class="col">
          <q-btn label="Test" @click="$store.dispatch('user/userRequest', null, { root: true })" color="primary" />
        </div>
        </div>
        
  </q-page>
</template>

<style>
</style>

<script>
export default {
  name: 'PageHome',
  data(){
    return {
      search:''
    }
  }
}
</script>
<style>

</style>
