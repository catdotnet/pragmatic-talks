<template>
    <div class="page">
        <v-grid-modal-crud-page :columns="columns" :service="service" :actions="actions" :sortBy="sortBy" :searchFilter="searchFilter"></v-grid-modal-crud-page>
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
                    { text: 'selected', value: 'isSelected', type: "text", sortable: true, align: 'left' },
                    { text: 'deleted', value: 'isDeleted', type: "text", sortable: true, align: 'left' },
                    { text: 'assigned', value: 'isAssignedToEvent', type: "text", sortable: true, align: 'left' },
                    { text: 'speaker', value: 'speakerName', type: "text", sortable: true, align: 'left' },
                ],
                service: service,
                actions: {
                    delete: true
                },
                sortBy: 'title',
                searchFilter: true
            }
        },
        mounted() {
           
        }
    }
</script>
