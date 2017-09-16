<template>
  <div id="home">
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-list two-line subheader>
            <v-subheader inset>Next event</v-subheader>
            <v-list-tile avatar v-for="item in items" v-bind:key="item.title" @click="">
              <v-list-tile-avatar>
                <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                <v-list-tile-sub-title>karma: {{ item.subtitle }}</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-divider inset></v-divider>
            <v-subheader inset>Stack</v-subheader>
            <v-list-tile v-for="item in items2" v-bind:key="item.title" avatar @click="">
              <v-list-tile-avatar>
                <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                <v-list-tile-sub-title>karma: {{ item.subtitle }}</v-list-tile-sub-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
          <v-card-actions class="white">
                <v-spacer></v-spacer>
                <v-btn floating fab dark router to="/create" success absolute top right>
                    <v-icon>add</v-icon>
                </v-btn>
            </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
    import { mapActions } from 'vuex'

    export default {
        
        data() {
            return {
                items: [
                    { icon: 'star', iconClass: 'amber lighten-1 white--text', title: 'Lorem ipsum sit amen', subtitle: '450' },
                    { icon: 'star', iconClass: 'amber lighten-1 white--text', title: 'Lorem ipsum sit amen', subtitle: '440' },
                    { icon: 'star', iconClass: 'amber lighten-1 white--text', title: 'Lorem ipsum sit amen', subtitle: '432' },
                    { icon: 'access_time', iconClass: 'grey lighten-1 white--text', title: 'Unasigned...', subtitle: '0' },
                    { icon: 'access_time', iconClass: 'grey lighten-1 white--text', title: 'Unasigned...', subtitle: '0' }
                ],
                items2: [
                    { icon: 'lightbulb_outline', iconClass: 'cyan white--text', title: 'Lorem ipsum sit amen', subtitle: '320' },
                    { icon: 'lightbulb_outline', iconClass: 'cyan white--text', title: 'Lorem ipsum sit amen', subtitle: '300' },
                ]
            }
        },

        methods: {
            ...mapActions(['loading'])
        },

        mounted() {
            this.loading(false)
        }
    }
</script>

<style>
#home {
  margin-top: 24px;
}
</style>