<style lang="scss" scoped>
.md-layout {
  height: calc(100vh - 64px);
  flex-direction: row;
}

#register-wrapper {
  padding: 0 5%;
  text-align: center;
}
</style>
<template>
  <div>
    <welcome-nav-bar></welcome-nav-bar>
    <div class="md-layout md-alignment-center">
      <div class="md-layout-item md-small-size-50 md-xsmall-size-100"></div>
      <div id="register-wrapper" class="md-layout-item md-small-size-50 md-xsmall-size-100">
        <register-card></register-card>
      </div>
    </div>
    <snackbar></snackbar>
  </div>
</template>
