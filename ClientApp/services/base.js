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
        return $http.get(this.createPath(path))
    },
    async post(path, data) {
        return $http.post(this.createPath(path), data)
    },
    async put(path, data) {
        return $http.put(this.createPath(path), data)
    },
    async patch(path, data) {
        return $http.patch(this.createPath(path))
    },
    async delete(path) {
        return $http.delete(this.createPath(path))
    },
    createPath(path) {
        let apiUrl = '/api'
        return asPath(apiUrl, path)
    },
    asPath
}