<template>
  <div class="container-fluid vault-page">
    <div class="row align-items-center">
      <div v-if="state.activeVault" class="col-md-5">
        <h1>Vault: {{ state.activeVault.name }}</h1>
        {{ state.vaults }}
      </div>
      <div v-if="state.activeVault" class="col-md-2">
        <i v-if="state.account.id === state.activeVault.creatorId" class="fas fa-trash-alt fa-2x hoverable" @click="remove()" title="delete vault"></i>
      </div>
    </div>
    <div class="row">
      <div v-if="state.keeps" class="col">
        <h5>Keeps: {{ state.keeps.length }}</h5>
      </div>
    </div>
    <div class="card-columns masonry">
      <KeepComponent v-for="Keeps in state.keeps" :key="Keeps.id" :keep-prop="Keeps" />
    </div>
    <KeepModalFromVault v-for="Keeps in state.keeps" :key="Keeps.id" :keep-prop="Keeps" />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
// import { profilesService } from '../services/ProfilesService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Notification from '../utils/Notification'
import router from '../router'

export default {
  name: 'VaultPage',
  // props: {
  //   vaultProp: {
  //     type: Object,
  //     required: true
  //   }
  // },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)

    })
    onMounted(async() => {
      await vaultsService.getById(route.params.id)
      await vaultsService.getKeepsByVaultId(route.params.id)
    })
    return {
      state,
      route,
      async remove() {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this vault?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await vaultsService.remove(route.params.id)
            Notification.toast('Successfully Deleted Vault', 'success')
            router.go(-1)
            // router.push({ path: 'Profile', params: { id: route.params.id } })
            // $('#keepModal' + props.keepProp.id).modal('hide')
          }
        } catch (error) {
          logger.error(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

.card-columns { /* Masonry container */
    -webkit-column-count: 4;
  -moz-column-count:4;
  column-count: 4;
  -webkit-column-gap: 1em;
  -moz-column-gap: 1em;
  column-gap: 1em;
   margin: 2.5em;
    padding: 0;
    -moz-column-gap: 1.5em;
    -webkit-column-gap: 1.5em;
    column-gap: 1.5em;
    font-size: .85em;
}
.item {
    display:inline-flex;
    background: #fff;
    margin: 0 0 1.5em;
    width: 100%;
  -webkit-transition:1s ease all;
    box-sizing:border-box;
    -moz-box-sizing: border-box;
    -webkit-box-sizing: border-box;
    box-shadow: 2px 2px 4px 0 #ccc;
}
.item img{
  max-width:50%;
  }

@media only screen and (max-width: 320px) {
    .card-columns {
        -moz-column-count: 1;
        -webkit-column-count: 1;
        column-count: 1;
    }
}

@media only screen and (min-width: 321px) and (max-width: 768px){
    .card-columns {
        -moz-column-count: 2;
        -webkit-column-count: 2;
        column-count: 2;
    }
}
@media only screen and (min-width: 769px) and (max-width: 1200px){
    .card-columns {
        -moz-column-count: 3;
        -webkit-column-count: 3;
        column-count: 3;
    }
}
@media only screen and (min-width: 1201px) {
    .card-columns {
        -moz-column-count: 4;
        -webkit-column-count: 4;
        column-count: 4;
    }
}

.rounded-corners {
  border-radius: 10px;
}
.gradient {
  background-image: linear-gradient(to top, rgba(0, 0, 0, 0.281) , transparent);
  background-size: cover;
  z-index: 1;
}

.circle-pic{
  border-radius: 50%;
}

</style>
