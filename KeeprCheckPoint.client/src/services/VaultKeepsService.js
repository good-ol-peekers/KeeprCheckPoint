
import { api } from "./AxiosService"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"

class VaultKeepsService{


    async addToVault(keepId, vaultId){
        const res = await api.post('api/vaultkeeps', {keepId, vaultId})
        // logger.log(res.data)
        AppState.vaultKeeps.push(res.data)
    }


}

export const vaultKeepsService = new VaultKeepsService()