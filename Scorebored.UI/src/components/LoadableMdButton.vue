<style lang="scss" scoped>
.spinner {
  margin-top: 3px;
}
</style>
<template>
  <md-button>
    <div v-if="loading">
      <md-progress-spinner
        class="spinner"
        :md-diameter="24"
        :md-stroke="3"
        md-mode="indeterminate"
      />
    </div>
    <slot v-else/>
  </md-button>
</template>
<script>
export default {
  props: {
    loading: {
      type: Boolean,
      default: false
    }
  }
};
</script>
