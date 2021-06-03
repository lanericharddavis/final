<template>
  <div class="vault-component card">
    <div class="card-body">
      <router-link :to="{name: 'Vault', params:{id: vaultProp.id}}">
        <h3>{{ vaultProp.name }}</h3>
      </router-link>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'

export default {
  name: 'VaultComponent',
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
