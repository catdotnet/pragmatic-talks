<template>
    <div id="rules" class="page container fluid grid-list-md">
        <v-layout row wrap>
            <v-flex xs12 sm12 md6 lg6 xl6>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'What'"></span>
                        </v-card-title>
                        <v-card-text>
                            <div v-locale-html="'WhatText'"></div>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-text>
                            <v-parallax src="/images/picture2.jpeg">
                                <v-layout column align-center justify-center>
                                    <h4 class="white--text" v-locale="'Learn, Talk, Share'"></h4>
                                </v-layout>
                            </v-parallax>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'How'"></span>
                        </v-card-title>
                        <v-card-text>
                            <div v-locale-html="'HowText'"></div>
                        </v-card-text>
                    </v-card>
                </v-flex>
            </v-flex>
            <v-flex xs12 sm12 md6 lg6 xl6>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-text>
                            <v-parallax src="/images/picture1.jpeg">
                                <v-layout column align-center justify-center>
                                    <h1 class="white--text" v-locale="'About'"></h1>
                                </v-layout>
                            </v-parallax>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'Rules'"></span>
                        </v-card-title>
                        <v-card-text>
                            <ol>
                                <li v-for="(o, index) in Array(11)" :key="index" v-locale="'rule' + ('0' + (index + 1)).slice(-2)"></li>
                            </ol>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'Why'"></span>
                        </v-card-title>
                        <v-card-text>
                            <div v-locale-html="'WhyText'"></div>
                        </v-card-text>
                    </v-card>
                </v-flex>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
    export default {

        data() {
            return {

            }
        }
    }
</script>

<style>
</style>