<template>
  <md-snackbar
    md-position="center"
    :md-duration="3000"
    :md-active.sync="show_snackbar"
    md-persistent
  >
    <span>{{ message }}</span>
    <md-button class="md-primary" @click="show_snackbar = false">Dismiss</md-button>
  </md-snackbar>
</template>
<script>
export default {
  data() {
    return {
      message: '',
      show_snackbar: false
    };
  },

  mounted() {
    Event.listen('show-snackbar-message', message => {
      this.show_snackbar = true;
      this.message = message;
    });
  }
};
</script>
