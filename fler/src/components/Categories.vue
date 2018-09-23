<template>
<div>
    <div style=" height:49px; line-height:49px;background-color: #F9F3EF">
       <span style="font-weight: 700;font-size: 16px;margin-right: 16px; color:#333">Категории</span>
    </div>
    <div style="position:relative">
        <q-list highlight>
            <div  v-for="cat in top" :key="cat.Id" style="" class="over" @mouseover="catehoryHover(cat.id)" @mouseout="categoryOut()">
                <q-item style="pointer-events: none;" >
                    <q-item-main :label="cat.name"  style="pointer-events: none;" />
                </q-item>
            </div>
        </q-list>
    <!-- {{$route.params.category}} -->
    <transition  name="fade" mode="out-in">
    <div v-show="popShow"  @mouseover="popOver()" @mouseout="popOut()" style="position:absolute; left:calc(100% + 5px); top:-10px; padding:10px; z-index:1000; width:850px; background-color:#F9F3EF; border:solid 1px #ccc; min-height:300px">
        <div class="rowa"  @mouseover="popShow=true" style="">
            <div class="cola" v-for="cat in categoryPopover()" :key="cat.id"  @mouseover="popShow=true">
                <q-list  dense   @mouseover="popShow=true">
                    <q-list-header   @mouseover="popShow=true"><strong>{{cat.name}}</strong></q-list-header>
                    <!-- no child -->
                    <div class="row">
                    <q-item dense  v-if="hasChild(child.id)" v-for="child in childCatWidthChild(cat.id)" :key="child.id"  @mouseover="popShow=true"  class="col-4 vertical-top" style="border:solid 0px #000">
                        
                        
                        <q-list no-border dense style="align-self: flex-start;">
                            <q-list-header>{{child.name}}</q-list-header>
                            <q-item dense v-for="cc in childCat(child.id)" :key="cc.id">
                                <router-link v-bind:to="'/category/'+cc.route"  @mouseover="popShow=true" @click.native="popShow=false">{{cc.name}}</router-link>
                            </q-item>
                        </q-list>
                       
                        
                        
                    </q-item>
                    <q-item dense   v-if="hasNoChild(cat.id)"  class="col-4 vertical-top" style="border:solid 0px red">
                        <q-list no-border dense  style="align-self: flex-start;">
                            <q-item dense  v-for="child in childCatWidthoutChild(cat.id)" :key="child.id">
                                <q-item-main>
                                 <router-link v-bind:to="'/category/'+child.route"  @mouseover="popShow=true" @click.native="popShow=false">{{child.name}}</router-link>
                                </q-item-main>
                            </q-item>
                        </q-list>
                    </q-item>
                    </div>
                </q-list>
                
            </div>
        </div>
    </div>
    </transition>
    </div>
</div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Categories",
  data() {
    return {
        hoveredCatId:0,
        catHover:false,
        popHover:false,
        popShow:false
    };
  },
  computed: {
    ...mapGetters({
        top: "category/top",
        all: "category/categories"
    })
  },
  methods: {
    hasChild(id){
        let _parent = this.all.filter(c=>c.id == id)[0]
        //console.log(_parent)
        return this.all.filter(c=>c.parentId == _parent.id).length > 0
    },
    hasNoChild(id){
        let _parent = this.all.filter(c=>c.id == id)[0]
        //console.log(_parent)
        let child = this.all.filter(c=>c.parentId == _parent.id)
        for(let i in child){
            if(this.hasChild(child[i].id)) return true
        }
        return false
    },
    catehoryHover(id){
        //console.log('***'+id)
        setTimeout(()=>{
        this.popShow = true
        this.popHover=false
        this.hoveredCatId = id
        },300)
    },
    categoryOut(){
        //console.log(this.popHover)
        
        setTimeout(()=>{
            if(!this.popHover)
            {
                this.popShow=false
            }
                },270)
                
        //})
    },
    categoryPopover(){
        return this.all.filter(c=>c.parentId == this.hoveredCatId)
    },
    childCat(parentId){
        return this.all.filter(c=>c.parentId == parentId )
    },
    childCatWidthChild(parentId){
        return this.all.filter(c=>c.parentId == parentId && this.hasChild(c.id))

    },
     childCatWidthoutChild(parentId){
        return this.all.filter(c=>c.parentId == parentId && !this.hasChild(c.id))

    },
    popOver(){
        //console.log('popOver')
        this.popHover=true
        this.popShow=true
    },
    popOut(){
        console.log('popOut')
        this.popHover = false
        this.popShow=false
    }
  },
}
</script>
<style scoped>
.over {cursor: pointer; margin-bottom:0px}
.over:hover{
     background-color: #6088f8c5;
     /*color:#0a3253*/
}
</style>
