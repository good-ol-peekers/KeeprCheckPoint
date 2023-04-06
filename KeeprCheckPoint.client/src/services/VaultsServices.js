import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";


class VaultsServices{

    async getVaultById(vaultId){
        const res = await api.get('/api/vaults/' + vaultId)
        AppState.vault = res.data
        logger.log( ["vaultspage / vaults Service"], res.data)
    }
    async createVault(formData){
        const res = await api.post('/api/vaults', formData)
        AppState.myVaults.push(res.data)
        logger.log(res.data)
    }
async getMyVaults(){
    const res = await api.get('/account/vaults')
    // logger.log(res.data)
    AppState.myVaults = res.data
}

async deleteVault(vaultId){
    const res = await api.delete('/api/vaults/' + vaultId)
    AppState.vault = []
    // logger.log(res.data)
}

}
export const vaultsServices = new VaultsServices();