<template>
  <md-dialog :md-active.sync="show_dialog">
    <md-dialog-title>Join Group</md-dialog-title>
    <md-dialog-content>
      <md-field>
        <label>Group Code</label>
        <md-input v-model="group_code"></md-input>
      </md-field>
      <div class="button-wrapper">
        <loadable-md-button
          class="md-raised md-primary"
          @click.native="join_group"
          :loading="loading"
        >Join</loadable-md-button>
      </div>
    </md-dialog-content>
    <md-dialog-actions>
      <md-button class="md-primary" @click="hide">Cancel</md-button>
    </md-dialog-actions>
  </md-dialog>
</template>

<script>
export default {
  name: 'JoinGroupModal',
  data: () => ({
    loading: false,
    show_dialog: false,
    group_code: ''
  }),
  methods: {
    show() {
      this.show_dialog = true;
    },
    hide() {
      this.show_dialog = false;
    },
    async join_group() {
      try {
        this.loading = true;
        const response = await this.$store.dispatch(
          'groups/join_group',
          this.group_code
        );
        this.show_dialog = false;
        this.group_code = '';
      } catch (error) {
      } finally {
        this.loading = false;
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.button-wrapper {
  text-align: center;
}
</style>
