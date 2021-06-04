<template>
  <div class="container-fluid profile-page">
    <div class="row align-items-center p-3">
      <div class="col-md-5">
        <img v-if="state.profile.picture" :src="state.profile.picture" alt="profile picture">
      </div>
      <div class="col-md-7">
        <div class="row">
          <h1>{{ state.profile.name }}</h1>
        </div>
        <div class="row">
          <h4>Vaults: {{ state.vaults.length }}</h4>
        </div>
        <div class="row">
          <h4>Keeps: {{ state.keeps.length }}</h4>
        </div>
      </div>
    </div>
    <div class="row p-3">
      <div class="col-md-12">
        <div class="row">
          <div class="col-md-1">
            <h3>Vaults</h3>
          </div>
          <div v-if="state.account.id === state.profile.id" class="col-md-10 d-flex align-items-center">
            <form class="form-inline" @submit.prevent="createVault">
              <div class="form-group mx-sm-3 mb-2">
                <label for="vaultNameInput" class="sr-only"></label>
                <input type="text"
                       class="form-control"
                       id="vault-name-input"
                       placeholder="Vault Name..."
                       v-model="state.newVault.name"
                       required
                >
              </div>
              <div class="col-auto my-1">
                <label class="mr-sm-2 sr-only" for="inlineFormCustomSelect"></label>
                <select class="custom-select mr-sm-2" id="inlineFormCustomSelect" v-model="state.newVault.IsPrivate" required>
                  <option selected>
                    Vault Privacy...
                  </option>
                  <option :value="true">
                    Private
                  </option>
                  <option :value="false">
                    Public
                  </option>
                </select>
              </div>
              <button type="submit" class="btn btn-primary mb-2">
                Create Vault
              </button>
            </form>
          </div>
        </div>

        <div class="card-columns masonry">
          <VaultComponent v-for="Vaults in state.vaults" :key="Vaults.id" :vault-prop="Vaults" />
        </div>
      </div>
    </div>
    <div class="row p-3">
      <div class="col-md-12">
        <div class="row">
          <div class="col-md-1">
            <h3>Keeps</h3>
          </div>
          <div v-if="state.account.id === state.profile.id" class="col-md-10 d-flex align-items-center">
            <form class="form-inline" @submit.prevent="createKeep">
              <div class="form-group mx-sm-3 mb-2">
                <label for="keepNameInput" class="sr-only"></label>
                <input type="text"
                       class="form-control"
                       id="keep-name-input"
                       placeholder="Keep Name..."
                       v-model="state.newKeep.name"
                       required
                >
              </div>
              <div class="form-group mx-sm-3 mb-2">
                <label for="keepImgInput" class="sr-only"></label>
                <input type="text"
                       class="form-control"
                       id="keep-img-input"
                       placeholder="Keep Img Url..."
                       v-model="state.newKeep.img"
                       required
                >
              </div>
              <div class="form-group mx-sm-3 mb-2">
                <label for="keepDescriptionInput" class="sr-only"></label>
                <input type="text"
                       class="form-control"
                       id="keep-description-input"
                       placeholder="Keep Description..."
                       v-model="state.newKeep.description"
                       required
                >
              </div>
              <button type="submit" class="btn btn-primary mb-2">
                Create Keep
              </button>
            </form>
          </div>
        </div>
        <div class="card-columns masonry">
          <KeepComponent v-for="Keeps in state.keeps" :key="Keeps.id" :keep-prop="Keeps" />
        </div>
        <KeepModal v-for="Keeps in state.keeps" :key="Keeps.id" :keep-prop="Keeps" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
// import { logger } from '../utils/Logger'
import Notification from '../utils/Notification'

export default {
  name: 'Profile',

  setup(props) {
    const route = useRoute()
    const state = reactive({
      activeProfile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      newVault: {
        creatorId: route.params.id
      },
      newKeep: {
        creatorId: route.params.id
      }
    })
    onMounted(async() => {
      await profilesService.getProfile(route.params.id)
      await profilesService.getVaultsByProfileId(route.params.id)
      await profilesService.getKeepsByProfileId(route.params.id)
    })
    return {
      state,
      route,
      async createVault() {
        try {
          await vaultsService.create(state.newVault)
          state.newVault = {}
        } catch (error) {
          Notification.toast('not passing on the ProfilePage', 'warning')
        }
      },
      async createKeep() {
        try {
          await keepsService.create(state.newKeep)
          state.newKeep = {}
        } catch (error) {
          Notification.toast('not passing on the ProfilePage', 'warning')
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
   margin: 1.5em;
    padding: 0;
    -moz-column-gap: 1.5em;
    -webkit-column-gap: 1.5em;
    column-gap: 1.5em;
    font-size: .85em;
}
.item {
    display: inline-flex;
    background: #fff;
    margin: 0 0 1.5em;
    width: 100%;
  -webkit-transition:1s ease all;
    box-sizing: border-box;
    -moz-box-sizing: border-box;
    -webkit-box-sizing: border-box;
    box-shadow: 2px 2px 4px 0 #ccc;
}
.item img{max-width:100%;}

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
</style>
