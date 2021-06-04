<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <keep-modal />
    <keep-modal-from-vault />
  </main>
  <footer>
    <div class="bg-color text-light text-center p-4">
      Made by Lane Davis | June2021
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepModal from './components/KeepModal'
import KeepModalFromVault from './components/KeepModalFromVault'

export default {
  components: { KeepModal, KeepModalFromVault },
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.bg-color{
  background-color: #331f08;
}
</style>
