<template>
    <div>
        <v-grid-page :loadData="load" :columns="columns" :filters="filters" :actions="actions" :sortBy="sortBy" :contextComponent="computedContextComponent"></v-grid-page>
        <v-delete-dialog :visible="deleteDialog.visible" :name="deleteDialog.item ? deleteDialog.item.name : ''" :action="deleteItem" @close="closeConfirmDelete"></v-delete-dialog>
        <v-form-dialog :title="editDialog.title" :visible="editDialog.visible" :fields="columns" :value="editDialog.item" :rules="editDialog.rules" :action="editItem" @close="closeEdit"></v-form-dialog>
    </div>
</template>

<script>
    import vGridPage from './grid-page'
    import vDeleteDialog from './confirm-delete-dialog'
    import vFormDialog from './form-dialog'

    export default {
        components: { 'v-grid-page': vGridPage, 'v-delete-dialog': vDeleteDialog, 'v-form-dialog': vFormDialog },

        props: {
            columns: {
                required: true,
                type: Array
            },
            service: {
                required: true,
                type: Object
            },
            sortBy: {
                required: false,
                type: String
            },
            searchFilter: {
                required: false,
                type: Boolean,
                defalt: false
            },
            contextComponent: {
                required: false,
                type: Object,
                default: null
            }
        },

        data() {
            return {
                filters: {
                },
                actions: {
                    add: { type: 'table', icon: 'add', click: function (reloadMethod) { this.showEdit(null, reloadMethod) }, absolute: true, top: true, right: true, success: true },
                    edit: { type: 'row', icon: 'edit', click: function (row, reloadMethod) { this.showEdit(row, reloadMethod) }, primary: true },
                    delete: { type: 'row', icon: 'delete', click: function (row, reloadMethod) { this.confirmDelete(row, reloadMethod) }, error: true }
                },
                deleteDialog: {
                    item: null,
                    reloadMethod: null,
                    visible: false
                },
                editDialog: {
                    item: {},
                    visible: false,
                    title: '',
                    reloadMethod: null,
                    rules: {
                    }
                }
            }
        },

        computed: {
            computedContextComponent() {
                return this.contextComponent || this
            }
        },

        methods: {
            load(page, rowsPerPage, filters, orderBy) {
                var sortBy = orderBy.split(' ')[0]
                var descending = orderBy.split(' ')[1] === 'desc'
                var service = this.service
                return new Promise(function (resolve, reject) {
                    service.getAll().then(
                        data => {
                            var list = data.data.filter(function (item) {
                                if (!filters.search) return true
                                var result = false
                                Object.keys(item).forEach(key => { if (item[key] && item[key].toString().toLowerCase().indexOf(filters.search.toLowerCase()) >= 0) result = true })
                                return result
                            })
                            list = list.sort(function (a, b) {
                                var aVal = a[sortBy], bVal = b[sortBy];
                                return descending ? aVal > bVal ? -1 : aVal < bVal ? 1 : 0 : aVal > bVal ? 1 : aVal < bVal ? -1 : 0;
                            })
                            resolve({ items: list, totalPages: 1 });
                        },
                        error => { reject(error) })
                })
            },
            confirmDelete(item, reloadMethod) {
                this.deleteDialog.item = item
                this.deleteDialog.reloadMethod = reloadMethod
                this.deleteDialog.visible = true
            },
            closeConfirmDelete() {
                this.deleteDialog.visible = false
                this.deleteDialog.reloadMethod = null
                this.deleteDialog.item = null
            },
            deleteItem() {
                this.service.delete(this.deleteDialog.item).then(response => {
                    this.deleteDialog.reloadMethod()
                    this.closeConfirmDelete()
                })
            },
            showEdit(item, reloadMethod) {
                this.editDialog.item = item || { id: 0, name: '' }
                this.editDialog.title = item ? 'Edit item' : 'Create item'
                this.editDialog.reloadMethod = reloadMethod
                this.editDialog.visible = true
            },
            closeEdit() {
                this.editDialog.reloadMethod()
                this.editDialog.visible = false
                this.editDialog.title = ''
                this.editDialog.reloadMethod = null
                this.editDialog.item = {}
            },
            editItem() {
                var method = this.editDialog.item.id ? this.service.update : this.service.create
                method(this.editDialog.item).then(response => { 
                    this.closeEdit()
                })
            }
        },

        created() {
            if (this.searchFilter)
                this.filters.search = { type: 'search-text', label: '', placeholder: 'Search...' }

            var rules = {};
            this.columns.forEach(column => {
                if (column.validate) {
                    rules[column.value] = { validate(val) { return column.validate(val) } }
                }
            })

            this.editDialog.rules = rules
        }
    }
</script>
