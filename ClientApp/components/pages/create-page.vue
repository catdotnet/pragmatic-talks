<template>
    <div id="create" class="page">
        <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-card>
                    <v-card-title>
                        <span class="headline">Create new talk</span>
                    </v-card-title>
                    <v-card-text>
                        <v-text-field label="Title" v-model="title" :counter="37" :rules="titleRule" required></v-text-field>
                        <div class="space"></div>
                        <v-select v-model="tags" label="Tags" multiple tags :items="items"></v-select>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn class="blue--text darken-1" flat @click.native="">Send</v-btn>
                        <v-btn floating fab dark router to="/" primary absolute top right>
                            <v-icon>chevron_left</v-icon>
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
    import { mapActions } from 'vuex'

    export default {
        
        data() {
            return {
                title: '',
                titleRule:[
                    s => (s && s.length > 37) ? 'you can not add more than 37 characters' : true,
                    s => (!s || s.length === 0) ? 'the title is required' : true
                ],
                tags: [],
                items: [
                    'front',
                    'back',
                    'data',
                    'dotnet',
                    'js',
                    'node',
                    'other languages',
                    'library',
                    'cloud',
                    'mobile',
                    'desktop',
                    'infrastructure',
                    'big data',
                    'devops',
                    'web',
                    'IoT',
                    'games'
                ]
            }
        },

        methods: {
            ...mapActions(['loading'])
        },

        mounted() {
            this.loading(false)
        }
    }
</script>

<style>
    .space {
        margin-top: 10px;
        margin-bottom: 10px;
    }
</style>