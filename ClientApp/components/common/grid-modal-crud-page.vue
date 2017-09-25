<template>
    <div>
        <v-grid-page :loadData="load" :columns="columns" :filters="filters" :actions="computedActions" :sortBy="sortBy" :contextComponent="computedContextComponent"></v-grid-page>
        <v-delete-dialog :visible="deleteDialog.visible" :name="deleteDialog.item ? deleteDialog.item.name || deleteDialog.item.title || deleteDialog.item.displayName : ''" :action="deleteItem" @close="closeConfirmDelete"></v-delete-dialog>
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
            actions: {
                required: false,
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
            computedActions() {
                var actions = this.actions || {};

                if (this.actions.add)
                    actions.add = { type: 'table', icon: 'add', click: function (reloadMethod) { this.showEdit(null, reloadMethod) }, absolute: true, top: true, right: true, success: true }
                if (this.actions.edit)
                    actions.edit = { type: 'row', icon: 'edit', click: function (row, reloadMethod) { this.showEdit(row, reloadMethod) }, primary: true }
                if (this.actions.delete)
                    actions.delete = { type: 'row', icon: 'delete', click: function (row, reloadMethod) { this.confirmDelete(row, reloadMethod) }, error: true }

                return actions;
            },
            computedContextComponent() {
                return this.contextComponent || this
            }
        },

        methods: {
            load(page, rowsPerPage, filters, orderBy) {
                return new Promise((resolve, reject) => {
                    this.service.search(page, rowsPerPage, filters, orderBy).then(
                        data => { resolve({ items: data.data.items, totalPages: data.data.totalPages }); },
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
