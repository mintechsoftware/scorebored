<template>
  <div>
    <home-empty-state v-if="!current_group_id"/>
    <home-board v-else/>
  </div>
</template>
<script>
import HomeBoard from '@/components/HomeBoard';
import { mapState, mapGetters } from 'vuex';

export default {
  components: {
    HomeBoard
  },
  computed: {
    ...mapState({
      current_group_id: state => state.groups.current_group_id
    })
  }
};
</script>
