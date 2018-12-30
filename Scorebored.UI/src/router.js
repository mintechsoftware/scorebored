import Vue from 'vue';
import Router from 'vue-router';
import Welcome from '@/views/Welcome';
import Login from '@/views/Login';
import Home from '@/views/Home';
import Register from '@/views/Register';
import HomeContent from '@/components/HomeContent';
import ScoreboardsContent from '@/components/ScoreboardsContent';
import SettingsContent from '@/components/SettingsContent';
import BillingContent from '@/components/BillingContent';

Vue.use(Router);

export default new Router({
  routes: [{
    path: '/',
    name: 'Welcome',
    component: Welcome
  }, {
    path: '/login',
    name: 'Login',
    component: Login
  }, {
    path: '/home',
    name: 'Home',
    component: Home,
    children: [{
      path: '',
      component: HomeContent
    }, {
      path: 'scoreboards/*',
      component: ScoreboardsContent
    }, {
      path: 'settings',
      component: SettingsContent
    }, {
      path: 'billing',
      component: BillingContent
    }]
  }, {
    path: '/register',
    name: 'Register',
    component: Register
  }],
  mode: 'history'
});
