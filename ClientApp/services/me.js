import $service from './base'

export default {
    get() {
        return $service.get('me')
    }
}