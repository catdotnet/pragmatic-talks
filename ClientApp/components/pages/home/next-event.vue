<template>
    <v-card v-if="item">
        <v-list three-line subheader>
            <v-subheader inset v-locale="'Next event'"></v-subheader>
            <v-list-tile avatar> <!-- @click="" -->
                <v-list-tile-content>
                    <v-list-tile-title>
                        {{ item.name }} 
                        <b class="subtitle">{{ getDate(item) }}</b>
                        <a href="item.url" v-if="item.url" target="_blank" title="meetup event"><i class="fa fa-meetup"></i></a>
                    </v-list-tile-title>
                    <div><v-chip v-for="tag in item.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></div>
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
                item: null
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
            service.next().then(response => {
                this.item = response.data
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