import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

    async createKeep(formData){
        const res = await api.post('api/keeps', formData)
        AppState.keeps.push(res.data)
    }
async GetAllKeeps(){
    const res = await api.get('api/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data
}
async GetKeepByID(id){
    const res = await api.get('api/keeps/' + id)
    // logger.log(res.data)
    AppState.keep = res.data
}

async deleteKeep(keepId){
    const res = await api.delete('api/keeps/' + keepId)
    // logger.log(res.data)
    let i = AppState.keeps.findIndex(k => k.id == keepId)
        // logger.log(i)
        AppState.keeps.splice(i, 1,)
    AppState.keep = null
}




}
export const keepsService = new KeepsService()