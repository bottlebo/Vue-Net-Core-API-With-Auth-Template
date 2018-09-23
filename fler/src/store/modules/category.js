import Vue from 'vue';
import { categoryService } from '../../services/category.service';

const _categories = [
    { id: 1, Name: "Fashion & Accessories", parentId:null },
    { id: 2, Name: "Jewelry ", parentId: null },
    { id: 3, Name: "Home & Living", parentId: null },

    { id: 4, Name: "Health & Beauty", parentId:null },

    { id: 5, Name: "For her",  parentId: 1 },
    { id: 6, Name: "For him",  parentId: 1 },
    { id: 7, Name: "Baby & kids",  parentId: 1 },
    //
    { id: 8, Name: "Clothing",  parentId: 5 },
    { id: 9, Name: "Bags",  parentId: 5 },
    { id: 10, Name: "Purses",  parentId: 5 },
    { id: 11, Name: "Shoes",  parentId: 5 },
    { id: 12, Name: "Cases",  parentId: 5 },
    //
    { id: 13, Name: "Dresses ",  parentId: 8 },
    { id: 14, Name: "Skirts",  parentId: 8 },
    { id: 15, Name: "Tops",  parentId: 8 },
    { id: 16, Name: "Trousers & Shorts",  parentId: 8 },
    { id: 17, Name: "Jackets & Coats",  parentId: 8 },
    { id: 18, Name: "Others",  parentId: 8 },
]
const state = {
    all:[],
    selectedId:null
};

const getters = {
    categories: state => state.all,
    selectedId: state => state.selectedId,
    top: state => state.all.filter(c=>c.parentId==null)
};

const actions = {
    categoryRequest:async ({commit}) => {
        try{
            let categories = await categoryService.all()
            commit('categoryResult', categories)
        }
        catch(error){

        }
    }
};

const mutations = {
    categoryResult:(categoryState, categories) =>{
        Vue.set(categoryState,'all',categories)
    }

};

export default {
    state,
    getters,
    actions,
    mutations,
};