<template>
    <v-list-tile avatar><!-- @click="" -->
        <v-list-tile-avatar>
            <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
        </v-list-tile-avatar>
        <v-list-tile-content>
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
            <v-list-tile-sub-title><v-chip><v-icon>{{ getFlag(item) }}</v-icon></v-chip><v-chip v-for="tag in item.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></v-list-tile-sub-title>
        </v-list-tile-content>
    </v-list-tile>
</template>

<script>
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
            getTagClass(tag) {
                return tag.color + ' white--text'
            },
            getFlag(item) {
                var flag = {
                    'en': 'flag-us',
                    'es': 'flag-es',
                    'ca': 'flag-catalonia'
                }
                return flag[item.language]
            }
        }
    }
</script>

<style>
  
</style>