<template>
    <v-layout row justify-center>
        <v-dialog v-model="shouldShow" persistent width="50%">
            <v-card>
                <v-card-title>
                    <div class="headline">{{ title }}</div>
                </v-card-title>
                <v-card-text>
                    <v-form :fields="fields" :value="value" :rules="rules" :contextComponent="computedContextComponent"></v-form>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn class="red--text darken-1" flat="flat" @click.native="cancel">Cancel</v-btn>
                    <v-btn class="green--text darken-1" flat="flat" @click.native="accept">Accept</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-layout>
</template>

<script>
    import vForm from './form'

    export default {
        components: { 'v-form': vForm },

        props: {
            visible: {
                required: true,
                type: Boolean
            },
            title: {
                required: true,
                type: String
            },
            fields: {
                required: true,
                type: Array
            },
            value: {
                required: true,
                type: Object
            },
            action: {
                required: true,
                type: Function
            },
            rules: {
                required: false,
                type: Object,
                default: () => { }
            },
            contextComponent: {
                required: false,
                type: Object,
                default: null
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
            computedContextComponent() {
                return this.contextComponent || this.$parent
            }
        },

        methods: {
            accept(model) {
                if (this.$children[0].$children[0].validate(model))
                    this.action(model)
            },
            cancel() {
                this.$emit('close')
            }
        }
    }
</script>
