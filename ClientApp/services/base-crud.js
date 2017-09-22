import $service from './base'

export default function (path) {
    return {
        getAll() {
            return $service.get(path)
        },
        create(item) {
            return $service.post(path, item)
        },
        update(item) {
            return $service.put($service.asPath(path, item.id), item)
        },
        delete(item) {
            return $service.delete($service.asPath(path, item.id))
        }
    }
}