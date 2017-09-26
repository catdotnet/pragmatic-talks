<template>
    <v-card>
        <v-grid :loadData="loadData" :columns="columns" :filters="filters" :actions="actions" :sortBy="sortBy" :autoSaveState="autoSaveState" :contextComponent="computedContextComponent"></v-grid>

        <v-card-actions class="white">
            <v-spacer></v-spacer>
            <v-btn v-if="action && action.icon" v-for="(action, key) in tableActions" :key="key" floating fab dark small :router="action.router" :to="action.to" :primary="action.primary" :success="action.success" :error="action.error" @click.native="executeAction(action)" :absolute="action.absolute" :top="action.top" :bottom="action.bottom" :right="action.right" :left="action.left">
                <v-icon>{{ action.icon }}</v-icon>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
    import vGrid from './grid'

    export default {
        components: { 'v-grid': vGrid },

        props: {
            loadData: {
                required: true,
                type: Function
            },
            columns: {
                required: true,
                type: Array
            },
            filters: {
                required: false,
                type: Object
            },
            actions: {
                required: false,
                type: Object
            },
            sortBy: {
                required: false,
                type: String
            },
            autoSaveState: {
                required: false,
                type: Boolean,
                default: true
            },
            contextComponent: {
                required: false,
                type: Object,
                default: null
            }
        },

        computed: {
            tableActions: function () {
                var result = []
                var keys = Object.keys(this.actions)
                keys.forEach(key => {
                    if (this.actions[key].type === 'table') result.push(this.actions[key])
                })

                return result;
            },
            computedContextComponent() {
                return this.contextComponent || this.$parent
            }
        },

        methods: {
            executeAction(action) {
                if (!action.click) return

                var self = this.contextComponent ? this.contextComponent : this.$parent
                var reloadMethod = this.$children[0].reload // be aware of layout changes
                action.click.call(self, reloadMethod)
            }
        }
    }
</script>
