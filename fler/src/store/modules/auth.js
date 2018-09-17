import { authService } from '../../services/auth.service';
import { EventBus } from '../../event-bus';
//
const state = {
    token: localStorage.getItem('auth-token') || '',
     status: '' 
    };
//
const getters = {
    isAuthenticated: (authState) => !!authState.token,
    authStatus: (authState) => authState.status,
    authToken: (authState) => authState.token,
};
//
const actions = {
    authRequest: ({commit, dispatch}, credentials) => {
        return new Promise((resolve, reject) => {
            commit('authRequest');
            authService.login(credentials)
            .subscribe((result) => {
              localStorage.setItem('auth-token', result); // stash the auth token in localStorage
              commit('authSuccess', result);
              EventBus.$emit('logged-in', null);
              dispatch('user/userRequest', null, { root: true });
              resolve(result);
            },
            (errors) => {
              commit('authError', errors);
              localStorage.removeItem('auth-token');
              reject(errors);
            });
          });
    },
    authLogout: ({commit, dispatch}) => {
        return new Promise((resolve, reject) => {
          commit('authLogout');
          localStorage.removeItem('auth-token');
          resolve();
        });
    }
};
//
const mutations = {
    authRequest: (authState) => {
        authState.status = 'attempting authentication request';
    },
    authSuccess: (authState, authToken) => {
        authState.status = 'authentication succeeded';
        authState.token = authToken;
    },
    authError: (authState) => {
        authState.status = 'error';
    },
    authLogout: (authState) => {
        authState.token = '';
    }
};
//
export default {
    state,
    getters,
    actions,
    mutations,
};