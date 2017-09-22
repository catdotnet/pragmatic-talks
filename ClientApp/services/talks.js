import $service from './base'

export default {
    get() {
        return $service.get('talks')
    },
    search(page, pageSize, orderBy, search, onlyOpened) {
        return $service.get('talks?page=' + page + '&pageSize=' + pageSize + '&orderBy=' + orderBy + '&search=' + search + '&onlyOpened=' + onlyOpened)
    },
    post(talk) {
        return $service.post('talks', talk)
    },
    select(id) {
        return $service.patch('talks/' + id + '/selected')
    },
    delete(id) {
        return $service.delete('talks/' + id)
    }
}