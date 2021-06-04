<template>
  <div class="keep-component card item shadow rounded-corners mb-4 mt-1">
    <img v-if="keepProp.img"
         :src="keepProp.img"
         class="rounded-corners"
         alt=""
    >
    <div class="card-img-overlay img d-flex align-items-end justify-content-between">
      <button class="card-title btn btn-info text-light hoverable"
              type="button"
              data-toggle="modal"
              :data-target="'#keepModal' + keepProp.id"
              title="view keep details"
      >
        <strong>{{ keepProp.name }}</strong>
      </button>
      <router-link :to="{name: 'Profile', params:{id: keepProp.creator.id}}">
        <img v-if="keepProp.creator.picture" :src="keepProp.creator.picture" class="circle-pic jump-up" alt="keeps owner profile picture" title="keeps owner's profile">
      </router-link>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import { useRouter } from 'vue-router'

export default {
  name: 'KeepComponent',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      activeVault: computed(() => AppState.activeVault)
    })
    return {
      route,
      state,
      async getProfile() {
        try {
          console.log(props.keepProp.creator)
          await profilesService.getProfile(props.keepProp.creatorId)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

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
  max-width: 50px;
}

</style>
