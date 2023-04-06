<template>
    
    
    <div v-if="vault" >
    <div class="container-fluid">
        <div class="row">
            <div class=" d-flex mt-3 mb-3 justify-content-center">
                <img  class="img-fluid vaultImg" :src="vault.img" alt="">
                <div class="vinfo" ><h1>{{ vault.name }}</h1>
                </div>
                    <div class="button-1" v-if="account?.id == vault.creatorId" >
                        <button class="btn btn-outline-danger" @click="deleteVault(vault.id)"> <i class="mdi mdi-delete-circle" > </i> Delete Vault </button>
                    </div>
            </div>
            <div class="text-center mb-3">
                <h1>
                    {{ vaultKeeps?.length }} Keeps 
                </h1>
            </div>

        </div>
    </div>
    
    </div>
    


    <div v-if="vaultKeeps" class="col-2">
        <div class="" v-for="v in vaultKeeps">
            <div class="mt-3" :vault="v">
                <KeepsCard :keep="v" />
                <button  v-if="account?.id == v.creatorId" :keep="v" @click="removeKeepFromVault(v.vaultKeepId)" class="btn btn-outline-danger fs-5" > <i class="mdi mdi-delete-circle" > </i> </button>
            </div>
        </div>
    </div>


</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { vaultKeepsService } from '../services/VaultKeepsService';
import Pop from '../utils/Pop';
import { computed, onMounted, onUnmounted } from 'vue';
import { AppState } from '../AppState';
import KeepsCard from '../components/KeepsCard.vue';
import { vaultsServices } from '../services/VaultsServices';


export default {
    setup() {
        const canEnterVault = (()=> canEnterVault);
        const route = useRoute();
        const router = useRouter();
        async function getVaultKeeps() {
            try {
                let vaultId = route.params.vaultId;
                await vaultKeepsService.getVaultKeeps(vaultId);
            }
            catch (error) {
                Pop.error(error.message);
            }
        }

        async function getVaultById(){
            try {
                let vaultId = route.params.vaultId;
                await vaultsServices.getVaultById(vaultId)

            } catch (error) {
                // Pop.error(error.message)
                router.push({name: 'Home'})
            }
        }
        onMounted(() => {
            getVaultKeeps();
            getVaultById();
            
        });
            onUnmounted(() => {
                AppState.vault = null;
                AppState.vaultKeeps = [];
            });
        return {
            account: computed(() => AppState.account),
            vaultKeeps: computed(() => AppState.vaultKeeps),
            vault: computed(() => AppState.vault),

            async removeKeepFromVault(vaultId){
                try {
                    await vaultKeepsService.removeKeepFromVault(vaultId)
                } catch (error) {
                    Pop.error(error.message)
                }
            },


            async deleteVault(vaultId){
                try {debugger
                    await vaultsServices.deleteVault(vaultId)
                } catch (error) {
                    Pop.error(error.message)
                }
            }
        };
    },
    components: { KeepsCard }
}
</script>


<style lang="scss" scoped>
.vaultImg{
    height: 45vh;
    width: auto;
    box-shadow: 10px 10px 10px black;
    position:absolute ;
}
.vinfo{
    position: relative;
    color: white;
    text-shadow: 1px 1px 1px black;
}
.button-1{
position: relative;
transform: translate(100%, -100%);

}


</style>