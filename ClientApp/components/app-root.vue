<template>
    <div id="app">
        <v-app>
            <v-navigation-pragmatic :items="menuOptions" :miniVariant="miniVariant" :drawer="drawer" @show-change="navigationChanged"></v-navigation-pragmatic>

            <v-toolbar-pragmatic :title="title" :items="menuOptions" :languages="languages" @show-menu="changeNavigation"></v-toolbar-pragmatic>

            <main>
                <v-container fluid>
                    <v-slide-y-transition mode="out-in">
                        <router-view></router-view>
                    </v-slide-y-transition>
                </v-container>
            </main>

        
        </v-app>

        <v-spinner :visible="isLoading"></v-spinner>

        <v-login-dialog :visible="isLogin" @close="showLogin(false)"></v-login-dialog>

        <v-error-dialog></v-error-dialog>

        <v-event-dialog></v-event-dialog>
    </div>
</template>

<script>
    import { mapState, mapActions } from 'vuex'
    import Vue from 'vue'
    import axios from 'axios'
    import Spinner from './layout/spinner'
    import Navigation from './layout/navigation'
    import Toolbar from './layout/toolbar'
    import Login from './dialogs/login-dialog'
    import Error from './dialogs/error-dialog'
    import Event from './dialogs/event-dialog'
    import service from '../services/me'
    import Vuetify from 'vuetify'

    Vue.use(Vuetify)

    export default {

        components: {
            'v-spinner': Spinner,
            'v-navigation-pragmatic': Navigation,
            'v-toolbar-pragmatic': Toolbar,
            'v-login-dialog': Login,
            'v-error-dialog': Error,
            'v-event-dialog': Event
        },

        data() {
            return {
                drawer: false,
                miniVariant: false,
                title: 'Pragmatic Talks',
                menuOptions: [
                    { title: 'Talks', to: { name: 'talks' }, icon: 'record_voice_over' },
                    { title: 'Rules', to: { name: 'rules' }, icon: 'format_list_numbered' },
                    { title: 'Events', to: { name: 'events' }, icon: 'event' },
                ],
                languages: [
                    { title: 'English', key: 'en', icon: 'flag-us' },
                    { title: 'Spanish', key: 'es', icon: 'flag-es' },
                    { title: 'Catalan', key: 'ca', icon: 'flag-catalonia' },
                ]
            }
        },

        computed: {
            ...mapState({
                isLoading: state => state.isLoading,
                isLogin: state => state.isLogin,
            })
        },

        methods: {
            ...mapActions(['showLogin', 'loading', 'error']),
            ...mapActions(['authenticate'], 'auth'),
            navigationChanged(value) {
                this.drawer = value;
            },
            changeNavigation() {
                this.drawer = !this.drawer
            }
        },

        mounted() {
            this.loading(true)
            service.get().then(response => {
                this.authenticate(response.data)
                this.loading(false)
            }).catch(error => {
                this.loading(false)
            })

            axios.interceptors.response.use(null, error => {
                if (!error.response) return Promise.reject(error)

                this.error(error.response && error.response.data && error.response.data.message ? error.response.data.message : error.status + ' ' + error.message )
                return Promise.reject(error);
            });
        }
    }
</script>

<style>
    .application .theme--dark.toolbar:before {
        background-image: url(/images/background.png);
        position: absolute;
        height: 100%;
        width: 100%;
        display: block;
        top: 0;
        left: 0;
        background-size: cover;
        background-position: center center;
        opacity: 0.33;
        content: " ";
    }

    .footer {
        bottom: 0;
        width: 100%;
        margin-top: 40px;
    }
</style>