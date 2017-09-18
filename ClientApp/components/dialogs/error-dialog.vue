<template>
    <v-layout row justify-center>
        <v-dialog v-model="shouldShow" persistent>
            <v-card>
                <v-card-title>
                    <div class="headline">Error</div>
                </v-card-title>
                <v-card-text>
                    {{ errorMessage }}
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn class="green--text darken-1" flat="flat" @click.native="cancel">Ok</v-btn>
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
                errorMessage: state => state.errorMessage
            }),
            shouldShow: {
                get() { return !!this.errorMessage },
                set(val) {  }
            }
        },

        methods: {
            ...mapActions(['error']),
            cancel() {
                this.error('')
                this.$emit('close')
            }
        },

        mounted() {
            if (window.errorMessage)
                this.error(window.errorMessage)
        }
    }
</script>

<style>
</style>