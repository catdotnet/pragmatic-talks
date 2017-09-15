<template>
    <div id="app">
        <v-app light>
            <v-toolbar fixed>
                <v-toolbar-side-icon @click.native.stop="drawer = !drawer" light></v-toolbar-side-icon>
                <v-toolbar-title v-text="title"></v-toolbar-title>
                <v-spacer></v-spacer>
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
                drawer: true,
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
</style>