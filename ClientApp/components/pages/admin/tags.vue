<template>
    <div>
        <v-grid-modal-crud-page :columns="columns" :service="service" :sortBy="sortBy" :searchFilter="searchFilter"></v-grid-modal-crud-page>
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
                    { text: 'name', value: 'name', type: "text", sortable: true, align: 'left', validate: function(val) { return { success: !!val, message: 'The name should not be empty'}} } 
                ],
                service: service,
                sortBy: 'name',
                searchFilter: false
            }
        }
    }
</script>
