import HomePage from 'components/pages/home-page'
import LoginPage from 'components/pages/login-page'
import CreatePage from 'components/pages/create-page'

const ContainerPage = {
    template: '<router-view></router-view>'
}

export const routes = [
    { path: '/', name: 'home', component: HomePage, meta: { display: 'Home', icon: 'home' } },
    { path: '/login', name: 'login', component: LoginPage, meta: { display: 'Login', icon: 'home' } },
    { path: '/create', name: 'create', component: CreatePage, meta: { display: 'Create', icon: 'home' } }
]