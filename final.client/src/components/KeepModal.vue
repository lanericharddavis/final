<template>
  <div class="modal"
       id="keepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="Keep Modal"
       aria-hidden="true"
  >
    <div class="container-fluid keep-modal">
      <div class="col-md-6">
        <img v-if="keepProp.img" :src="keepProp.img" alt="">
      </div>
      <div class="col-md-6">
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
          <hr>
          <div class="col-md-6">
            <p>tag?</p>
          </div>
          <div class="col-md-6">
            <div class="col-md-6">
              <p>tag?</p>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-md-5">
            <button class="btn btn-outline-info">
              Add To Vault
            </button>
          </div>
          <div class="col-md-2">
            <i class="fas fa-trash-alt"></i>
          </div>
          <div class="col-md-2">
            <img v-if="keepProp.creator.picture" :src="keepProp.creator.picture" class="circle-pic" alt="profile picture">
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
            <button type="button" class="btn btn-primary">
              Save changes
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'

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
      keeps: computed(() => AppState.activeKeep)
    })
    onMounted(async() => {
      await keepsService.getById(props.keepProp.id)
    })
    return {
      state
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

.circle-pic{
  border-radius: 50%;
}

</style>
