<template>
    <div id="home" class="page container fluid grid-list-md">
        <v-layout row wrap class="not-mobile">
            <v-flex d-flex xs12 sm12 md8 lg8 xl8>
                <v-layout row wrap xs12 sm12>
                    <v-flex xs12 sm12>
                        <v-next-event></v-next-event>
                    </v-flex>
                    <v-flex xs12 sm12>
                        <v-ranking></v-ranking>
                    </v-flex>
                </v-layout>
            </v-flex>
            <v-flex d-flex xs12 sm12 md4 lg4 xl4 class="align-start">
                <v-layout row wrap>
                    <v-flex xs12 sm12>
                        <v-top-speakers></v-top-speakers>
                    </v-flex>
                    <v-flex xs12 sm12>
                        <v-top-tags></v-top-tags>
                    </v-flex>
                </v-layout>
            </v-flex>
        </v-layout>
        <!-- work-arround... i don't know how to fix the problem with big titles in tasks displaying in mobile -->
        <v-layout row wrap class="only-mobile">
            <v-flex xs12 sm12>
                <v-next-event></v-next-event>
            </v-flex>
            <v-flex xs12 sm12>
                <v-ranking></v-ranking>
            </v-flex>
            <v-flex xs12 sm12>
                <v-top-speakers></v-top-speakers>
            </v-flex>
            <v-flex xs12 sm12>
                <v-top-tags></v-top-tags>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
    import { mapActions } from 'vuex'
    import NextEvent from './home/next-event'
    import Ranking from './home/ranking'
    import TopSpeakers from './home/top-speakers'
    import TopTags from './home/top-tags'

    export default {
        components: { 'v-next-event': NextEvent, 'v-ranking': Ranking, 'v-top-speakers': TopSpeakers, 'v-top-tags': TopTags },

        data() {
            return {

            }
        }
    }
</script>

<style>
    .chip {
        font-size: 10px;
        height: 24px;
    }

    .list__tile {
        font-size: 18px;
    }

    .align-start {
        -webkit-align-content: flex-start; /* Safari */
        align-content: flex-start;
    }

    @media only screen and (max-width: 599px) {
        .align-start {
            -webkit-align-content: initial; /* Safari */
            align-content: initial;
        }
    }
</style>