import $service from './base'

export default {
    get() {
        return $service.get('talks')
    },
    search(page, pageSize, filers, orderBy, onlyOpened) {
        return $service.get('talks/search?page=' + page + '&pageSize=' + pageSize + '&orderBy=' + orderBy + '&search=' + filers.search + '&onlyOpened=' + true)
    },
    post(talk) {
        return $service.post('talks', talk)
    },
    select(talk) {
        return $service.patch('talks/' + talk.id + '/selected')
    },
    unselect(talk) {
        return $service.patch('talks/' + talk.id + '/unselected')
    },
    delete(talk) {
        return $service.delete('talks/' + talk.id)
    }
}