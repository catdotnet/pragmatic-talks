<template>
    <v-card>
        <v-card-title primary-title>
            <h3 class="headline mb-0" v-locale="'Talks'"></h3>
        </v-card-title>
        <v-list two-line subheader>
            <v-subheader inset v-locale="'Promoted'"></v-subheader>
            <v-talk v-for="item in items" :key="item.title" :item="item"></v-talk>
            <v-divider inset></v-divider>
            <v-subheader inset v-locale="'Stack'"></v-subheader>
            <v-talk v-for="item in items2" :key="item.title" :item="item"></v-talk>
        </v-list>
        <v-card-actions class="white">
            <v-spacer></v-spacer>
            <v-btn floating fab dark router to="/create" success absolute top right>
                <v-icon>add</v-icon>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
    import { mapActions } from 'vuex'
    import Talk from './talk'
    import service from '../../../services/talks'

    export default {
        components: { 'v-talk': Talk },

        data() {
            return {
                items: [],
                items2: []
            }
        },

        methods: {
            ...mapActions(['loading'])
        },

        mounted() {
            this.loading(true)
            var items = [], items2 = [];
            service.get().then(response => {
                response.data.forEach(item => {
                    if (item.isSelected) items.push({ icon: 'star', iconClass: 'amber lighten-1 white--text', title: item.title, tags: item.tags, language: item.language })
                    else items2.push({ icon: 'lightbulb_outline', iconClass: 'cyan white--text', title: item.title, tags: item.tags, language: item.language })
                })

                if (items.length < 5) {
                    var emptySpaces = 5 - items.length;
                    for (var i = 0; i < emptySpaces; i++)
                        items.push({ icon: 'access_time', iconClass: 'grey lighten-1 white--text', title: this.t('Unasigned...') })
                }

                this.items = items
                this.items2 = items2
                this.loading(false)
            }).catch(error => {
                this.loading(false)
            });
        }
    }
</script>

<style>
</style>