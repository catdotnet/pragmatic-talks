import $service from './base'

export default {
    get() {
        return $service.get('talks')
    },
    search(page, pageSize, filers, orderBy, onlyOpened) {
        return $service.get('talks/search?page=' + page + '&pageSize=' + pageSize + '&orderBy=' + orderBy + '&search=' + filers.search + '&onlyOpened=' + filers.onlyOpened)
    },
    post(talk) {
        return $service.post('talks', talk)
    },
    select(talk) {
        return $service.patch('talks/' + talk.id + '/selected')
    },
    delete(talk) {
        return $service.delete('talks/' + talk.id)
    }
}