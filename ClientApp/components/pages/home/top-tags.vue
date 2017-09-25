<template>
    <v-card>
        <v-list subheader>
            <v-subheader inset v-locale="'Top Tags'"></v-subheader>
            <div>
                <v-chip v-for="item in items" :key="item.name" :class="item.color + ' white--text'">
                    <strong>{{ item.name }}</strong>
                    &nbsp;
                    <span>({{ item.talksCount }})</span>
                </v-chip>
            </div>
        </v-list>
    </v-card>
</template>

<script>
    import { mapActions } from 'vuex'
    import service from '../../../services/tags'

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
            service.getTop(20).then(response => {
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