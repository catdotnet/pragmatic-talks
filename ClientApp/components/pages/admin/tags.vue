<template>
    <div class="page">
        <v-grid-modal-crud-page :columns="columns" :service="service" :actions="actions" :sortBy="sortBy" :searchFilter="searchFilter" :autoSaveState="autoSaveState"></v-grid-modal-crud-page>
    </div>
</template>

<script>
    import vGridPage from '../../common/grid-modal-crud-page'
    import service from '../../../services/tags'

    export default {
        components: { 'v-grid-modal-crud-page': vGridPage },

        data() {
            return {
                columns: [
                    { text: 'name', value: 'name', type: "text", sortable: true, align: 'left', validate: function (val) { return { success: !!val, message: 'The name should not be empty' } } },
                    { text: 'color', value: 'color', type: "select", options: [], optionValue: 'id', optionText: 'name', sortable: true, align: 'left' } 
                ],
                service: service,
                actions: {
                    add: true,
                    edit: true,
                    delete: true
                },
                sortBy: 'name',
                searchFilter: true,
                autoSaveState: false
            }
        },
        mounted() {
            var colors = ['red', 'pink', 'purple', 'deep-purple', 'indigo', 'blue', 'light-blue', 'cyan', 'teal', 'green', 'light-green', 'lime', 'yellow', 'amber', 'orange', 'deep-orange', 'brown', 'blue-grey', 'grey']
            var colorOptions = []
            colors.sort().forEach(color => { colorOptions.push({ id: color, name: color }) })
            this.columns[1].options = colorOptions;
        }
    }
</script>
