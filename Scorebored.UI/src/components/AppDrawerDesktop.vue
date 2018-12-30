<template>
  <md-app>
    <md-app-toolbar class="md-accent" md-elevation="2">
      <span class="md-title">Scorebored</span>
      <md-button class="md-primary md-raised" @click="logout">Logout</md-button>
    </md-app-toolbar>

    <md-app-drawer md-permanent="clipped">
      <drawer-list/>
    </md-app-drawer>
    <md-app-content>
      <md-progress-spinner md-mode="indeterminate" class="loading-spinner" v-if="loading"/>
      <router-view v-else/>
    </md-app-content>
  </md-app>
</template>
<script>
import { MdProgress } from 'vue-material/dist/components';

export default {
  components: {
    MdProgress
  },
  props: {
    loading: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    async logout() {
      const { dispatch } = this.$store;
      await dispatch('users/logout');
    }
  }
};
</script>
<style lang="scss" scoped>
.md-title {
  flex: 1;
}
.loading-spinner {
  top: 40%;
  left: calc(50% - 24px);
  position: relative;
}
</style>
