import {
  group_services
} from '@/services';

const initial_state = {
  groups: [],
  status: {
    groups_loading: false
  },
  current_group_id: null
};

export const groups = {
  namespaced: true,
  state: initial_state,
  getters: {
    group_name: state => state.current_group_id ? state.groups.find(group => group.group_id === state.current_group_id).name : ''
  },
  actions: {
    async create_group({
      commit,
      dispatch,
      rootState
    }, {
      name
    }) {
      commit('create_group_request');
      try {
        const response = await group_services.create_group(name);
        commit('create_group_success');
        await dispatch('get_user_groups');
        commit('set_current_group_id', response.data.group_id);
        return response.data;
      } catch (error) {
        commit('create_group_failure', error);
        throw error;
      }
    },
    async get_user_groups({
      commit,
      rootState
    }) {
      try {
        commit('get_user_groups_request');
        const response = await group_services.get_user_groups(rootState.users.user.user_id);
        const user_groups = response.data.groups;
        commit('get_user_groups_success', user_groups);
        return user_groups;
      } catch (error) {
        commit('get_user_groups_failure', error);
        throw error;
      } finally {
        commit('set_groups_loading', false);
      }
    },
    update_user_current_group_id({
      commit
    }, {
      user_id,
      group_id
    }) {
      try {
        commit('set_current_group_id', group_id);
        group_services.update_user_current_group_id(user_id, group_id);
      } catch (error) {

      }
    },
    async join_group({
      commit,
      rootState,
      dispatch
    }, code) {
      try {
        const response = await group_services.join_group(rootState.users.user.user_id, code);
        await dispatch('get_user_groups');
        commit('set_current_group_id', response.data.group_id);
        group_services.update_user_current_group_id(rootState.users.user.user_id, response.data.group_id);
        return response.data;
      } catch (error) {
        throw error;
      }
    }
  },
  mutations: {
    create_group_request() {},
    create_group_success() {},
    create_group_failure() {},
    get_user_groups_request(state) {
      state.status.groups_loading = true;
    },
    get_user_groups_success(state, groups) {
      state.groups = groups;
    },
    get_user_groups_failure() {},
    set_groups_loading(state, loading) {
      state.status.groups_loading = loading;
    },
    initialize_selected_group_id(state, user) {
      if (!user.current_group_id && state.groups.length) {
        state.current_group_id = state.groups[0].group_id;
      } else if (user.current_group_id && state.groups.find(g => g.group_id === user.current_group_id)) {
        state.current_group_id = user.current_group_id;
      } else if (state.groups.length) {
        state.current_group_id = state.groups[0].group_id;
      }
    },
    set_current_group_id(state, group_id) {
      state.current_group_id = group_id;
    }
  }
};
