<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <keep-modal />
  </main>
  <footer>
    <div class="bg-dark text-light text-center p-4">
      Made by Lane Davis | June2021
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepModal from './components/KeepModal'

export default {
  components: { KeepModal },
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

</style>
