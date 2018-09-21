import Vue from 'vue'
import Vuex from 'vuex'
import auth from './modules/auth';
import user from './modules/user';
import category from './modules/category';

import createLogger from 'vuex/dist/logger';

Vue.use(Vuex)
const debug = process.env.NODE_ENV !== 'production'
export default new Vuex.Store({
  strict: debug,
  plugins: debug ? [createLogger()] : [],
  state: {

  },
  mutations: {

  },
  actions: {

  },
  modules: {
    auth: {
      namespaced: true,
      state: auth.state,
      mutations: auth.mutations,
      getters: auth.getters,
      actions: auth.actions,
    },
    user: {
      namespaced: true,
      state: user.state,
      actions: user.actions,
      mutations: user.mutations,
      getters: user.getters,
  },
  category:{
    namespaced: true,
    state: category.state,
    actions: category.actions,
    mutations: category.mutations,
    getters: category.getters
  }
  }
})
