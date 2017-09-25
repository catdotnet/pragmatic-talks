<template>
    <v-navigation-drawer temporary
                         :mini-variant="miniVariant"
                         clipped
                         overflow
                         v-model="show"
                         enable-resize-watcher>
        <v-list>
            <router-link class="naviagation-title" :to="{ name: 'home'}">
                <v-list-tile avatar tag="div">
                    <v-list-tile-avatar>
                        <v-icon>forum</v-icon>
                    </v-list-tile-avatar>
                    <v-list-tile-content>
                        <v-list-tile-title>Pragmatic Talks</v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </router-link>

            <router-link v-for="(item, index) in items" :key="index" class="router-link" :to="item.to" tag="li">
                <a class="list__tile list__tile--active">
                    <v-list-tile-action>
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content>
                        <v-list-tile-title v-locale="item.title"></v-list-tile-title>
                    </v-list-tile-content>
                </a>
            </router-link>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
    export default {
        props: {
            items: {
                type: Array,
                required: true
            },
            miniVariant: {
                type: Boolean,
                required: true
            },
            drawer: {
                type: Boolean,
                required: true
            }
        },

        data() {
            return {
                show: false
            }
        },

        watch: {
            drawer(value) {
                this.show = value
            },
            show(value) {
                this.$emit('show-change', value)
            }
        }
    }
</script>

<style>
    .naviagation-title {
        text-decoration: none;
    }
</style>