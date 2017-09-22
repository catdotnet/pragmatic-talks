<template>
    <form>
        <v-container fluid>
            <v-layout row v-for="(field, index) in fields" :key="index">
                <v-field :name="field.value" :field="field" v-model="model[field.value]"></v-field>
            </v-layout>

            <v-alert error="error" v-model="hasError" xs12="xs12">
                <div v-for="error in errors"> {{ error }}</div>
            </v-alert>

            <v-layout row>
                <v-flex xs12>
                    <v-btn v-for="(action, key) in actions" :key="key" floating dark small
                           :router="action.router" :to="{ name: action.to ? action.to.name : '', params: { id: model.id }}"
                           :primary="action.primary" :success="action.success" :error="action.error"
                           @click.native="executeAction(action, model)">
                        <v-icon>{{ action.icon }}</v-icon> {{ action.text }}
                    </v-btn>
                </v-flex>
            </v-layout>
        </v-container>
    </form>
</template>

<script>
    import vField from './field'

    export default {
        components: { 'v-field': vField },

        props: {
            fields: {
                required: true,
                type: Array
            },
            value: {
                required: true,
                type: Object
            },
            actions: {
                required: false,
                type: Object,
                default: function () { return {} }
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
                model: this.value,
                hasError: false,
                errors: [],
                message: ''
            }
        },

        computed: {
            
        },

        watch: {
            value(val) {
                this.model = val
            }
        },

        methods: {
            executeAction(action) {
                if (!action.click) return

                var self = this.contextComponent ? this.contextComponent : this.$parent
                action.click.call(self, this.model, this.validate)
            },
            validate() {
                this.errors = []
                this.hasError = false
                Object.keys(this.rules).forEach(key => {
                    var rule = this.rules[key]
                    var value = this.model[key]
                    var result = rule.validate(value, this.model)
                    if (result && !result.success) {
                        this.errors.push(result.message)
                        this.hasError = true
                    }
                })

                return !this.hasError;
            }
        },

        mounted() {

        },

        created() {

        }
    }
</script>

