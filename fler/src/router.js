import Vue from 'vue'
import Router from 'vue-router'
import DefaultLayout from './layouts/Default.vue'
import AdminLayout from './layouts/Admin.vue'

import Home from './views/Home.vue'
import About from './views/About.vue'
import LoginForm from './views/account/LoginForm.vue';
import AdminHome from './views/admin/AdminHome.vue'
import store from './store';
Vue.use(Router)

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: DefaultLayout,
      children: [
        {
          path: '',
          name: 'home',
          component: Home
        },
        {
          path: '/about',
          name: 'about',
          component: About,
          meta: { requiresAuth: true }
        },
        {
          path: '/login',
          name: 'loginForm',
          component: LoginForm
        }
      ]
    },
    {
    path:'/admin',
    component: AdminLayout,
    children:[
      {
        path:'/',
        name:'admin',
        component: AdminHome,
        meta: { requiresAuth: true }

      }
    ]
    }
  ]
})
router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    // this route requires auth, check if logged in
    // if not, redirect to login page.
    if (!store.getters['auth/isAuthenticated']) {
      next({
        path: '/login',
        query: { redirect: to.fullPath },
      });
    } else {
      next();
    }
  } else {
    next(); // make sure to always call next()!
  }
});

export default router