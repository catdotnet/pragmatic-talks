<template>
    <div id="admin">
        <v-layout row v-if="isAuthenticated && isAdministrator">
            <v-flex xs12>
                <v-card>
                    
                    <v-card-text>
                        <v-tabs fixed>
                            <v-tabs-bar class="white">
                                <v-tabs-slider class="yellow"></v-tabs-slider>
                                <v-tabs-item v-for="(l, i) in items"
                                             :key="i"
                                             :href="'#tab-' + i">
                                    {{ l }}
                                </v-tabs-item>
                            </v-tabs-bar>
                            <v-tabs-items>
                                <v-tabs-content v-for="(c, i) in components"
                                                :key="i"
                                                :id="'tab-' + i" lazy>
                                    <v-card flat>
                                        <component :is="c"></component>
                                    </v-card>
                                </v-tabs-content>
                            </v-tabs-items>
                        </v-tabs>
                    </v-card-text>              
                </v-card>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
    import { mapState, mapActions } from 'vuex'
    import Talks from './admin/talks'
    import Events from './admin/events'
    import Tags from './admin/tags'
    import Speakers from './admin/speakers'

    export default {
        components: { 'v-talks': Talks, 'v-events': Events, 'v-tags': Tags, 'v-speakers': Speakers },

        data() {
            return {
                items: ['Talks', 'Events', 'Tags', 'Speakers'],
                components: ['v-talks', 'v-events', 'v-tags', 'v-speakers']
            }
        },

        computed: {
            ...mapState({
                isLoading: state => state.isLoading,
                isLogin: state => state.isLogin,
                isAuthenticated: state => state.auth.isAuthenticated,
                isAdministrator: state => state.auth.profile.isAdministrator
            })
        },

        methods: {
            ...mapActions(['loading', 'showLogin'])
        },

        watch: {
            isAuthenticated: {
                handler(after, before) {
                    if (after) this.showLogin(false)
                }
            }
        },

        mounted() {
            this.loading(false)
            if (!this.isAuthenticated) this.showLogin(true)
        }
    }
</script>

<style>

    .tabs:not(.tabs--centered):not(.tabs--grow):not(.tabs--mobile) .tabs__wrapper--scrollable {
        margin-left: 0px;
    }
</style>