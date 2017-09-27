<template>
    <v-layout row justify-center>
        <v-dialog v-model="shouldShow" persistent scrollable :width="width">
            <v-card v-if="event">
                <v-card-title>
                    <div class="headline">{{ event.name }}</div>
                </v-card-title>
                <v-card-text style="height: 60vh">
                    <v-list subheader>
                        <v-divider inset></v-divider>
                        <v-subheader inset v-locale="'Information'"></v-subheader>
                        <v-list-tile>
                            <v-list-tile-content>
                                <v-list-tile-title><span v-locale="'When'"></span>: {{ getDate(event) }}</v-list-tile-title>
                            </v-list-tile-content>
                        </v-list-tile>
                        <v-list-tile>
                            <v-list-tile-content>
                                <v-list-tile-title><span v-locale="'Web'"></span>: <a :href="event.url" target="_blank"><i class="fa fa-meetup"></i></a></v-list-tile-title>
                            </v-list-tile-content>
                        </v-list-tile>
                        <v-divider inset></v-divider>
                        <v-subheader inset v-locale="'Talks'"></v-subheader>
                        <v-list-tile v-for="item in event.talks" :key="item" avatar>
                            <v-list-tile-avatar>
                                <v-icon class="amber lighten-1 white--text">lightbulb_outline</v-icon>
                            </v-list-tile-avatar>
                            <v-list-tile-content>
                                <v-list-tile-title>{{ item }}</v-list-tile-title>
                            </v-list-tile-content>
                        </v-list-tile>
                        <v-divider inset></v-divider>
                        <v-subheader inset v-locale="'Speakers'"></v-subheader>
                        <v-list-tile v-for="item in event.speakers" :key="item.title" avatar>
                            <v-list-tile-avatar>
                                <img :src="item.avatarUrl" />
                            </v-list-tile-avatar>
                            <v-list-tile-content>
                                <v-list-tile-title>{{ item.name }}</v-list-tile-title>
                            </v-list-tile-content>
                        </v-list-tile>
                        <v-divider inset></v-divider>
                    </v-list>
                    <v-list three-line subheader>
                        <v-subheader inset v-locale="'Tags'"></v-subheader>
                        <v-list-tile>
                            <v-list-tile-content>
                                <div><v-chip v-for="tag in event.tags" :key="tag.name" :class="getTagClass(tag)">{{ tag.name }}</v-chip></div>
                            </v-list-tile-content>
                        </v-list-tile>
                    </v-list>

                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn class="green--text darken-1" flat="flat" @click.native="cancel"><span v-locale="'Ok'"></span></v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
    import { mapState, mapActions } from 'vuex'

    export default {
        data() {
            return {
            }
        },

        computed: {
            ...mapState({
                event: state => state.event
            }),
            shouldShow: {
                get() { return !!this.event },
                set(val) {  }
            },
            width() {
                return screen.width <= 599 ? '90%' : '50%'
            }
        },

        methods: {
            ...mapActions(['setEvent']),
            cancel() {
                this.setEvent(null)
                this.$emit('close')
            },
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
    .event-dialog {
        width: 50%;
    }
</style>