<template>
<div>
    <div class="row">
        <div class="col"  v-for="n in 40" :key="n" style="max-width: 200px; min-width:150px; margin:0 10px" >
      <q-card>
{{$route.params.category}}
      </q-card>
      </div>
    </div>
</div>
</template>

<script>
export default {
  name: "Products",
  data() {
    return {}
  },
  computed:{
      selectedCategory:function(){return this.$route.params.category}
  },
   watch: {
       selectedCategory:function(){
           console.log(this.selectedCategory)
       }
   }
}
</script>