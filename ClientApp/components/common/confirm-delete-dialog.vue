<template>
    <v-layout row justify-center>
        <v-dialog v-model="shouldShow" persistent>
            <v-card>
                <v-card-title>
                    <div class="headline">Delete confirmation</div>
                </v-card-title>
                <v-card-text>You are going to delete '<i v-text="showName"></i>', are you sure?</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn class="red--text darken-1" flat="flat" @click.native="cancel">No</v-btn>
                    <v-btn class="green--text darken-1" flat="flat" @click.native="accept">Yes</v-btn>
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
            },
            action: {
                required: true,
                type: Function
            },
            name: {
                required: false,
                type: String
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
            },
            showName: {
                get() { return this.name || '' },
                set(val) { }
            }
        },

        methods: {
            accept() {
                this.action()
            },
            cancel() {
                this.$emit('close')
            }
        }
    }
</script>
