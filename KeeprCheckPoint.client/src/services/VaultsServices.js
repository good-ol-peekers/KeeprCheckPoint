import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";


class VaultsServices{

    async createVault(formData){
        const res = await api.post('api/vaults', formData)
        AppState.myVaults.push(res.data)
        logger.log(res.data)
    }
async getMyVaults(){
    const res = await api.get('account/vaults')
    // logger.log(res.data)
    AppState.myVaults = res.data
}
}
export const vaultsServices = new VaultsServices();