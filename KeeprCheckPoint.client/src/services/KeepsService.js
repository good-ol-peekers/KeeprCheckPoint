import { AppState } from "../AppState"
import { api } from "./AxiosService"

class KeepsService {
async GetAllKeeps(){
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
}
}
export const keepsService = new KeepsService()