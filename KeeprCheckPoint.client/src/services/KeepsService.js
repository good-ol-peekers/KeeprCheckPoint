import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
async GetAllKeeps(){
    const res = await api.get('api/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data
}
async GetKeepByID(id){
    const res = await api.get('api/keeps/' + id)
    logger.log(res.data)
    AppState.keep = res.data
}




}
export const keepsService = new KeepsService()