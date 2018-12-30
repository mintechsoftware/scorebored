<template>
  <md-dialog :md-active.sync="show_dialog">
    <md-dialog-title>Add User to Group</md-dialog-title>
    <md-dialog-content>
      <md-tabs>
        <md-tab id="tab-send-email" md-label="Send Email">
          <md-field>
            <label>Email</label>
            <md-input v-model="email"></md-input>
          </md-field>
          <div class="button-wrapper">
            <md-button class="md-raised md-primary">Send</md-button>
          </div>
        </md-tab>
        <md-tab id="tab-get-code" md-label="Get Code">
          <p>To generate a code that another user can use to join your group, press the button below.</p>
          <div class="button-wrapper">
            <loadable-md-button
              class="md-raised md-primary"
              @click.native="generate_code"
              :loading="loading"
            >Get Code</loadable-md-button>
          </div>
          <p id="generated-code" v-if="generated_code">
            Code:
            <b>{{generated_code }}</b>
          </p>
        </md-tab>
      </md-tabs>
    </md-dialog-content>
    <md-dialog-actions>
      <md-button class="md-primary" @click="hide">Cancel</md-button>
    </md-dialog-actions>
  </md-dialog>
</template>

<script>
import { group_services } from '@/services';
import { mapState } from 'vuex';

export default {
  name: 'AddUserModal',
  data: () => ({
    loading: false,
    show_dialog: false,
    email: '',
    generated_code: ''
  }),
  methods: {
    show() {
      this.show_dialog = true;
    },
    hide() {
      this.show_dialog = false;
    },
    add_user() {},
    async generate_code() {
      try {
        this.loading = true;
        const response = await group_services.generate_code(
          this.user_id,
          this.group_id
        );
        this.generated_code = response.data.code;
      } catch (error) {
        Event.fire(
          'show-snackbar-message',
          'There was an error generating a code'
        );
      } finally {
        this.loading = false;
      }
    }
  },
  computed: {
    ...mapState({
      user_id: state => state.users.user.user_id,
      group_id: state => state.groups.current_group_id
    })
  }
};
</script>
<style lang="scss" scoped>
.md-dialog {
  width: 500px;
  min-height: 300px;
}
.button-wrapper {
  text-align: center;
}
</style>
