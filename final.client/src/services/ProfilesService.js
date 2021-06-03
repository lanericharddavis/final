import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(id) {
    try {
      const res = await api.get('/api/profiles/' + id)
      AppState.profile = res.data
    } catch (err) {
      logger.error('Error: Cannot Get Profile', err)
    }
  }

  async getVaultsByProfileId(id) {
    const res = await api.get(`/api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }

  async getKeepsByProfileId(id) {
    const res = await api.get(`/api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async getActive(id) {
    const res = await api.get(`api/profile/${id}/active`)
    AppState.activeProfile = res.data
  }
}

export const profilesService = new ProfilesService()
