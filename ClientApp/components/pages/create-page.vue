<template>
    <div id="create" class="page">

        <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-card v-if="isAuthenticated">
                    <v-form v-model="valid" ref="form">
                        <v-card-title>
                            <span class="headline" v-locale="'Create new talk'"></span>
                        </v-card-title>
                        <v-card-text>
                            <v-text-field :label="t('Title')" v-model="title" :counter="37" :rules="titleRules" required></v-text-field>
                            <div class="space"></div>
                            <v-select v-model="tags" :label="t('Tags')" :rules="tagsRules" multiple tags :items="tagItems"></v-select>
                            <div class="space"></div>
                            <v-select v-model="language" item-text="title" item-value="key" :label="t('Language')" :rules="languageRules" :items="languageItems">
                                <template slot="item" scope="data">
                                    <v-list-tile-action>
                                        <v-icon>{{ data.item.icon }}</v-icon>
                                    </v-list-tile-action>
                                    <v-list-tile-content>
                                        <span>{{ data.item.title }}</span>
                                    </v-list-tile-content>
                                </template>
                            </v-select>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn :class="{ 'green--text': valid, 'red--text': !valid, 'darken-1': true }" flat @click="create" v-locale="'Send'"></v-btn>
                            <v-btn floating fab dark router to="/" primary absolute top right>
                                <v-icon>chevron_left</v-icon>
                            </v-btn>
                        </v-card-actions>
                    </v-form>
                </v-card>
                <v-card v-else>
                    <v-card-title>
                        <span class="headline" v-locale="'Create new talk'"></span>
                    </v-card-title>
                    <v-card-text>
                        <v-alert error value="true">
                            <span v-locale="'can not create talks'"></span>
                        </v-alert>
                    </v-card-text>
                </v-card>
            </v-flex>
        </v-layout>

        <v-talk-added-dialog :visible="addedDialogVisible" @close="addedDialogVisible = false"></v-talk-added-dialog>
    </div>
</template>

<script>
    import { mapState, mapActions } from 'vuex'
    import service from '../../services/talks'
    import tagsService from '../../services/tags'
    import TalkAddedDialog from '../dialogs/talk-added-dialog'

    export default {
        components: { 'v-talk-added-dialog': TalkAddedDialog },

        data() {
            return {
                valid: false,
                title: '',
                tags: [],
                language: '',
                titleRules: [],
                tagsRules: [],
                languageRules: [],
                tagItems: [],
                languageItems: [],
                addedDialogVisible: false
            }
        },

        computed: {
            ...mapState({
                isLoading: state => state.isLoading,
                isLogin: state => state.isLogin,
                isAuthenticated: state => state.auth.isAuthenticated
            })
        },

        methods: {
            ...mapActions(['loading', 'showLogin']),
            create() {
                if (this.$refs.form.validate()) {
                    this.loading(true)
                    service.post({ title: this.title, tags: this.tags, language: this.language }).then(response => {
                        this.title = ''
                        this.tags = []
                        this.loading(false)
                        this.addedDialogVisible = true
                    }).catch(error => {
                        this.loading(false)
                    })
                }
            }
        },

        watch: {
            isAuthenticated: {
                handler(after, before) {
                    if (after) this.showLogin(false)
                }
            }
        },

        mounted() {
            this.loading(true)
            if (!this.isAuthenticated) this.showLogin(true)
            tagsService.getAll().then(response => {
                var items = [];
                response.data.forEach(i => items.push(i.name))
                this.tagItems = items;
                this.loading(false)
            })

            
            this.languageItems = [
                { title: this.t('English'), key: 'en', icon: 'flag-us' },
                { title: this.t('Spanish'), key: 'es', icon: 'flag-es' },
                { title: this.t('Catalan'), key: 'ca', icon: 'flag-catalonia' }
            ]
            this.language = this.$locale

            this.titleRules = [
                s => (s && s.length > 37) ? this.t('max. 37 characters') : true,
                s => (!s || s.length === 0) ? this.t('the title is required') : true
            ]
            this.tagsRules = [
                (v) => !!v && v.length <= 3 || this.t('max. 3 tags')
            ]
            this.languageRules = [
                (v) => !!v || this.t('required')
            ]
        }
    }
</script>

<style>
    .space {
        margin-top: 10px;
        margin-bottom: 10px;
    }
</style>