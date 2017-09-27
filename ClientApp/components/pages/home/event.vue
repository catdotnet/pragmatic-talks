<template>
    <v-list-tile avatar @click="setEvent(item)">
        <v-list-tile-content>
            <v-list-tile-title>
                {{ item.name }} <a :href="item.url" v-if="item.url" target="_blank" title="meetup event"><i class="fa fa-meetup"></i></a><b class="subtitle">{{ getDate(item) }}</b>
            </v-list-tile-title>
            <v-list-tile-sub-title><v-chip v-for="tag in item.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></v-list-tile-sub-title>
        </v-list-tile-content>
    </v-list-tile>
</template>

<script>
    import { mapActions } from 'vuex'

    export default {
        props: {
            item: {
                type: Object,
                required: true
            }
        },

        data() {
            return {

            }
        },

        methods: {
            ...mapActions(['setEvent']),
            getTagClass(tag) {
                return tag.color + ' white--text'
            },
            getDate(item) {
                if (!item.date) return ''
                var d = new Date(item.date)
                if (!d) return ''
                return d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate()
            }
        }
    }
</script>

<style>
    .subtitle {
        font-size: 12px;
        color: #939393;
    }
</style>