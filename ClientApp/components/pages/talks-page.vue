<template>
  <div id="home" class="page">
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-ranking></v-ranking>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
    import { mapActions } from 'vuex'
    import Ranking from './home/ranking'

    export default {
        components: { 'v-ranking': Ranking },

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
</style>