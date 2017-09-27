<template>
    <div id="rules" class="page container fluid grid-list-md">
        <v-layout row wrap>
            <v-flex xs12 sm12 md6 lg6 xl6>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'What'"></span>
                        </v-card-title>
                        <v-card-text>
                            <p><b>Pragmatic Talks</b> es una iniciativa de <a href="http://catdotnet.net" target="_blank">CatdotNet</a>, una asociación resultado de la suma de esfuerzos de diferentes grupos de usuarios de .NET, unidos con el propósito de aprender y compartir.</p>
                            <p>Las ideas principales son aprender, charlar y compartir. Bajo ese lema abrimos a todos los apasionados por la programación la posibilidad de participar activamente en esta serie de eventos periodicos.</p>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-text>
                            <v-parallax src="/images/picture2.jpeg">
                                <v-layout column align-center justify-center>
                                    <h4 class="white--text" v-locale="'Learn, Talk, Share'"></h4>
                                </v-layout>
                            </v-parallax>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'How'"></span>
                        </v-card-title>
                        <v-card-text>
                            <p>
                                El funcionamiento es simple:
                                <ul>
                                    <li>Cualquiera puede proner una charla. Lo único que te pedimos es que la temática sea el desarrollo de software.</li>
                                    <li>La charla durará como máximo 15 minutos.</li>
                                    <li>El algoritmo de esta web ordenará las charlas y elegirá las 5 primeras.</li>
                                    <li>Un administrador se pondrá en contacto con los diferentes ponentes para avisarles de una fecha libre para montar el evento.</li>
                                    <li>Una vez todo esté confirmado se organizará el evento y se anunciará en esta web y en meetup.</li>
                                </ul>
                            </p>
                        </v-card-text>
                    </v-card>
                </v-flex>
            </v-flex>
            <v-flex xs12 sm12 md6 lg6 xl6>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-text>
                            <v-parallax src="/images/picture1.jpeg">
                                <v-layout column align-center justify-center>
                                    <h1 class="white--text" v-locale="'About'"></h1>
                                </v-layout>
                            </v-parallax>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'Rules'"></span>
                        </v-card-title>
                        <v-card-text>
                            <ol>
                                <li v-for="(o, index) in Array(11)" :key="index" v-locale="'rule' + ('0' + (index + 1)).slice(-2)"></li>
                            </ol>
                        </v-card-text>
                    </v-card>
                </v-flex>
                <v-flex xs12 sm12>
                    <v-card>
                        <v-card-title>
                            <span class="headline" v-locale="'Why'"></span>
                        </v-card-title>
                        <v-card-text>
                            <p>
                                Creemos que hay muchas personas que sienten verdadera pasión por el mundo del desarrollo y con ganas de compartir y participar en la comunidad. Pragmatic Talks pretende responder a tres necesidades que hemos encontrado:
                                <ul>
                                    <li>Que las charlas sean productivas. Proponemos charlas cortas para poder dar una presentación de un solo tema. A partir de ahí cada uno es libre de interiorizar más en él y así poder generar debate.</li>
                                    <li>Que todo el mundo tenga acceso. Varias personas nos han hecho llegar sus ganas de participar en las comunidades técnicas. En Pragmatic Talks, no solo podrán participar, si no que el sistema da  les da prioridad.</li>
                                    <li>Que podamos compartir entre todos nuestros gustos, opiniones y experiencia.</li>
                                </ul>
                            </p>
                        </v-card-text>
                    </v-card>
                </v-flex>
            </v-flex>
        </v-layout>
    </div>
</template>

<script>
    export default {

        data() {
            return {

            }
        }
    }
</script>

<style>
</style>