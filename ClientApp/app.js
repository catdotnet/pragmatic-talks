import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from 'store/store'
import { sync } from 'vuex-router-sync'
import locale from 'plugins/locale/vue-locale'
import translations from 'translations/all'
import App from 'components/app-root'

Vue.prototype.$http = axios

locale.use(translations)
Vue.use(locale)

sync(store, router)

const app = new Vue({
    store,
    router,
    ...App
})

export {
    app,
    router,
    store
}
