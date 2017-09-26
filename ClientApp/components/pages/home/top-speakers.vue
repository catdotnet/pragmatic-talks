<template>
    <v-card>
        <v-card-title primary-title>
            <h3 class="headline mb-0" v-locale="'Top Speakers'"></h3>
        </v-card-title>
        <v-list>
            <v-list-tile v-for="item in items" :key="item.title" avatar>
                <!--  @click="" -->
                <v-list-tile-avatar>
                    <img :src="item.avatarUrl" />
                </v-list-tile-avatar>
                <v-list-tile-content>
                    <v-list-tile-title>{{ item.displayName }}</v-list-tile-title>
                    <v-list-tile-sub-title><b>{{ item.talksInEventsCounter }}</b> / {{ item.talksCounter }}</v-list-tile-sub-title>
                </v-list-tile-content>
            </v-list-tile>
        </v-list>
    </v-card>
</template>

<script>
    import { mapActions } from 'vuex'
    import service from '../../../services/speakers'

    export default {

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
            var items = []
            service.getTop(5).then(response => {
                this.items = response.data
                this.loading(false)
            }).catch(error => {
                this.loading(false)
            });
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

    .subheader--inset {
        margin-left: 4px;
    }

    .divider--inset {
        margin-left: 4px;
        width: calc(100% - 4px);
    }
</style>