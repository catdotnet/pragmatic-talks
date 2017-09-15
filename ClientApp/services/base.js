import $store from '../store/store'
import $auth from './auth'
import $http from 'axios'

var asPath = function(...args) {
    var paths = []
    args.forEach(arg => {
        arg = arg.toString()
        if (arg.startsWith('/'))
            arg = arg.substr(1, arg.length - 1)
        if (arg.endsWith('/'))
            arg = arg.substr(0, arg.length - 1)

        paths.push(arg)
    })

    return paths.join('/')
}


export default {
    async get(path) {
        var tokenHeader = await $auth.getTokenHeaders()
        return $http.get(this.createPath(path), tokenHeader)
    },
    async post(path, data) {
        var tokenHeader = await $auth.getTokenHeaders()
        return $http.post(this.createPath(path), data, tokenHeader)
    },
    async put(path, data) {
        var tokenHeader = await $auth.getTokenHeaders()
        return $http.put(this.createPath(path), data, tokenHeader)
    },
    async patch(path, data) {
        var tokenHeader = await $auth.getTokenHeaders()
        return $http.patch(this.createPath(path), tokenHeader)
    },
    async delete(path) {
        var tokenHeader = await $auth.getTokenHeaders()
        return $http.delete(this.createPath(path), tokenHeader)
    },
    createPath(path) {
        let apiUrl = $store.getters.apiUrl
        return asPath(apiUrl, path)
    },
    asPath
}