<template>
  <header>
    <!-- <Navbar /> -->
    <div class="container-fluid bg-light">
      <div class="row">
        <div class="col-12 d-flex justify-content-between ">
          <div v-if="account?.id">

            <button class="btn btn-outline-success button1 m-3" type="button" data-bs-toggle="offcanvas"
              data-bs-target="#offcanvasExample" aria-controls="offcanvasExample">
              Create a Keep <i class="mdi mdi-plus"></i>
            </button>
          </div>
   <div v-if="account?.id">
            <button class="btn btn-outline-success button1 mt-3" type="button" data-bs-toggle="offcanvas"
              data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">Create a Vault <i
                class="mdi mdi-plus"></i></button>
</div>
            <router-link :to="{ name: 'Home' }" class="selectable text-uppercase" aria-controls="Navigate to Home">
            
              <p class="text-center fs-4 p-2 mt-3" title="the keepr co.">
                the keepr co.
              </p>
            </router-link>

          <div class="mt-3 p-2">
            <Login />
          </div>
        </div>
      </div>
    </div>


  </header>
  <main>
    <router-view />
  </main>
  <footer class="text-center footerStyle">
    Made by Thomas Peek <a href="https://github.com/good-ol-peekers"><img class="m-2 img-fluid githubImg" src="https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png" alt=""></a>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
// import Navbar from './components/Navbar.vue'
import Login from '../src/components/Login.vue'



export default {
  setup() {
    return {
      appState: computed(() => AppState),
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

.button1 {
  box-shadow: 5px 7px 9px black;
  color: rgb(255, 0, 115)
}

.footerStyle {
  min-width: 100vw;
  box-shadow: 2px 2px 2px black;
  color: whitesmoke;
  background-color: aqua;
  text-shadow: 2px 2px 2px black;
  font-size: large;
  height: 8vh;

}
.githubImg{
  height: 6vh;
  width: auto;
}
// footer {
//   // display: grid;
//   place-content: center;
//   height: 40px;
//   box-shadow: 2px 2px 2px black;
// }</style>
