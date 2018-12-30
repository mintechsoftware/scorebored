<style lang="scss">
@import '@/sass/_variables.scss';

#login {
  height: 100vh;
  width: 100vw;
  background-color: $secondary-color;
  text-align: center;
  padding: 10px;
}

.md-layout {
  height: 100%;
}
</style>
<template>
  <div id="login">
    <div class="md-layout md-alignment-center">
      <login-card></login-card>
    </div>
    <snackbar></snackbar>
  </div>
</template>
<script>
export default {};
</script>
