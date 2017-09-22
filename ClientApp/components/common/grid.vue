<template>
    <div>
        <v-layout row mt-2 v-if="filtersCount > 0">
            <v-flex :class="'xs' + (12 - 3 * filtersCount)">
                &nbsp;
            </v-flex>
            <v-flex v-for="(field, name) in internalFilters.fields" :key="name" xs3>
                <v-field :field="field" :name="name" v-model="internalFilters.model[name]"></v-field>
            </v-flex>
        </v-layout>
        <v-data-table v-bind:headers="columns" v-bind:items="items" v-bind:pagination.sync="pagination" :loading="loading" hide-actions>
            <template slot="items" scope="props">
                <tr :key="props.item.id">
                    <td :class="column.align === 'right' ? 'text-xs-right' : column.align === 'center' ? 'text-xs-center' : '' " v-for="column in columns">
                        <span class="table-icon" v-if="column.type === 'if-icon'">
                            <v-icon small v-if="props.item[column.value]">
                                {{ column.icon }}
                            </v-icon>
                        </span>
                        <span v-else-if="column.type === 'select'">
                            {{ getSelectText(column, props.item) }}
                        </span>
                        <span v-else>
                            {{ props.item[column.value] }}
                        </span>
                    </td>
                    <td v-if="actions" width="180" class="text-xs-center">
                        <v-btn v-if="action && action.icon" v-for="(action, key) in rowActions" :key="key" floating fab dark small 
                               :router="action.router" :to="{ name: action.to ? action.to.name : '', params: { id:props.item.id }}" 
                               :primary="action.primary" :success="action.success" :error="action.error" 
                               @click.native="executeAction(action, props.item)">
                            <v-icon>{{ action.icon }}</v-icon>
                        </v-btn>
                    </td>
                </tr>
            </template>
        </v-data-table>

        <v-layout class="text-xs-center" v-if="page.total > 1">
            <v-pagination v-model="page.current" :length="page.total" circle class="data-pagination"></v-pagination>
        </v-layout>
    </div>
</template>

<script>
    import vField from './field'

    export default {
        components: { 'v-field': vField },

        props: {
            loadData: {
                required: true,
                type: Function,
                default: function (page, rowsPerPage, filters, orderBy) {
                    return new Promise(function (resolve) {
                        resolve({ items: [], totalPages: 1 });
                    });
                }
            },
            columns: {
                required: true,
                type: Array,
                default: []
            },
            filters: {
                required: false,
                type: Object,
                default: {}
            },
            actions: {
                required: false,
                type: Object,
                default: {}
            },
            sortBy: {
                required: false,
                type: String,
                default: ''
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

        data() {
            return {
                internalFilters: {
                    model: {},
                    fields: {}
                },
                pagination: {
                    sortBy: this.sortBy,
                    descending: false,
                    page: 1,
                    rowsPerPage: 10
                },
                page: {
                    current: 1,
                    total: 0
                },
                items: [],
                lastPage: 0,
                loading: false
            }
        },

        computed: {
            filtersCount: function () {
                return Object.keys(this.filters).length;
            },
            rowActions: function () {
                var result = []
                var keys = Object.keys(this.actions)
                keys.forEach(key => {
                    if (!this.actions[key].type || this.actions[key].type === 'row') result.push(this.actions[key])
                })

                return result;
            }
        },

        methods: {
            executeAction(action, item) {
                if (!action.click) return

                var self = this.contextComponent ? this.contextComponent : this.$parent
                action.click.call(self, item, this.reload)
            },
            reload() {
                if (this.loading) return
                this.loading = true
                this.saveState()
                var orderBy = this.pagination.sortBy + (this.pagination.descending ? ' desc' : '')
                return this.loadData(this.page.current - 1, this.pagination.rowsPerPage, this.internalFilters.model, orderBy).then(
                    data => {
                        this.items = data.items
                        this.page.total = data.totalPages
                        if(data.items.length > this.pagination.rowsPerPage)
                            this.pagination.rowsPerPage = data.items.length
                        
                        this.loading = false
                    },
                    error => { console.log(error) })
            },
            saveState() {
                if (!this.autoSaveState) return

                var state = {
                    page: this.page.current,
                    filters: this.internalFilters.model,
                    sort: {
                        column: this.pagination.sortBy,
                        descending: this.pagination.descending
                    }
                }
                sessionStorage.setItem(this.$route.name + '-grid', JSON.stringify(state));
            },
            loadState() {
                if (!this.autoSaveState) return
                var state = JSON.parse(sessionStorage.getItem(this.$route.name + '-grid'));
                try {
                    this.lastPage = state.page || 1
                    this.page.current = state.page || 1
                    this.pagination.sortBy = state.sort.column || ''
                    this.pagination.descending = !!state.sort.descending
                    Object.keys(state.filters).forEach(key => {
                        this.internalFilters.model[key] = state.filters[key]
                    })
                } catch(e) {
                    console.log('cannot parse state in "' + this.$route.name + ' - grid"');
                }
            },
            getSelectText(column, item){
                try {
                    return column.options.filter(op => op[column.optionValue] === item[column.value])[0][column.optionText]
                } catch (e){
                    return item[column.value];
                }
            }
        },

        watch: {
            pagination: {
                handler(after, before) {
                    this.reload()
                },
                deep: true
            },
            page: {
                handler(after, before) {
                    //if (after.current !== before.current) { //it does not work :/
                    if (after.current !== this.lastPage) {
                        this.reload()
                        this.lastPage = after.current
                    }
                },
                deep: true
            },
            internalFilters: {
                handler(after, before) {
                    if (!Object.keys(before.model).length) return //initialization
                    this.page.current = 1
                    this.reload()
                },
                deep: true
            }
        },

        created() {
            this.loading = true
            var filters = {
                model: {},
                fields: this.filters
            }

            Object.keys(filters.fields).forEach(function (key) {
                if (filters.fields[key].type === 'bool') filters.model[key] = false
                else filters.model[key] = ''
            })

            this.internalFilters = filters

            this.loadState()
            this.loading = false
        }
    }
</script>

<style>
    .data-pagination {
        margin: auto auto;
    }

    .table-icon .icon {
        font-size: 13px !important;
    }
</style>