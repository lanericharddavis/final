<template>
  <div class="container-fluid home">
    <div class="card-column masonry">
      <!-- This Section is repeated Keeps -->
      <!-- <div class="card item shadow rounded-corners mb-4 mt-1 hoverable">
        <img src="../assets/img/boat.png" class="rounded-corners" alt="">
        <div class="card-img-overlay d-flex align-items-end justify-content-between">
          <h4 class="card-title text-light">
            Keep Title
          </h4>
          <router-link :to="{name: 'Profile'}">
            <img src="//placehold.it/50x50" class="circle-pic jump-up" alt="keeps owner profile picture">
          </router-link>
        </div>
      </div> -->
      <!-- -^^- Single Keep Example -^^- -->
      <KeepComponent v-for="Keeps in state.keeps" :key="Keeps.id" :keep-prop="Keeps" />
    </div>
    {{ state.keeps }}
    <keep-modal />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import KeepModal from '../components/KeepModal.vue'

export default {
  components: { KeepModal },
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      await keepsService.getAll()
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">

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
