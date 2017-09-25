<template>
    <div class="page">
        <v-grid-modal-crud-page :columns="columns" :service="service" :actions="actions" :sortBy="sortBy" :searchFilter="searchFilter"></v-grid-modal-crud-page>
    </div>
</template>

<script>
    import vGridPage from '../../common/grid-modal-crud-page'
    import service from '../../../services/speakers'

    export default {
        components: { 'v-grid-modal-crud-page': vGridPage },

        data() {
            return {
                columns: [
                    { text: '', value: 'avatarUrl', type: "image", sortable: true, align: 'left' },
                    { text: 'name', value: 'displayName', type: "text", sortable: true, align: 'left' },
                    { text: 'email', value: 'email', type: "text", sortable: true, align: 'left' },
                    { text: 'admin', value: 'isAdministrator', type: "checkbox", sortable: true, align: 'left', onChange: this.adminChange },
                    { text: 'blocked', value: 'isBlocked', type: "checkbox", sortable: true, align: 'left', onChange: this.blockedChange },
                    { text: 'talks', value: 'talksCounter', type: "text", sortable: true, align: 'left' },
                ],
                service: service,
                actions: {
                    delete: true
                },
                sortBy: 'displayName',
                searchFilter: true
            }
        },

        methods: {
            adminChange(item, value, reload) {
                var call = value ? service.makeAdmin : service.unmakeAdmin
                call(item).then(response => {
                }).catch(error => { reload() })
            },
            blockedChange(item, value, reload) {
                var call = value ? service.block : service.unblock
                call(item).then(response => {
                }).catch(error => { reload() })
            }
        },

        mounted() {
           
        }
    }
</script>
