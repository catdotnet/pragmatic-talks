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
                            <v-text-field :label="t('Title')" v-model="title" :counter="37" :rules="titleRule" required></v-text-field>
                            <div class="space"></div>
                            <v-select v-model="tags" :label="t('Tags')" :rules="tagsRule" multiple tags :items="items"></v-select>
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

    </div>
</template>

<script>
    import { mapState, mapActions } from 'vuex'
    import service from '../../services/talks'
    import tagsService from '../../services/tags'

    export default {

        data() {
            return {
                valid: false,
                title: '',
                titleRule: [
                    s => (s && s.length > 37) ? 'you can not add more than 37 characters' : true,
                    s => (!s || s.length === 0) ? 'the title is required' : true
                ],
                tagsRule: [
                    (v) => !!v && v.length <= 3 || 'max. 3 tags'
                ],
                tags: [],
                items: [
                ]
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
                    service.post({ title: this.title, tags: this.tags }).then(response => {
                        this.title = ''
                        this.tags = []
                        this.loading(false)
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
                this.items = items;
                this.loading(false)
            })
        }
    }
</script>

<style>
    .space {
        margin-top: 10px;
        margin-bottom: 10px;
    }
</style>