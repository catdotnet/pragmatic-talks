import Vue from 'vue'
import Vuex from 'vuex'
import auth from './auth-module'

Vue.use(Vuex)

// TYPES
const MAIN_SET_LOADING = 'MAIN_SET_LOADING'
const MAIN_SET_APIURL = 'MAIN_SET_APIURL'

// STATE
const state = {
    isLoading: false,
    apiUrl : ''
}

// GETTERS
const getters = {
    apiUrl() {
        return state.apiUrl
    }
}

// MUTATIONS
const mutations = {
    [MAIN_SET_LOADING](state, visible) {
        state.isLoading = visible
    },
    [MAIN_SET_APIURL](state, apiUrl) {
        state.apiUrl = apiUrl
    }
}

// ACTIONS
const actions = {
    loading(context, visible) {
        context.commit(MAIN_SET_LOADING, visible);
    }
}

export default new Vuex.Store({
    modules: { auth },
    state,
    getters,
    mutations,
    actions
})


