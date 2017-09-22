<template>
    <div id="rules">
        <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
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