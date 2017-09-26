<template>
    <v-card v-if="items">
        <v-card-title primary-title>
            <h3 class="headline mb-0" v-locale="'Last events'"></h3>
        </v-card-title>
        <v-list three-line>
            <v-list-tile v-for="(item, index) in items" :key="index" avatar> <!-- @click="" -->
                <v-list-tile-content>
                    <v-list-tile-title>
                        {{ item.name }} 
                        <b class="subtitle">{{ getDate(item) }}</b>
                        <a href="item.url" v-if="item.url" target="_blank" title="meetup event"><i class="fa fa-meetup"></i></a>
                    </v-list-tile-title>
                    <v-list-tile-sub-title><v-chip v-for="tag in item.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></v-list-tile-sub-title>
                </v-list-tile-content>
            </v-list-tile>
        </v-list>
    </v-card>
</template>

<script>
    import { mapActions } from 'vuex'
    import service from '../../../services/events'

    export default {

        data() {
            return {
                items: []
            }
        },

        methods: {
            ...mapActions(['loading']),
            getTagClass(tag) {
                return tag.color + ' white--text'
            },
            getDate(item) {
                if (!item.date) return ''
                var d = new Date(item.date)
                if (!d) return ''
                return d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate()
            }
        },

        mounted() {
            this.loading(true)
            service.last().then(response => {
                this.items = response.data
                this.loading(false)
            }).catch(error => {
                this.loading(false)
            });
        }
    }
</script>

<style>
    .subtitle {
        font-size: 12px;
        color: #939393;
    }
</style>