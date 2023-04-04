import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";


class VaultsServices{
async getMyVaults(){
    const res = await api.get('account/vaults')
    logger.log(res.data)
    AppState.myVaults = res.data
}
}
export const vaultsServices = new VaultsServices();