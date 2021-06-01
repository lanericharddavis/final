<template>
  <div class="keep-component">
    <div class="card item shadow rounded-corners mb-4 mt-1">
      <img v-if="keepProp.img"
           :src="keepProp.img"
           class="rounded-corners"
           alt=""
      >
      <div class="card-img-overlay d-flex align-items-end justify-content-between">
        <button class="card-title btn btn-info text-light hoverable"
                type="button"
                data-toggle="modal"
                :data-target="'#keepModal' + keepProp.id"
        >
          <strong>{{ keepProp.name }}</strong>
        </button>
        <router-link :to="{name: 'Profile', params:{id: keepProp.creatorId}}">
          <img :src="keepProp.creator.picture" class="circle-pic jump-up" alt="keeps owner profile picture">
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'

export default {
  name: 'KeepComponent',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    })
    return {
      state
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
