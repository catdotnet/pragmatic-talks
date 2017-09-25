<template>
    <div id="events">
        <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
               <v-next-event></v-next-event>
            </v-flex>
        </v-layout>
        <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
               <v-last-events></v-last-events>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
    import NextEvent from './home/next-event'
    import LastEvents from './home/last-events'

    export default {
        components: { 'v-next-event': NextEvent, 'v-last-events': LastEvents },

        data() {
            return {
               
            }
        }
    }
</script>

<style>
</style>