import { replace } from './replace'
import { set, fetch } from './translations'

export default {
    use: function (translations) {
        set(translations);
    },
    install: function (Vue, translations) {

        if (translations)
            set(translations);

        Vue.directive('locale', {
            bind(el, binding, vnode) {
                var locale = vnode.context.$locale
                var translation = fetch(locale, binding.value || binding.expression)
                var translated = replace(translation, {})
                el.innerText = translated
            }
        })

        Vue.directive('locale-html', {
            bind(el, binding, vnode) {
                var locale = vnode.context.$locale
                var translation = fetch(locale, binding.value || binding.expression)
                var translated = replace(translation, {})
                el.innerHTML  = translated
            }
        })

        Vue.mixin({
            computed: {
                $locale: {
                    get: function () {
                        var data = sessionStorage.getItem('vue-locale-current')
                        return data ? data : 'en'
                    },
                    set: function (newValue) {
                        sessionStorage.setItem('vue-locale-current', newValue)
                        window.location.reload(true)
                    }
                }
            },
            methods: {
                t(text) {
                    var translation = fetch(this.$locale, text)
                    return replace(translation, {})
                }
            }
        })
    }
}