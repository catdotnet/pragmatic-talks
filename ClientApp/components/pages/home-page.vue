<template>
  <div id="home" class="page">
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-list two-line subheader>
            <v-subheader inset  v-locale="'Promoted'"></v-subheader>
            <v-list-tile avatar v-for="item in items" v-bind:key="item.title" @click="">
              <v-list-tile-avatar>
                <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
              </v-list-tile-avatar>
              <v-list-tile-content>
                  <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                  <div><v-chip v-for="tag in item.tags" :key="tag" class="green white--text">{{ tag }}</v-chip></div>
            </v-list-tile>
            <v-divider inset></v-divider>
            <v-subheader inset v-locale="'Stack'"></v-subheader>
            <v-list-tile v-for="item in items2" v-bind:key="item.title" avatar @click="">
              <v-list-tile-avatar>
                <v-icon v-bind:class="[item.iconClass]">{{ item.icon }}</v-icon>
              </v-list-tile-avatar>
              <v-list-tile-content>
                  <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                  <div><v-chip v-for="tag in item.tags" :key="tag" class="green white--text">{{ tag }}</v-chip></div>
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
    import service from '../../services/talks'

    export default {
        
        data() {
            return {
                items: [],
                items2: []
            }
        },

        methods: {
            ...mapActions(['loading'])
        },

        mounted() {
            this.loading(true)
            var items = [], items2 = [];
            service.get().then(response => {
                response.data.forEach(item => {
                    if (item.isSelected) items.push({ icon: 'star', iconClass: 'amber lighten-1 white--text', title: item.title, tags: item.tags })
                    else items2.push({ icon: 'lightbulb_outline', iconClass: 'cyan white--text', title: item.title, tags: item.tags })
                })

                if (items.length < 5) {
                    new Array(5 - items.length).forEach(_ => {
                        items.push({ icon: 'access_time', iconClass: 'grey lighten-1 white--text', title: 'Unasigned...' })
                    });
                }

                this.items = items
                this.items2 = items2
                this.loading(false)
            }).catch(error => {
                this.loading(false)
            });
        }
    }
</script>

<style>
    .chip {
        font-size: 10px;
        height: 24px;
    }

    .list__tile {
        font-size: 18px;
    }
</style>