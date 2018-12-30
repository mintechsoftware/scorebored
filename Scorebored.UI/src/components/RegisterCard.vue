<style lang="scss" scoped>
#register-card {
  max-width: 600px;
}
</style>
<template>
  <div>
    <md-card id="register-card" class="md-elevation-5">
      <md-card-header>
        <div class="md-title">Sign up and begin keeping score immediately!</div>
      </md-card-header>
      <md-card-content>
        <md-field :class="{ 'md-invalid': !email_is_valid }">
          <label>Email Address</label>
          <md-input @blur="validate_email" @focus="email_is_valid = true" v-model="email"></md-input>
          <span class="md-error">{{ invalid_email_message }}</span>
        </md-field>
        <md-field :class="{ 'md-invalid': !password_is_valid }">
          <label>Password</label>
          <md-input
            type="password"
            @blur="validate_password"
            @focus="password_is_valid = true"
            v-model="password"
          ></md-input>
          <span class="md-error">{{ invalid_password_message }}</span>
        </md-field>
        <div class="row">
          <loadable-md-button
            class="md-raised md-primary"
            :loading="is_registering"
            @click.native="sign_up"
            :on-click="sign_up"
          >Sign Up</loadable-md-button>
        </div>By signing up you agree to the terms of service
      </md-card-content>
    </md-card>
    <md-snackbar
      md-position="center"
      :md-duration="3000"
      :md-active.sync="show_snackbar"
      md-persistent
    >
      <span>{{ snackbar_message }}</span>
      <md-button class="md-primary" @click="show_snackbar = false">Dismiss</md-button>
    </md-snackbar>
  </div>
</template>
<script>
export default {
  data() {
    return {
      email: '',
      password: '',
      email_is_valid: true,
      password_is_valid: true,
      show_snackbar: false,
      snackbar_message: ''
    };
  },

  methods: {
    async sign_up() {
      this.validate_email();
      this.validate_password();

      if (this.email_is_valid && this.password_is_valid) {
        const { dispatch } = this.$store;
        const { email, password } = this;
        await dispatch('users/register', { email, password });
      }
    },

    validate_email() {
      var email_regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

      this.email_is_valid = email_regex.test(String(this.email).toLowerCase());
    },

    validate_password() {
      this.password_is_valid = this.password.length >= 7;
    }
  },

  watch: {
    email() {
      if (!this.email_is_valid) {
        this.validate_email();
      }
    },

    password() {
      if (!this.password_is_valid) {
        this.validate_password();
      }
    }
  },

  computed: {
    invalid_email_message() {
      return this.email.length ? 'Invalid email' : 'Email required';
    },

    invalid_password_message() {
      return this.password.length
        ? 'Passwords must be at least 7 characters'
        : 'Password required';
    },

    is_registering() {
      return this.$store.state.users.status.registering;
    }
  }
};
</script>
