<template>
    <v-layout row justify-center>
        <v-dialog v-model="shouldShow" persistent>
            <v-card>
                <v-card-title>
                    <div class="headline" v-locale="'Talk Created'"></div>
                </v-card-title>
                <v-card-text>
                    <span v-locale="'Your talk has been created successfully'"></span>
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
            cancel() {
                this.$emit('close')
            }
        }
    }
</script>

<style>
</style>