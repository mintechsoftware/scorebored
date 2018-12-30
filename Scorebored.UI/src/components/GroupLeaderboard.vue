<template>
  <div>
    <md-table md-card table>
      <md-table-toolbar>
        <h1 class="md-title">Group Leaderboard</h1>
      </md-table-toolbar>
      <md-table-row>
        <md-table-head>Name</md-table-head>
        <md-table-head>Record</md-table-head>
        <md-table-head>Win %</md-table-head>
        <md-table-head>Current Streak</md-table-head>
      </md-table-row>
      <md-table-row v-for="(user, key) in group_leaderboard" :key="key">
        <md-table-cell>{{ get_user_display_name(user) }}</md-table-cell>
        <md-table-cell>{{ get_user_display_record(user) }}</md-table-cell>
        <md-table-cell>{{ get_user_display_win_percentage(user) }}</md-table-cell>
        <md-table-cell>{{ get_user_display_streak(user) }}</md-table-cell>
      </md-table-row>
    </md-table>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'GroupLeaderboard',
  methods: {
    get_user_display_name(user) {
      return user.name ? user.name : user.email;
    },
    get_user_display_record(user) {
      return user.wins && user.losses
        ? `${user.wins} - ${user.losses}`
        : '0 - 0';
    },
    get_user_display_win_percentage(user) {
      return user.win_percentage ? user.win_percentage : '-';
    },
    get_user_display_streak(user) {
      return user.current_streak ? user.current_streak : '-';
    }
  },
  computed: {
    ...mapState({
      group_leaderboard: state => state.scoreboards.group_leaderboard
    })
  }
};
</script>

<style lang="scss" scoped>
.md-app-content .md-card {
  margin: 0;
  overflow: scroll;
}
</style>
