<template>
    <v-flex xs12>
        
        <v-select v-if="field.type === 'select'" :items="field.options" :label="field.label || field.text" :item-text="field.optionText" :item-value="field.optionValue" v-model="model" v-bind="field"></v-select>
        
        
        <template v-else-if="['radios', 'checkboxes'].indexOf(field.type) > -1">
            <p>{{ field.label || field.text }}</p>
            <v-layout row="row" wrap="wrap"><v-flex v-bind="{[field.width]: true}" xs12="xs12" v-for="option in field.choices" :key="field.value"><component v-model="model" hide-details="hide-details" :is="field.type == 'radios' ? 'v-radio' : 'v-checkbox'" :key="option.value" :value="option.value" :label="option.text"></component></v-flex></v-layout>
        </template>
        
        <template v-else-if="['bool'].indexOf(field.type) > -1">
            <v-switch :label="field.label || field.text" v-model="model"></v-switch>
        </template>

        <template v-else-if="['date', 'datetime', 'time'].indexOf(field.type) > -1">
            <v-menu>
                <v-text-field slot="activator" v-model="model" :label="field.label || field.text"></v-text-field>
                <v-date-picker v-model="model" no-title="no-title" scrollable="scrollable" actions="actions"></v-date-picker>
            </v-menu>
        </template>
        
        <v-text-field v-else-if="field.type === 'search-text'" @blur="emit" @keyup.native.enter="emit" v-model="model" v-bind="field" :label="field.label || field.text" :append-icon="'search'" :placeholder="field.placeholder" type="text" :multiLine="field.type == 'textarea'"></v-text-field>

        <v-text-field v-else v-model="model" v-bind="field" :label="field.label || field.text" :placeholder="field.placeholder" type="text" :multiLine="field.type == 'textarea'"></v-text-field>

    </v-flex>
</template>

<script>

    export default {
        props: {
            field: {
                type: Object,
                required: true
            },
            name: {
                type: String,
                required: false
            },
            value: {
                required: false
            }
        },

        data() {
            return {
                inputGroupClass: 'input-group input-group--dirty input-group--text-field'
            }
        },

        computed: {
            model: {
                get() {
                    return this.value
                },
                set(val) {
                    if (this.field.type !== 'search-text')
                        this.emit(val)
                }
            }
        },

        methods: {
            onSelectFile(e) {
                console.log(e)
            },
            emit(val) {
                this.$emit('input', typeof val === 'object' ? val.target.value : val)
            }
        }
    }
</script>
