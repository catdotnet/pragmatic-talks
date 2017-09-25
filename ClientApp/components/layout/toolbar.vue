<template>
    <v-toolbar fixed dark class="primary">
        <v-toolbar-side-icon @click.native.stop="$emit('show-menu')" dark></v-toolbar-side-icon>
        <v-toolbar-title>
            <v-btn class="toolbar-title" flat :to="{ name: 'home' }">
                <v-icon dark class="not-mobile">forum</v-icon>
                {{ title }}
            </v-btn>
        </v-toolbar-title>

        <v-spacer></v-spacer>
        <v-menu>
            <v-btn icon slot="activator">
                <v-icon>flag</v-icon>
            </v-btn>
            <v-list class="flags">
                <v-list-tile v-for="(item, index) in languages" :key="index" @click.prevent="setLanguage(item.key)">
                    <v-list-tile-title v-locale="item.title"></v-list-tile-title>
                    <v-list-tile-action><v-icon>{{ item.icon }}</v-icon></v-list-tile-action>
                </v-list-tile>
            </v-list>
        </v-menu>
        <v-toolbar-items class="hidden-sm-and-down">
            <v-menu left class="not-mobile" v-if="isAuthenticated">
                <v-btn flat slot="activator">
                    {{ userName }}
                </v-btn>
                <v-list>
                    <v-list-tile @click="logout">
                        <v-list-tile-action><v-icon>stop</v-icon></v-list-tile-action>
                        <v-list-tile-title>Logout</v-list-tile-title>
                    </v-list-tile>
                </v-list>
            </v-menu>
            <v-btn flat class="not-mobile" v-else @click="showLogin(true)">Login</v-btn>
        </v-toolbar-items>
        <v-menu left class="only-mobile">
            <v-btn icon slot="activator">
                <v-icon>more_vert</v-icon>
            </v-btn>
            <v-list v-if="isAuthenticated">
                <v-list-tile>
                    <v-list-tile-action><v-icon>person</v-icon></v-list-tile-action>
                    <v-list-tile-title v-text="userName"></v-list-tile-title>
                </v-list-tile>
                <v-list-tile @click="logout">
                    <v-list-tile-action><v-icon>stop</v-icon></v-list-tile-action>
                    <v-list-tile-title>Logout</v-list-tile-title>
                </v-list-tile>
            </v-list>
            <v-list v-else>
                <v-list-tile @click="showLogin(true)">
                    <v-list-tile-action><v-icon>person</v-icon></v-list-tile-action>
                    <v-list-tile-title>Login</v-list-tile-title>
                </v-list-tile>
            </v-list>
        </v-menu>
        <v-btn v-for="(item, index) in items" :key="index" flat slot="extension" route :to="item.to">
            <v-icon left dark>{{ item.icon }}</v-icon>
            <span v-locale="item.title"></span>
        </v-btn>
    </v-toolbar>
</template>

<script>
    import { mapState, mapActions } from 'vuex'

    export default {
        props: {
            title: {
                type: String,
                required: true
            },
            items: {
                type: Array,
                required: true
            },
            languages: {
                type: Array,
                required: true
            }
        },

        data() {
            return {
            }
        },

        computed: {
            ...mapState({
                isAuthenticated: state => state.auth.isAuthenticated,
                userName: state => state.auth.userName
            })
        },

        methods: {
            ...mapActions(['showLogin']),
            setLanguage(value) {
                this.$locale = value
            },
            logout() {
                window.location.href = '/Account/Logout'
            }
        }
    }
</script>

<style>
    .toolbar .toolbar__content > .toolbar__side-icon {
        display: none;
    }

    .toolbar-title,
    .toolbar-title > .btn__content,
    .toolbar-title.btn--active .btn__content:before,
    .toolbar-title.btn:focus .btn__content:before,
    .toolbar-title.btn:hover .btn__content:before {
        background-color: transparent !important;
        transition: none !important;
        border-radius: 0px !important;
        margin: 0 !important;
        padding: 0 !important;
        font-size: 20px;
        font-weight: 500;
        letter-spacing: .02em;
        margin-left: 16px;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }

    .toolbar-title .icon {
        margin-right: 14px;
    }

    @media only screen and (max-width: 599px) {
        .toolbar__title {
            margin-left: 0px !important;
        }
        
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