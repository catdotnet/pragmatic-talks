<template>
    <v-layout row justify-center>
        <v-dialog v-model="shouldShow" persistent>
            <v-card>
                <v-layout row wrap>
                    <v-flex xs12>
                        <v-card class="blue darken-2 white--text">
                            <v-container fluid grid-list-lg>
                                <v-layout row>
                                    <v-flex xs7>
                                        <div>
                                            <div class="headline">Google</div>
                                            <div v-locale="'Use your Google+ account'"></div>
                                        </div>
                                    </v-flex>
                                    <v-flex xs5>
                                        <v-btn fab dark :href="url('Google')">
                                            <v-icon>fa-google-plus</v-icon>
                                        </v-btn>
                                    </v-flex>
                                </v-layout>
                            </v-container>
                        </v-card>
                    </v-flex>
                    <v-flex xs12>

                        <v-card class="cyan darken-2 white--text">
                            <v-container fluid grid-list-lg>
                                <v-layout row>
                                    <v-flex xs7>
                                        <div>
                                            <div class="headline">Twitter</div>
                                            <div v-locale="'Use your Twitter account'"></div>
                                        </div>
                                    </v-flex>
                                    <v-flex xs5>
                                        <v-btn fab dark :href="url('Twitter')">
                                            <v-icon x-large right>fa-twitter</v-icon>
                                        </v-btn>
                                    </v-flex>
                                </v-layout>
                            </v-container>
                        </v-card>
                    </v-flex>
                </v-layout>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn class="red--text darken-1" flat="flat" @click.native="cancel"><span v-locale="'Cancel'"></span></v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
    export default {

        props: {
            visible: {
                required: true,
                type: Boolean
            }
        },

        data() {
            return {
            }
        },

        computed: {
            shouldShow: {
                get() { return this.visible },
                set(val) { }
            }
        },

        methods: {
            url(provider) {
                return '/Account/ExternalLogin?provider=' + provider + '&returnUrl=' + window.location.href
            },
            cancel() {
                this.$emit('close')
            }
        }
    }
</script>

<style>
</style>