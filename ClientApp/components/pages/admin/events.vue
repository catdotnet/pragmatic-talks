<template>
    <div class="page">
        <v-grid-modal-crud-page :columns="columns" :service="service" :actions="actions" :sortBy="sortBy" :searchFilter="searchFilter"></v-grid-modal-crud-page>
    </div>
</template>

<script>
    import vGridPage from '../../common/grid-modal-crud-page'
    import service from '../../../services/events'

    export default {
        components: { 'v-grid-modal-crud-page': vGridPage },

        data() {
            return {
                columns: [
                    { text: 'name', value: 'name', type: "text", sortable: true, align: 'left' },
                    { text: 'date', value: 'date', type: "date", sortable: false, align: 'left' },
                    { text: 'url', value: 'url', type: "text", sortable: true, align: 'left' },
                    { text: 'talks', value: 'talks', type: "list", sortable: false, align: 'left' }
                ],
                service: service,
                actions: {
                    add: true,
                    edit: true,
                    delete: true
                },
                sortBy: 'name',
                searchFilter: true
            }
        },

        mounted() {
           
        }
    }
</script>
