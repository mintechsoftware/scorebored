import Vue from 'vue';
import App from './App';
import router from './router';
import 'es6-promise/auto';
import './sass/app.scss';
import {
  store
} from './store';

import 'vue-material/dist/vue-material.css';

import {
  MdButton,
  MdToolbar,
  MdCard,
  MdField,
  MdProgress,
  MdSnackbar,
  MdCheckbox,
  MdDrawer,
  MdApp,
  MdList,
  MdContent,
  MdEmptyState,
  MdDialog,
  MdTooltip,
  MdMenu,
  MdTabs,
  MdTable
} from 'vue-material/dist/components';

Vue.config.productionTip = false;

Vue.use(MdButton);
Vue.use(MdToolbar);
Vue.use(MdCard);
Vue.use(MdField);
Vue.use(MdProgress);
Vue.use(MdSnackbar);
Vue.use(MdCheckbox);
Vue.use(MdDrawer);
Vue.use(MdApp);
Vue.use(MdList);
Vue.use(MdContent);
Vue.use(MdEmptyState);
Vue.use(MdDialog);
Vue.use(MdTooltip);
Vue.use(MdMenu);
Vue.use(MdTabs);
Vue.use(MdTable);

Vue.component('welcome-nav-bar', require('./components/WelcomeNavBar.vue').default);
Vue.component('register-card', require('./components/RegisterCard.vue').default);
Vue.component('login-card', require('./components/LoginCard.vue').default);
Vue.component('loadable-md-button', require('./components/LoadableMdButton.vue').default);
Vue.component('snackbar', require('./components/Snackbar.vue').default);
Vue.component('app-drawer-mobile', require('./components/AppDrawerMobile.vue').default);
Vue.component('app-drawer-desktop', require('./components/AppDrawerDesktop.vue').default);
Vue.component('home-content', require('./components/HomeContent.vue').default);
Vue.component('drawer-list', require('./components/DrawerList.vue').default);
Vue.component('home-empty-state', require('./components/HomeEmptyState.vue').default);

window.Event = new class {
  constructor() {
    this.vue = new Vue();
  }

  fire(event, data = null) {
    this.vue.$emit(event, data);
  }

  listen(event, callback) {
    this.vue.$on(event, callback);
  }
}();

// eslint-disable-next-line no-new
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
