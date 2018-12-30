<template>
  <div>
    <md-empty-state
      md-icon="people_outline"
      md-label="Create your own group"
      md-description="Looks like your not a part of a group yet! You can either create your own group or join an existing one."
      class="md-accent"
    >
      <div>
        <md-button class="md-primary md-raised" @click="showCreateDialog">Create</md-button>
        <md-button class="md-primary md-raised" @click="showJoinDialog">Join</md-button>
      </div>
    </md-empty-state>
    <create-group-modal ref="createGroupModal"/>
    <join-group-modal ref="joinGroupModal"/>
  </div>
</template>

<script>
import CreateGroupModal from '@/components/modals/CreateGroupModal';
import JoinGroupModal from '@/components/modals/JoinGroupModal';

export default {
  name: 'HomeEmptyState',
  components: {
    CreateGroupModal,
    JoinGroupModal
  },
  methods: {
    showCreateDialog() {
      this.$refs.createGroupModal.show();
    },
    showJoinDialog() {
      this.$refs.joinGroupModal.show();
    }
  }
};
</script>

<style lang="scss" scoped>
.md-button {
  margin: 10px;
}
</style>
