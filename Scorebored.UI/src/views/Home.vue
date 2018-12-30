<template>
  <div class="page-container">
    <app-drawer-mobile :loading="loading" v-if="is_mobile"/>
    <app-drawer-desktop :loading="loading" v-else/>
    <snackbar/>
  </div>
</template>
<script>
import { mapState } from 'vuex';

export default {
  data: () => ({
    loading: false
  }),
  async created() {
    try {
      this.loading = true;
      await Promise.all([
        this.$store.dispatch('groups/get_user_groups'),
        this.$store.dispatch('users/get_user_data')
      ]);
      await this.$store.commit(
        'groups/initialize_selected_group_id',
        this.user
      );
      await this.$store.dispatch('scoreboards/get_group_leaderboard');
    } catch (error) {
      Event.fire(
        'show-snackbar-message',
        'There was an error retrieving user data'
      );
    } finally {
      this.loading = false;
    }
  },
  computed: {
    ...mapState({
      user: state => state.users.user
    }),
    is_mobile() {
      const w = window;
      const d = document;
      const e = d.documentElement;
      const g = d.getElementsByTagName('body')[0];

      return Math.min(w.innerWidth, e.clientWidth, g.clientWidth) < 600;
    }
  }
};
</script>
