﻿import Vue from 'vue'
import Vuex from 'vuex'
import auth from './auth-module'

Vue.use(Vuex)

// TYPES
const MAIN_SET_LOADING = 'MAIN_SET_LOADING'
const MAIN_SET_LOGIN = 'MAIN_SET_LOGIN'
const MAIN_SET_ERROR = 'MAIN_SET_ERROR'
const MAIN_SET_EVENT = 'MAIN_SET_EVENT'

// STATE
const state = {
    isLoading: false,
    isLogin: false,
    errorMessage: '',
    event: null
}

// GETTERS
const getters = {
   
}

// MUTATIONS
const mutations = {
    [MAIN_SET_LOADING](state, visible) {
        state.isLoading = visible
    },
    [MAIN_SET_LOGIN](state, visible) {
        state.isLogin = visible
    },
    [MAIN_SET_ERROR](state, message) {
        state.errorMessage = message
    },
    [MAIN_SET_EVENT](state, event) {
        state.event = event
    }
}

// ACTIONS
const actions = {
    loading(context, visible) {
        context.commit(MAIN_SET_LOADING, visible)
    },
    showLogin(context, visible) {
        context.commit(MAIN_SET_LOGIN, visible)
    },
    error(context, message) {
        context.commit(MAIN_SET_ERROR, message)
    },
    setEvent(context, event) {
        context.commit(MAIN_SET_EVENT, event)
    }
}

export default new Vuex.Store({
    modules: { auth },
    state,
    getters,
    mutations,
    actions
})


