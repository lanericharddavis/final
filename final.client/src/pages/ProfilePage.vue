<template>
  <div class="container-fluid profile-page">
    <h1>You Have Reached the Profile Page</h1>
    <div class="row">
      <div class="col-md-5">
        <img v-if="state.activeProfile.picture" :src="state.activeProfile.picture" alt="profile picture">
      </div>
      <div class="col-md-7">
        <div class="row">
          <h2>Profile Name</h2>
        </div>
        <div class="row">
          <h4>Vaults: 0</h4>
        </div>
        <div class="row">
          <h4>Keeps: 0</h4>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <div class="row">
          <div class="col-md-1">
            <h3>Vaults</h3>
          </div>
          <div class="col-md-1 d-flex align-items-center">
            <i class="fas fa-plus fa-2x hoverable jump-up" title="Create Vault"></i>
          </div>
        </div>
        <div class="row">
          <div class="col">
            {{ state.vaults }}
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <div class="row">
          <div class="col-md-1">
            <h3>Keeps</h3>
          </div>
          <div class="col-md-1 d-flex align-items-center">
            <i class="fas fa-plus fa-2x hoverable jump-up" title="Create Keep"></i>
          </div>
        </div>
        <div class="row">
          <div class="col">
            {{ keepProp.creator }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
// import { logger } from '../utils/Logger'

export default {
  name: 'ProfilePage',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      activeProfile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    })
    // watch(() => state.loading, () => {
    //   profilesService.getActive(route.params.id)
    // })
    onMounted(async() => {
      await profilesService.getProfile(route.params.id)
      await profilesService.getVaultsByProfileId(route.params.id)
      await profilesService.getKeepsByProfileId(route.params.id)
    })
    return {
      state,
      route
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.fa-plus{
  size: 200%;
}

.masonry { /* Masonry container */
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
    display: inline-block;
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
    .masonry {
        -moz-column-count: 1;
        -webkit-column-count: 1;
        column-count: 1;
    }
}

@media only screen and (min-width: 321px) and (max-width: 768px){
    .masonry {
        -moz-column-count: 2;
        -webkit-column-count: 2;
        column-count: 2;
    }
}
@media only screen and (min-width: 769px) and (max-width: 1200px){
    .masonry {
        -moz-column-count: 3;
        -webkit-column-count: 3;
        column-count: 3;
    }
}
@media only screen and (min-width: 1201px) {
    .masonry {
        -moz-column-count: 4;
        -webkit-column-count: 4;
        column-count: 4;
    }
}
</style>
