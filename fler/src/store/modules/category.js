import Vue from 'vue';

const _categories = [
    { id: 1, Name: "Телевизоры", hasChild: true, companyId: 1 },
    { id: 2, Name: "Телевизоры LED", hasChild: false, parentID: 1, companyId: 1 },
    { id: 3, Name: "Телевизоры PLAZMA", hasChild: false, parentID: 1, companyId: 1 },
    //
    { id: 4, Name: "Кофеварки", hasChild: true, companyId: 2 },
    { id: 5, Name: "Esspresso", hasChild: false, parentID: 4, companyId: 2 },
]
const state = {
    all:[],
    selectedId:null
};

const getters = {
    categories: state => state.all,
    selectedId: state => state.selectedId
};

const actions = {
    categoryRequest:({commit}) => {
        commit('categoryResult', _categories)
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