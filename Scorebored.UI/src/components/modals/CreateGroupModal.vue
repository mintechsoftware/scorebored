<template>
  <md-dialog :md-active.sync="show_dialog">
    <md-dialog-title>Create Group</md-dialog-title>
    <md-dialog-content>
      <md-field :class="{'md-invalid': invalid_name}">
        <label>Group Name</label>
        <md-input v-model="name"></md-input>
        <span class="md-error">Invalid name</span>
      </md-field>
    </md-dialog-content>
    <md-dialog-actions>
      <loadable-md-button
        :loading="loading"
        class="md-primary md-raised"
        @click.native="create_group"
      >Save</loadable-md-button>
      <md-button class="md-primary" @click="hide">Cancel</md-button>
    </md-dialog-actions>
  </md-dialog>
</template>

<script>
export default {
  name: 'CreateGroupModal',
  data: () => ({
    loading: false,
    show_dialog: false,
    name: '',
    invalid_name: false
  }),
  methods: {
    show() {
      this.show_dialog = true;
    },
    hide() {
      this.show_dialog = false;
    },
    async create_group() {
      try {
        this.loading = true;
        const response = await this.$store.dispatch('groups/create_group', {
          name: this.name
        });
        this.show_dialog = false;
        this.name = '';
      } catch (error) {
        if (error.response.status === 409) {
          Event.fire('show-snackbar-message', 'Group already exists');
          this.invalid_name = true;
        }
      } finally {
        this.loading = false;
      }
    }
  },
  watch: {
    name() {
      this.invalid_name = false;
    }
  }
};
</script>

<style>
</style>
