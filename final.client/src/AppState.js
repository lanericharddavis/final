import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  // vvv not attached to Auth
  loading: true,
  profile: {},
  vaults: [],
  keeps: [],
  activeKeep: null,
  activeProfile: null
})
