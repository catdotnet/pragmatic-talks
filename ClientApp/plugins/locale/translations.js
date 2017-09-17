import Vue from 'vue'

const locale_translations = {
    /*
    'es': {
      'hello': 'hola'
    }
    */
}

export var set = function (translations) {
    Object.keys(translations).forEach(function (locale) {
        locale_translations[locale] = translations[locale]
    })
}

export var fetch = function (locale, key) {
    if (!locale) return key;

    var translations = locale_translations[locale]

    if (translations && key in translations)
        return translations[key];

    if (locale.indexOf('_') > -1)
        return fetch(locale.substr(0, locale.indexOf('_')), key)

    if (locale.indexOf('-') > -1)
        return fetch(locale.substr(0, locale.indexOf('-')), key)

    if (translations && window.console && Vue.config.debug)
        console.warn(`[vue-localize] Translations exist for the locale '${locale}', but there is not an entry for '${key}'`)

    return key
}