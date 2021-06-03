<template>
  <div class="modal-body">
    <div class="container-fluid keep-modal">
      <div v-if="keepProp"
           class="modal"
           :id="'keepModal' + keepProp.id"
           tabindex="-1"
           role="dialog"
           aria-labelledby="Keep Modal"
           aria-hidden="true"
      >
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
            <div class="row">
              <div class="col">
                <h1>{{ keepProp.name }}</h1>
              </div>
            </div>
            <div class="row">
              <div class="col">
                <p>{{ keepProp.description }}</p>
              </div>
            </div>
            <div class="row">
              <div class="col-md-5">
                <button class="btn btn-info">
                  Add To Vault
                </button>
              </div>
              <div class="col-md-2">
                <i v-if="keepProp.creatorId !== state.account.Id" class="fas fa-trash-alt fa-2x hoverable" @click="remove(keepProp.id)" title="delete keep"></i>
              </div>
              <div class="col-md-2">
                <router-link :to="{name: 'Profile', params:{id: keepProp.creatorId}}">
                  <img v-if="keepProp.creator.picture" :src="keepProp.creator.picture" class="circle-pic jump-up" title="profile page" alt="profile picture">
                </router-link>
              </div>
              <div class="col-md-3">
                <strong><p>{{ keepProp.creator.name }}</p></strong>
              </div>
            </div>
            <div class="row">
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
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
import { keepsService } from '../services/KeepsService'
import Notification from '../utils/Notification'

export default {
  name: 'KeepModal',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      keeps: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account)
    })
    // NOTE Doing this goes into endless loop
    // onMounted(async() => {
    //   await keepsService.getById(props.keepProp.id)
    // })
    return {
      state,
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
