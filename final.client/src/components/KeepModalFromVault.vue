<template>
  <div v-if="keepProp"
       class="modal"
       :id="'keepModal' + keepProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="Keep Modal"
       aria-hidden="true"
  >
    <div class="container-fluid keep-modal-from-vault">
      <div>
        <div class="row main-row">
          <div class="col-md-6 p-2">
            <img v-if="keepProp.img" :src="keepProp.img" class="modal-img" alt="">
          </div>
          <div class="col-md-6 pt-5 pl-2">
            <div class="row justify-content-center">
              <div class="col-md-4">
                <i class="fas fa-eye fa-2x"></i>
                <p>{{ keepProp.views }}</p>
              </div>
              <div class="col-md-4">
                <i class="fas fa-share-alt fa-2x"></i>
                <p>{{ keepProp.shares }}</p>
              </div>
              <div class="col-md-4">
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
                    <img v-if="keepProp.creator.picture" :src="keepProp.creator.picture" class="circle-pic jump-up" title="profile page" alt="profile picture">
                  </router-link>
                </div>
                <div class="row">
                  <strong><p>{{ keepProp.creator.name }}</p></strong>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col">
                <p>{{ keepProp.description }}</p>
              </div>
            </div>
            <div class="col-md-6">
              <button v-if="state.account.id === keepProp.creatorId" class="btn btn-info" @click="removeFromVault()" data-dismiss="modal">
                Remove From Vault
              </button>
            </div>
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
import { vaultKeepsService } from '../services/VaultKeepsService'
import { profilesService } from '../services/ProfilesService'
import { useRouter } from 'vue-router'
// import $ from 'jquery'

export default {
  name: 'KeepModalFromVault',
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
      account: computed(() => AppState.account)
      // newKeepInVault: {
      //   name: props.keepProp.name,
      //   creatorId: props.keepProp.creatorId,
      //   description: props.keepProp.description,
      //   img: props.keepProp.img,
      //   views: props.keepProp.views,
      //   shares: props.keepProp.shares,
      //   keeps: props.keepProp.keeps
      // }
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
      async removeFromVault() {
        try {
          if (await Notification.confirmAction('Are you sure you want to remove the keep from this vault?', 'Confirm', '', 'Yes, Remove')) {
            await vaultKeepsService.removeFromVault(props.keepProp.vaultKeepId)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async remove() {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this keep?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await keepsService.remove(props.keepProp.id)
            Notification.toast('Successfully Deleted Keep', 'success')
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
