<template>
<div>
    <div style=" height:49px; line-height:49px;background-color: #F9F3EF">
       <span style="font-weight: 700;font-size: 16px;margin-right: 16px; color:#333">Категории</span>
    </div>
    <div>
        <q-list highlight>
            <div  v-for="cat in top" :key="cat.Id" style="" class="over" @mouseover="catehoryHover(cat.id)">
                <q-item style="pointer-events: none;" >
                    <q-item-main :label="cat.Name"  style="pointer-events: none;" />
                </q-item>
            </div>
        </q-list>
    </div>
</div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Categories",
  data() {
    return {};
  },
  computed: {
    ...mapGetters({
        top: "category/top"
    })
  },
  methods: {
    catehoryHover(id){
        console.log('***'+id)
    }
  },
}
</script>
<style scoped>
.over {cursor: pointer;}
.over:hover{
     background-color: rgba(85, 139, 238, 0.5);
}
</style>
