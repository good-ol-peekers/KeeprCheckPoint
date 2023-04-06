
import { api } from "./AxiosService"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { applyStyles } from "@popperjs/core"

class VaultKeepsService{


    async addToVault(vaultKeepData){
        const res = await api.post('/api/vaultkeeps', vaultKeepData)
        logger.log(res.data)
        AppState.myVaults.push(res.data)
    }

        async getVaultKeeps(vaultId){
            const res = await api.get('/api/vaults/' + vaultId + '/keeps')
            AppState.vaultKeeps =res.data
            console.log(res.data)
        }

        async removeKeepFromVault(vaultId){
            const res = await api.delete('api/vaultkeeps/' + vaultId)
            logger.log('delete response', res.data)
            // AppState.vaultKeeps
        }
}

export const vaultKeepsService = new VaultKeepsService()