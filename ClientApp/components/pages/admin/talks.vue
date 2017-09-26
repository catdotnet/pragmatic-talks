<template>
    <div class="page">
        <v-grid-modal-crud-page :columns="columns" :service="service" :actions="actions" :sortBy="sortBy" :searchFilter="searchFilter" :autoSaveState="autoSaveState"></v-grid-modal-crud-page>
    </div>
</template>

<script>
    import vGridPage from '../../common/grid-modal-crud-page'
    import service from '../../../services/talks'

    export default {
        components: { 'v-grid-modal-crud-page': vGridPage },

        data() {
            return {
                columns: [
                    { text: 'title', value: 'title', type: "text", sortable: true, align: 'left'},
                    { text: 'date', value: 'dateCreation', type: "text", sortable: true, align: 'left' },
                    { text: 'selected', value: 'isSelected', type: "checkbox", sortable: true, align: 'left', onChange: this.selectedChange },
                    { text: 'assigned', value: 'isAssignedToEvent', type: "text", sortable: true, align: 'left' },
                    { text: 'speaker', value: 'speakerName', type: "text", sortable: true, align: 'left' },
                ],
                service: service,
                actions: {
                    delete: true
                },
                sortBy: 'title',
                searchFilter: true,
                autoSaveState: false
            }
        },

        methods: {
            selectedChange(item, value, reload) {
                if (item.isAssignedToEvent) return false;
                var call = value ? service.select : service.unselect
                call(item).then(response => {
                }).catch(error => { reload() })
            }
        },

        mounted() {
           
        }
    }
</script>
