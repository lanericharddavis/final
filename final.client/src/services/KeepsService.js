import { AppState } from '../AppState'
import { api } from './AxiosService'
class KeepsService {
  async create(keep) {
    const res = await api.post('/api/keeps', keep)
    AppState.keeps.push(res.data)
  }

  async getAll() {
    const res = await api.get('/api/keeps')
    console.log(res.data)
    AppState.keeps = res.data
  }

  async getById(id) {
    AppState.keeps = []
    const res = await api.get(`/api/keeps/${id}`)
    AppState.activeKeep = res.data
    await this.getById(id)
  }

  async update(keep) {
    await api.put(`/api/keeps/${keep.id}`, keep)
  }

  async remove(keepId) {
    await api.delete(`/api/keeps/${keepId}`)
    AppState.keeps = AppState.keeps.filter(x => x.id !== keepId)
  }
}

export const keepsService = new KeepsService()
