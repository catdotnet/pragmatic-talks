import HomePage from 'components/pages/home-page'
import CreatePage from 'components/pages/create-page'
import RulesPage from 'components/pages/rules-page'

const ContainerPage = {
    template: '<router-view></router-view>'
}

export const routes = [
    { path: '/', name: 'root', component: ContainerPage, children: [
        { path: '', name: 'home', component: HomePage, meta: { display: 'Home', icon: 'home' } },
        { path: 'ranking', name: 'ranking', component: HomePage, meta: { display: 'Home', icon: 'home' } },
        { path: 'create', name: 'create', component: CreatePage, meta: { display: 'Create', icon: 'home' } },
        { path: 'rules', name: 'rules', component: RulesPage, meta: { display: 'Rules', icon: 'home' } }
    ]}
]