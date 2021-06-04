import { AppState } from '../AppState'
import { api } from './AxiosService'
class VaultsService {
  async create(vault) {
    const res = await api.post('/api/vaults', vault)
    AppState.vaults.push(res.data)
  }

  async getAll() {
    const res = await api.get('/api/vaults')
    AppState.vaults = res.data
  }

  async getById(id) {
    AppState.vaults = []
    const res = await api.get(`/api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async getKeepsByVaultId(id) {
    AppState.keeps = []
    const res = await api.get(`/api/vaults/${id}/keeps`)
    AppState.keeps = res.data
  }

  async update(vault) {
    await api.put(`/api/vaults/${vault.id}`, vault)
  }

  async remove(vaultId) {
    await api.delete(`/api/vaults/${vaultId}`)
    AppState.vaults = AppState.vaults.filter(x => x.id !== vaultId)
  }
}

export const vaultsService = new VaultsService()
