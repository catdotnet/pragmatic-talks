<template>
    <div id="login">
        <v-layout row justify-center>
            <v-dialog v-model="dialog" persistent>
                <v-card>
                    <v-card-title>
                        <span class="headline">User login</span>
                    </v-card-title>
                    <v-card-text>
                        <v-text-field label="Email" v-model="email" required></v-text-field>
                        <v-text-field label="Password" v-model="password" type="password" required></v-text-field>
                        <small>*indicates required field</small>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn class="blue--text darken-1" flat @click.native="onLogin">Login</v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-layout>
    </div>
</template>

<script>
    import { mapActions, mapState } from 'vuex'

    export default {
        
        data() {
            return {
                dialog: true,
                email: '',
                password: ''
            }
        },

        methods: {
            ...mapActions(['loading', 'auth/login']),
            ...mapActions(['login'], 'auth'),
            onLogin() {
                this.login({ username: this.email, password: this.password }).then(() => {
                    this.dialog = false;
                    this.$router.push({ path: '/' })
                })
            }
        },

        computed: {
            ...mapState({
                isAuthenticated: state => state.auth.isAuthenticated
            })
        },

        mounted() {
            if (this.isAuthenticated) {
                this.$router.push({ path: '/' });
            }
        }
    }
</script>

<style>
</style>