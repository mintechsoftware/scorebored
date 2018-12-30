<style lang="scss">
@import '@/sass/_variables.scss';

#register {
  height: 100vh;
  width: 100vw;
  background-color: $secondary-color;
  text-align: center;
  padding: 10px;
}

.md-layout {
  height: 100%;
  flex-direction: column;
}

.row {
  margin-top: 10px;
  color: white;
}

#login-link {
  color: white;
  text-decoration: underline;
}
</style>
<template>
  <div id="register">
    <div class="md-layout md-alignment-center">
      <register-card></register-card>
      <div class="row">
        <span>
          Already have an account?
          <a href="login" id="login-link">Login</a>
        </span>
      </div>
    </div>
  </div>
</template>
<script>
export default {};
</script>
