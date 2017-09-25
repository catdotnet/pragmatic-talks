import $service from './base'

export default {
    getAll() {
        return $service.get('tags')
    },
    getTop(number) {
        return $service.get($service.asPath('tags', 'top') + '?number=' + number)
    },
    search(page, pageSize, filers, orderBy) {
        return $service.get($service.asPath('tags','search') + '?page=' + page + '&pageSize=' + pageSize + '&orderBy=' + orderBy + '&search=' + filers.search)
    },
    create(item) {
        return $service.post('events', item)
    },
    update(item) {
        return $service.put($service.asPath('tags', item.id), item)
    },
    delete(item) {
        return $service.delete($service.asPath('tags', item.id))
    }
}