import $service from './base'

export default {
    getTop(number) {
        return $service.get('speakers/top?number=' + number)
    },
    search(page, pageSize, filers, orderBy) {
        return $service.get('speakers/search?page=' + page + '&pageSize=' + pageSize + '&orderBy=' + orderBy + '&search=' + filers.search)
    },
    makeAdmin(speaker) {
        return $service.patch('speakers/' + speaker.id + '/makeadmin')
    },
    unmakeAdmin(speaker) {
        return $service.patch('speakers/' + speaker.id + '/unmakeadmin')
    },
    block(speaker) {
        return $service.patch('speakers/' + speaker.id + '/block')
    },
    unblock(speaker) {
        return $service.patch('speakers/' + speaker.id + '/unblock')
    },
    delete(speaker) {
        return $service.delete('speakers/' + speaker.id)
    }
}