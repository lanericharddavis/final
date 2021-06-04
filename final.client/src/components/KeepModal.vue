<template>
  <div v-if="keepProp"
       class="modal"
       :id="'keepModal' + keepProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="Keep Modal"
       aria-hidden="true"
  >
    <div class="container-fluid keep-modal">
      <div>
        <div class="row main-row">
          <div class="col-md-6 col-12 p-2">
            <img v-if="keepProp.img" :src="keepProp.img" class="modal-img" alt="">
          </div>
          <div class="col-md-6 col-12 pt-5 pl-2">
            <div class="row justify-content-center">
              <div class="col-md-4 col-3">
                <i class="fas fa-eye fa-2x"></i>
                <p>{{ keepProp.views }}</p>
              </div>
              <div class="col-md-4 col-3">
                <i class="fas fa-share-alt fa-2x"></i>
                <p>{{ keepProp.shares }}</p>
              </div>
              <div class="col-md-4 col-3">
                <i class="fas fa-folder fa-2x"></i>
                <p>{{ keepProp.keeps }}</p>
              </div>
            </div>
            <div class="row justify-content-between">
              <div class="col-md-6">
                <h1>{{ keepProp.name }}</h1>
              </div>
              <div class="col-md-5">
                <div class="row">
                  <router-link :to="{name: 'Profile', params:{id: keepProp.creatorId}}" data-dismiss="modal">
                    <img v-if="keepProp.creator.picture" :src="keepProp.creator.picture" class="circle-pic jump-up ml-3" title="profile page" alt="profile picture">
                  </router-link>
                </div>
                <div class="row">
                  <div class="col ml-3">
                    <strong><p>{{ keepProp.creator.name }}</p></strong>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col">
                <p>{{ keepProp.description }}</p>
              </div>
            </div>
            <form class="form row align-items-center mt-2" @submit.prevent="addKeepToVault">
              <div class="col-md-6">
                <div class="form-group col-auto my-1">
                  <label class="mr-sm-2 sr-only" for="inlineFormCustomSelect"></label>
                  <select @click="getVaultsByProfileId()" class="custom-select mr-sm-2" id="inlineFormCustomSelect" v-model="state.newVaultKeep.vaultId">
                    <!--TODO ^^^^  v-model="state.vaultId.id" -->
                    <option selected>
                      Insert Into Vault...
                    </option>
                    <option v-for="Vaults in state.vaults" :key="Vaults.id" :value="Vaults.id" @click="addKeepToVault(Vaults.id)">
                      {{ Vaults.name }}
                    </option>
                  </select>
                </div>
              </div>
              <div class="col-md-6">
                <button class="btn btn-info ml-3" type="submit">
                  Add To Vault
                </button>
              </div>
            </form>
            <div class="row mt-5">
              <div class="modal-footer col-md-12 justify-content-between">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <div v-if="state.account.id === keepProp.creator.id" class="col-md-2">
                  <i class="fas fa-trash-alt fa-2x hoverable" @click="remove()" title="delete keep" data-dismiss="modal"></i>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Notification from '../utils/Notification'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { useRouter } from 'vue-router'
// import $ from 'jquery'

export default {
  name: 'KeepModal',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account),
      // vaultId: {},
      newVaultKeep: {
        creatorId: props.keepProp.creatorId,
        keepId: props.keepProp.id
      }
    })
    return {
      state,
      route,
      async getVaultsByProfileId() {
        try {
          await profilesService.getVaultsByProfileId(state.account.id)
        } catch (error) {
          logger.error(error)
        }
      },
      async addKeepToVault() {
        try {
          // debugger
          // Vaults.id
          await vaultKeepsService.create(state.newVaultKeep)
        } catch (error) {
          logger.error(error)
        }
      },
      async remove() {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this keep?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await keepsService.remove(props.keepProp.id)
            Notification.toast('Successfully Deleted Keep', 'success')

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

.circle-pic{
  border-radius: 50%;
  max-width: 50px;
}

.modal-img{
  max-width: 37vw;
  // border-radius: 10%;
}

.main-row{
  margin-top:10vh;
  margin-right: 10vw;
  margin-left: 10vw;
  background-color:rgba(255, 255, 255, 0.637);
}

</style>
