<template>
 <!--  <q-page class="flex flex-center">
    <img alt="Quasar logo" src="../assets/logo.png">
  </q-page> -->
  <q-page padding   class="row justify-center">
    <div style="max-width:1200px; margin:0 auto; width:100%">



 
 <div class="row">
   <div class="col-1" style="width:240px;">
      <div>
          <img alt="Quasar logo" src="../assets/logo.png">
          <div style="color:#777; font-size:12px;font-weight: 500; letter-spacing:0.265em">handi made market</div>
      </div>
   </div>
   <div class="col"  style="max-width:900px; min-width:400px">
     <q-search v-model="search" />
   </div>
   <div class="col" style="max-width:200px">
     <div class="float-right"><q-btn flat round dense icon="more_vert" /></div>
   </div>
 </div>
     <div class="row" style="padding-top:10px">
        <div class="col-1" style="width:240px;">
     
       <categories/>
        </div>
        <div class="col">
          <div style=" height:49px; line-height:49px;background-color: #F9F3EF"></div>
          <q-btn label="Test" @click="$store.dispatch('user/userRequest', null, { root: true })" color="primary" />
        </div>
        <!-- <div class="col-1" style="background-color:#555; width:240px;">1</div> -->
        </div>
    </div>
  </q-page>
</template>

<style>
</style>

<script>
import Categories from "../components/Categories"
export default {
  name: 'PageHome',
  data(){
    return {
      search:''
    }
  },
  components: {
    Categories
  }
}
</script>
<style>

</style>
