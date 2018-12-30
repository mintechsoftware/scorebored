import {
  scoreboard_services
} from '@/services';

const initial_state = {
  group_leaderboard: []
};

export const scoreboards = {
  namespaced: true,
  state: initial_state,
  actions: {
    async get_group_leaderboard({
      rootState,
      commit
    }) {
      try {
        const group_id = rootState.groups.current_group_id;
        if (group_id) {
          const response = await scoreboard_services.get_group_leaderboard(group_id);
          const group_leaderboard = response.data;
          commit('set_group_leaderboard', group_leaderboard);
          return group_leaderboard;
        }
      } catch {
        Event.fire('show-snackbar-message', 'There was an error retrieving the group leaderboard');
      }
    }
  },
  mutations: {
    set_group_leaderboard(state, group_leaderboard) {
      state.group_leaderboard = group_leaderboard;
    }
  }
};
