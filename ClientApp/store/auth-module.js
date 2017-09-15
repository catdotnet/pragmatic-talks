import $http from 'axios'

// TYPES
const MAIN_SET_AUTH = 'MAIN_SET_AUTH'

// STATE
const state = {
    isAuthenticated: false,
    userName: '',
    userId: '',
    profile: {},
    permissions: []
}

// GETTERS
const getters = {
    isAuthenticated() {
        return state.isAuthenticated
    },
    userName() {
        return state.userName
    },
    userId() {
        return state.userId
    },
    profile() {
        return state.profile
    },
    permissions() {
        return state.permissions
    }
}
// MUTATIONS
const mutations = {
    [MAIN_SET_AUTH](state, payload) {
        state.isAuthenticated = payload.isAuthenticated
        state.userName = payload.userName
        state.userId = payload.userId
        state.profile = payload.profile
        state.permissions = payload.permissions
    }
}

// ACTIONS
const actions = {
   
}

export default {
    state,
    getters,
    mutations,
    actions
}


