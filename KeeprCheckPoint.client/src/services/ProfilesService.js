import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"




class ProfilesService{

async profileKeeps(profileId){
    const res = await api.get('api/profiles/' + profileId + ('/keeps'))
    AppState.profileKeeps = res.data
    logger.log(res.data, "profileKEEPS")
}
async profileVaults(profileId){
    const res = await api.get('api/profiles/' + profileId +'/vaults')
    AppState.profileVaults = res.data
    logger.log(res.data, "profileVAULTS")
}


    async getProfile(profileId){
        // debugger
        const res = await api.get('/api/profiles/' + profileId)
        AppState.profile = res.data
        logger.log(AppState.profile)
    }




}
export const profilesService = new ProfilesService()
