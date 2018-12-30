<template>
  <md-dialog :md-active.sync="show_dialog">
    <md-dialog-title>Change Groups</md-dialog-title>
    <md-dialog-content>
      <md-tabs>
        <md-tab id="switch-group" md-label="Switch">
          <md-field>
            <label for="group">Groups</label>
            <md-select v-model="group_id" name="group" id="group">
              <md-option
                v-for="group in groups"
                :value="group.group_id"
                :key="group.group_id"
              >{{ group.name}}</md-option>
            </md-select>
          </md-field>
          <div class="button-wrapper">
            <loadable-md-button
              :loading="switch_loading"
              class="md-primary md-raised"
              @click.native="switch_groups"
            >Switch</loadable-md-button>
          </div>
        </md-tab>
        <md-tab id="join-group" md-label="Join">
          <md-field>
            <label>Group Code</label>
            <md-input v-model="group_code"></md-input>
          </md-field>
          <div class="button-wrapper">
            <loadable-md-button
              class="md-raised md-primary"
              @click.native="join_group"
              :loading="join_loading"
            >Join</loadable-md-button>
          </div>
        </md-tab>
        <md-tab id="create-group" md-label="Create">
          <md-field :class="{'md-invalid': invalid_name}">
            <label>Group Name</label>
            <md-input v-model="name"></md-input>
            <span class="md-error">Invalid name</span>
          </md-field>
          <div class="button-wrapper">
            <loadable-md-button
              :loading="create_loading"
              class="md-primary md-raised"
              @click.native="create_group"
            >Create</loadable-md-button>
          </div>
        </md-tab>
      </md-tabs>
    </md-dialog-content>
    <md-dialog-actions>
      <md-button class="md-primary" @click="hide">Cancel</md-button>
    </md-dialog-actions>
  </md-dialog>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'ChangeGroupsModal',
  data: () => ({
    switch_loading: false,
    join_loading: false,
    create_loading: false,
    show_dialog: false,
    group_id: null,
    group_code: '',
    name: '',
    invalid_name: false
  }),
  mounted() {
    this.group_id = this.current_group_id;
  },
  methods: {
    show() {
      this.show_dialog = true;
    },
    hide() {
      this.show_dialog = false;
      this.group_id = this.current_group_id;
    },
    switch_groups() {
      try {
        this.$store.dispatch('groups/update_user_current_group_id', {
          user_id: this.user_id,
          group_id: this.group_id
        });
      } catch {
      } finally {
        this.hide();
      }
    },
    async join_group() {
      try {
        this.join_loading = true;
        const response = await this.$store.dispatch(
          'groups/join_group',
          this.group_code
        );
        this.show_dialog = false;
      } catch (error) {
      } finally {
        this.join_loading = false;
      }
    },
    async create_group() {
      try {
        this.create_loading = true;
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
        this.create_loading = false;
      }
    }
  },
  computed: {
    ...mapState({
      user_id: state => state.users.user.user_id,
      groups: state => state.groups.groups,
      current_group_id: state => state.groups.current_group_id
    })
  },
  watch: {
    name() {
      this.invalid_name = false;
    }
  }
};
</script>
<style lang="scss" scoped>
.button-wrapper {
  text-align: center;
}
</style>
