<template>
  <div>
    <md-content class="md-elevation-1 title-wrapper">
      <h1 class="group-title">{{ group_name }}</h1>
      <span class="icon-wrapper">
        <md-icon @click.native="open_add_user_modal">person_add</md-icon>
        <md-tooltip>Add User</md-tooltip>
      </span>
      <span class="icon-wrapper">
        <md-icon @click.native="open_change_groups_modal">swap_horiz</md-icon>
        <md-tooltip>Change Groups</md-tooltip>
      </span>
    </md-content>
    <group-leaderboard/>
    <change-groups-modal ref="change_groups_modal"/>
    <add-user-modal ref="add_user_modal"/>
  </div>
</template>
<script>
import ChangeGroupsModal from '@/components/modals/ChangeGroupsModal';
import AddUserModal from '@/components/modals/AddUserModal';
import GroupLeaderboard from '@/components/GroupLeaderboard';
import { mapGetters } from 'vuex';

export default {
  components: {
    ChangeGroupsModal,
    AddUserModal,
    GroupLeaderboard
  },
  computed: {
    ...mapGetters({
      group_name: 'groups/group_name'
    })
  },
  methods: {
    open_change_groups_modal() {
      this.$refs.change_groups_modal.show();
    },
    open_add_user_modal() {
      this.$refs.add_user_modal.show();
    }
  }
};
</script>
<style lang="scss" scoped>
@import '@/sass/_variables.scss';

.md-content {
  padding: 4px 16px;
  display: flex;
  flex-direction: row;
}
.group-title {
  flex: 1;
  margin: 14px 6px;
}

.md-icon:hover {
  color: $primary-color;
  cursor: pointer;
}

.icon-wrapper {
  display: flex;
  margin: 2px;
}

.title-wrapper {
  border-radius: 2px;
  margin-bottom: 16px;
}
</style>
