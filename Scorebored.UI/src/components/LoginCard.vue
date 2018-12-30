<style lang="scss" scoped>
#login-card-wrapper {
  max-width: 600px;
  width: 600px;
}
</style>
<template>
  <div id="login-card-wrapper">
    <md-card class="md-elevation-5">
      <md-card-header>
        <div class="md-title">Log In</div>
      </md-card-header>
      <md-card-content>
        <md-field>
          <label>Email Address</label>
          <md-input :value="email" @input="update_email"></md-input>
        </md-field>
        <md-field>
          <label>Password</label>
          <md-input type="password" v-model="password"></md-input>
        </md-field>
        <md-checkbox>Remember Me</md-checkbox>
        <div class="row">
          <loadable-md-button
            class="md-raised md-primary"
            :loading="is_logging_in"
            @click.native="login"
          >Log In</loadable-md-button>
        </div>
      </md-card-content>
    </md-card>
  </div>
</template>
<script>
import { mapState } from 'vuex';
export default {
  data: () => ({
    password: ''
  }),

  methods: {
    async login() {
      const { dispatch } = this.$store;
      const { email, password } = this;
      await dispatch('users/login', { email, password });
    },

    update_email(value) {
      this.$store.commit('users/update_email', value);
    }
  },

  computed: {
    ...mapState({
      email: state => {
        return state.users.user.email;
      }
    }),

    is_logging_in() {
      return this.$store.state.users.status.logging_in;
    }
  }
};
</script>
