<template>
    <div id="app">
        <v-app>
            <v-navigation-drawer temporary 
                                 :mini-variant="miniVariant"
                                 clipped
                                 overflow
                                 v-model="drawer"
                                 enable-resize-watcher>
                <v-list>
                    <v-list-tile avatar tag="div">
                        <v-list-tile-avatar>
                            <v-icon>record_voice_over</v-icon>
                        </v-list-tile-avatar>
                        <v-list-tile-content>
                            <v-list-tile-title>Pragmatic Talks</v-list-tile-title>
                        </v-list-tile-content>
                    </v-list-tile>

                    <router-link class="router-link" :to="{ name: 'ranking' }" tag="li">
                        <a class="list__tile list__tile--active">
                            <v-list-tile-action>
                                <v-icon>format_list_numbered</v-icon>
                            </v-list-tile-action>
                            <v-list-tile-content>
                                <v-list-tile-title v-locale="'Talks'"></v-list-tile-title>
                            </v-list-tile-content>
                        </a>
                    </router-link>
                    <router-link class="router-link" :to="{ name: 'rules' }" tag="li">
                        <a class="list__tile list__tile--active">
                            <v-list-tile-action>
                                <v-icon>format_list_bulleted</v-icon>
                            </v-list-tile-action>
                            <v-list-tile-content>
                                <v-list-tile-title v-locale="'Rules'"></v-list-tile-title>
                            </v-list-tile-content>
                        </a>
                    </router-link>
                    <router-link class="router-link" :to="{ name: 'events' }" tag="li">
                        <a class="list__tile list__tile--active">
                            <v-list-tile-action>
                                <v-icon>event</v-icon>
                            </v-list-tile-action>
                            <v-list-tile-content>
                                <v-list-tile-title v-locale="'Events'"></v-list-tile-title>
                            </v-list-tile-content>
                        </a>
                    </router-link>
                </v-list>
            </v-navigation-drawer>
            <v-toolbar fixed dark class="primary">
                <v-toolbar-side-icon @click.native.stop="drawer = !drawer" dark></v-toolbar-side-icon>
                <v-toolbar-title v-text="title"></v-toolbar-title>
                <v-spacer></v-spacer>
                <v-menu>
                    <v-btn icon slot="activator">
                        <v-icon>flag</v-icon>
                    </v-btn>
                    <v-list class="flags">
                        <v-list-tile @click.prevent="setLanguage('en')">
                            <v-list-tile-title v-locale="'English'"></v-list-tile-title>
                            <v-list-tile-action><v-icon>flag-us</v-icon></v-list-tile-action>
                        </v-list-tile>
                        <v-list-tile @click.prevent="setLanguage('es')">
                            <v-list-tile-title v-locale="'Spanish'"></v-list-tile-title>
                            <v-list-tile-action><v-icon>flag-es</v-icon></v-list-tile-action>
                        </v-list-tile>
                        <v-list-tile @click.prevent="setLanguage('ca')">
                            <v-list-tile-title v-locale="'Catalan'"></v-list-tile-title>
                            <v-list-tile-action><v-icon>flag-catalonia</v-icon></v-list-tile-action>
                        </v-list-tile>
                    </v-list>
                </v-menu>
                <v-menu left>
                    <v-btn icon slot="activator">
                        <v-icon>more_vert</v-icon>
                    </v-btn>
                    <v-list>
                        <v-list-tile>
                            <v-list-tile-action><v-icon>person</v-icon></v-list-tile-action>
                            <v-list-tile-title v-text="userName"></v-list-tile-title>
                        </v-list-tile>
                        <v-list-tile>
                            <v-list-tile-action><v-icon>stop</v-icon></v-list-tile-action>
                            <v-list-tile-title>Logout</v-list-tile-title>
                        </v-list-tile>
                    </v-list>
                </v-menu>
                <v-btn flat slot="extension" route :to="{ name: 'ranking' }">
                    <v-icon left dark>format_list_numbered</v-icon>
                    <span v-locale="'Talks'"></span>
                </v-btn>
                <v-btn flat slot="extension" route :to="{ name: 'rules' }">
                    <v-icon left dark>format_list_bulleted</v-icon>
                    <span v-locale="'Rules'"></span>
                </v-btn>
                <v-btn flat slot="extension">
                    <v-icon left dark>event</v-icon>
                    <span v-locale="'Events'"></span>
                </v-btn>
            </v-toolbar>
            <main>
                <v-container fluid>
                    <v-slide-y-transition mode="out-in">
                        <router-view></router-view>
                    </v-slide-y-transition>
                </v-container>
            </main>

            <v-footer right>
                <span>&copy; 2017</span>
            </v-footer>
        </v-app>

        <v-spinner :visible="isLoading"></v-spinner>
    </div>
</template>

<script>
    import { mapState } from 'vuex'
    import Vue from 'vue'
    import Spinner from './layout/spinner'
    import Vuetify from 'vuetify'

    Vue.use(Vuetify)

    export default {

        components: {
            'v-spinner': Spinner
        },

        data() {
            return {
                drawer: false,
                miniVariant: false,
                title: 'Pragmatic Talks',
                nextRoute: null
            }
        },

        computed: {
            ...mapState({
                isLoading: state => state.isLoading,
                isAuthenticated: state => state.auth.isAuthenticated,
                userName: state => state.auth.profile.name
            })
        },

        methods: {
            setLanguage(value) {
                this.$locale = value
            }
        }
    }
</script>

<style>
    .application--light {
        background-color: rgb(240, 240, 240);
    }

        .application--light .toolbar {
            background-color: #fff;
        }

    .footer {
        bottom: 0;
        width: 100%;
        margin-top: 40px;
    }

    .toolbar .toolbar__content > .toolbar__side-icon {
        display: none;
    }


    @media only screen and (max-width: 599px) {
        .toolbar .toolbar__content > .toolbar__side-icon {
            display: block;
        }

        .toolbar .toolbar__extension {
            display: none;
            height: 0px;
            overflow-y: hidden;
        }

        .toolbar--fixed.toolbar--extended + main {
            padding-top: 44px;
        }
    }

    li.router-link {
        list-style-type: none;
    }

    .list.flags li {
        cursor: pointer;
    }

    .list.flags .list__tile__action {
        padding-left: 5px;
        min-width: 32px;
    }
</style>