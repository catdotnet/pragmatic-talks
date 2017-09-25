<template>
    <v-card>
        <v-list two-line subheader>
            <v-subheader inset v-locale="'Promoted'"></v-subheader>
            <v-list-tile avatar v-for="item in items" :key="item.title"> <!-- @click="" -->
                <v-list-tile-avatar>
                    <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
                </v-list-tile-avatar>
                <v-list-tile-content>
                    <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    <div><v-chip v-if="item.language"><v-icon>{{ getFlag(item) }}</v-icon></v-chip><v-chip v-for="tag in item.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></div>
                </v-list-tile-content>
            </v-list-tile>
            <v-divider inset></v-divider>
            <v-subheader inset v-locale="'Stack'"></v-subheader>
            <v-list-tile v-for="item in items2" :key="item.title" avatar> <!-- @click="" -->
                <v-list-tile-avatar>
                    <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
                </v-list-tile-avatar>
                <v-list-tile-content>
                    <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                    <div><v-chip><v-icon>{{ getFlag(item) }}</v-icon></v-chip><v-chip v-for="tag in item.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></div>
                </v-list-tile-content>
            </v-list-tile>
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
    import service from '../../../services/talks'

    export default {

        data() {
            return {
                items: [],
                items2: []
            }
        },

        methods: {
            ...mapActions(['loading']),
            getTagClass(tag) {
                return tag.color + ' white--text'
            },
            getFlag(item) {
                var flag = {
                    'en': 'flag-us',
                    'es': 'flag-es',
                    'ca': 'flag-catalonia'
                }
                return flag[item.language]
            }
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