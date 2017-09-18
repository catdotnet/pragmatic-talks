import $http from 'axios'

// TYPES
const MAIN_SET_AUTH = 'MAIN_SET_AUTH'

// STATE
const state = {
    isAuthenticated: false,
    userName: '',
    profile: {}
}

// GETTERS
const getters = {
    isAuthenticated() {
        return state.isAuthenticated
    },
    userName() {
        return state.userName
    },
    profile() {
        return state.profile
    }
}
// MUTATIONS
const mutations = {
    [MAIN_SET_AUTH](state, payload) {
        state.isAuthenticated = payload.isAuthenticated
        state.userName = payload.userName
        state.profile = payload.profile
    }
}

// ACTIONS
const actions = {
    authenticate(context, data) {
        debugger
        context.commit(MAIN_SET_AUTH, { isAuthenticated: true, userName: data.displayName, profile: data })
    },
}

export default {
    state,
    getters,
    mutations,
    actions
}


