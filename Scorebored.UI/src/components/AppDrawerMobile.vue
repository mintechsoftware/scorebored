<template>
  <md-app>
    <md-app-toolbar class="md-accent">
      <md-button class="md-icon-button" @click="toggle_menu" v-if="!menu_visible">
        <md-icon>menu</md-icon>
      </md-button>
      <span class="md-title">Scorebored</span>
    </md-app-toolbar>
    <md-app-drawer :md-active.sync="menu_visible" md-persistent="full">
      <md-toolbar class="md-transparent" md-elevation="0">
        <span>Navigation</span>

        <div class="md-toolbar-section-end">
          <md-button class="md-icon-button md-dense" @click="toggle_menu">
            <md-icon>keyboard_arrow_left</md-icon>
          </md-button>
        </div>
      </md-toolbar>
      <drawer-list :show-logout="true"/>
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
  data: () => ({
    menu_visible: false
  }),

  methods: {
    toggle_menu() {
      this.menu_visible = !this.menu_visible;
    }
  }
};
</script>
<style lang="scss" scoped>
.loading-spinner {
  top: 40%;
  left: calc(50% - 24px);
  position: relative;
}
</style>
