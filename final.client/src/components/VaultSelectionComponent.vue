<template>
  <option :value="vaultProp.id">
    <h3>{{ vaultProp.name }}</h3>
  </option>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'

export default {
  name: 'VaultSelectionComponent',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      vaults: computed(() => AppState.vaults)
    })
    return {
      state
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
