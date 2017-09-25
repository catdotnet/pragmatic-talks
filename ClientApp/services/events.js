import $service from './base'

export default {
    next() {
        return $service.get($service.asPath('events', 'next'))
    },
    last() {
        return $service.get($service.asPath('events', 'last'))
    },
    search(page, pageSize, filers, orderBy) {
        return $service.get($service.asPath('events','search') + '?page=' + page + '&pageSize=' + pageSize + '&orderBy=' + orderBy + '&search=' + filers.search)
    },
    create(item) {
        return $service.post('events', item)
    },
    update(item) {
        return $service.put($service.asPath('events', item.id), item)
    },
    delete(item) {
        return $service.delete($service.asPath('events', item.id))
    }
}