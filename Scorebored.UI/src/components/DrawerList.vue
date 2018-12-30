<style lang="scss" scoped>
#add-icon {
  margin-right: 10px;
}
</style>

<template>
  <md-list>
    <router-link to="/home">
      <md-list-item>
        <md-icon>home</md-icon>
        <span class="md-list-item-text">Home</span>
      </md-list-item>
    </router-link>
    <md-list-item md-expand :md-expanded.sync="expand">
      <md-icon>dashboard</md-icon>
      <span class="md-list-item-text">Scoreboards</span>
      <md-list slot="md-expand">
        <router-link to="/home/scoreboards/world">
          <md-list-item class="md-inset">
            <span class="md-list-item-text">World</span>
          </md-list-item>
        </router-link>
        <md-list-item class="md-inset">
          <span class="md-list-item-text">Europe</span>
        </md-list-item>
        <md-list-item class="md-inset">
          <span class="md-list-item-text">South America</span>
        </md-list-item>
        <md-list-item class="md-inset">
          <span class="md-list-item-text">Add a Scoreboard</span>
          <md-icon class="add-icon">add</md-icon>
        </md-list-item>
      </md-list>
    </md-list-item>
    <router-link to="/home/settings">
      <md-list-item>
        <md-icon>settings</md-icon>
        <span class="md-list-item-text">Settings / Profile</span>
      </md-list-item>
    </router-link>
    <router-link to="/home/billing">
      <md-list-item>
        <md-icon>credit_card</md-icon>
        <span class="md-list-item-text">Billing</span>
      </md-list-item>
    </router-link>
    <md-list-item @click="logout" v-if="showLogout">
      <md-icon>arrow_back</md-icon>
      <span class="md-list-item-text">Logout</span>
    </md-list-item>
  </md-list>
</template>
<script>
export default {
  props: {
    showLogout: {
      type: Boolean,
      default: false
    }
  },
  data: () => ({
    expand: true
  }),
  methods: {
    async logout() {
      await this.$store.dispatch('users/logout');
    }
  }
};
</script>
<style lang="scss" scoped>
.router-link-exact-active {
  background-color: #eee;
}
a:hover {
  background-color: lightgrey;
}
</style>
