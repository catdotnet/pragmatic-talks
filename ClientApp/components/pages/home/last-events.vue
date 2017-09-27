<template>
    <v-card v-if="items">
        <v-card-title primary-title>
            <h3 class="headline mb-0" v-locale="'Last events'"></h3>
        </v-card-title>
        <v-list three-line>
            <v-event v-for="(item, index) in items" :key="index" :item="item"></v-event>
        </v-list>
    </v-card>
</template>

<script>
    import { mapActions } from 'vuex'
    import Event from './event'
    import service from '../../../services/events'

    export default {
        components: { 'v-event': Event },

        data() {
            return {
                items: []
            }
        },

        methods: {
            ...mapActions(['loading'])
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
</style>