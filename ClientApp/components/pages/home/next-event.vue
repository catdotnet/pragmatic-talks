<template>
    <v-card v-if="item">
        <v-card-title primary-title>
            <h3 class="headline mb-0" v-locale="'Next event'"></h3>
        </v-card-title>
        <v-list three-line>
            <v-event :item="item"></v-event>
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
                item: null
            }
        },

        methods: {
            ...mapActions(['loading'])
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
</style>