import {
  user_services
} from '../services';
import router from '../router';

const user = JSON.parse(localStorage.getItem('user'));
const initial_state =
  user && user.access_token ? {
    status: {
      logged_in: true
    },
    user
  } : {
    status: {},
    user: {
      email: ''
    }
  };

export const users = {
  namespaced: true,
  state: initial_state,
  actions: {
    async register({
      commit
    }, {
      email,
      password
    }) {
      commit('register_request');

      try {
        const response = await user_services.register(email, password);
        if (!response.data) {
          throw new Error();
        }
        localStorage.setItem('user', JSON.stringify(response.data));
        commit('register_success', response.data);
        router.push({
          path: '/home'
        });
      } catch (error) {
        commit('register_failure', error);
      }
    },
    async login({
      commit
    }, {
      email,
      password
    }) {
      commit('login_request');
      try {
        const user = await user_services.login(email, password);
        commit('login_success', user);
        router.push({
          path: 'home'
        });
      } catch (error) {
        Event.fire('show-snackbar-message', 'Invalid login');
        commit('login_failure', error);
      }
    },
    logout({
      commit
    }) {
      user_services.logout();
      commit('logout');
      router.push({
        path: '/'
      });
    },
    async get_user_data({
      commit,
      state
    }) {
      const response = await user_services.get_user_data(state.user.user_id);
      commit('set_user_data', {
        email: response.data.email,
        current_group_id: response.data.current_group_id
      });
    }
  },
  mutations: {
    update_email(state, email) {
      state.user.email = email;
    },
    update_password(state, password) {
      state.user.password = password;
    },
    register_request(state) {
      state.status = {
        registering: true
      };
    },
    register_success(state, user) {
      state.status = {
        logged_in: true
      };
      state.user = user;
    },
    register_failure(state) {
      state.status = {};
      state.user = null;
    },
    logout(state) {
      state.status = {};
      state.user.access_token = null;
      state.user.user_id = null;
    },
    login_request(state) {
      state.status = {
        logging_in: true
      };
    },
    login_success(state, user) {
      state.status = {
        logged_in: true
      };
      state.user = user;
    },
    login_failure(state) {
      state.status = {};
      state.user = {
        email: state.user.email,
        password: null,
        access_token: null
      };
    },
    set_user_data(state, user) {
      state.user.email = user.email;
      state.user.current_group_id = user.current_group_id;
    }
  }
};
