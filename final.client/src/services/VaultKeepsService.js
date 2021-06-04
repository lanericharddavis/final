// import { AppState } from '../AppState'
import router from '../router'
import { api } from './AxiosService'
class VaultKeepsService {
  async create(newVaultKeep) {
    // const res =
    await api.post('/api/vaultkeeps', newVaultKeep)
    // AppState.vaults.push(res.data)
  }

  async removeFromVault(vaultKeepId) {
    // const res =
    await api.delete(`/api/vaultkeeps/${vaultKeepId}`)
    // AppState.keeps = AppState.keeps.filter(x => x.id !== keepId)
    router.go(-1)
  }
}

export const vaultKeepsService = new VaultKeepsService()
