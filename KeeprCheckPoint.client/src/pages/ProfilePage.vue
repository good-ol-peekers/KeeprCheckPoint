<template>

            <div  v-if="profile">
                <div class="d-flex justify-content-center">
                    <div>
                        <img class="backgroundimg" :src="profile.coverImg" alt="">
                    </div>
                </div>
                <div class="d-flex justify-content-center">
                    <img class="imageStyle" :src="profile.picture" alt="">
                </div>

                 <div class="d-flex justify-content-center">
                        <div class="textStyle fs-3">{{ profile.name }}</div>
                </div>
                
                <div v-if="profileKeeps && profileVaults" class="d-flex justify-content-center mt-2">

                    <div>{{ profileKeeps.length }} <i class="mdi mdi-briefcase">    </i>   |   <i class="mdi mdi-safe" ></i>   </div>

                    <div>{{ profileVaults.length }}  </div>
                </div>

                <div v-if="profileKeeps" class="bricks">
                    <div v-for="k in profileKeeps" >
                        <div>
                            <KeepsCard @click="GetKeepByID(k.id)" :keep="k" />
                        </div>
                    </div>
                </div>

            </div>



      <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div v-if="profileKeep">

              <div  class="modal-header">
                <h1  class="modal-title fs-5" id="exampleModalLabel">{{ profileKeep?.name }}</h1>
                <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal" aria-label="Close"><i
                  class="mdi mdi-close-box-multiple-outline"></i></button>
                </div>
                <div class="modal-body row">
                  <div class="col-6"> <img class="img-fluid modal-img" :src="profileKeep.img" alt=""> <button type="button" data-bs-dismiss="modal" @click="deleteKeep(profileKeep.id)" class="modal-button-trash btn btn-outline-danger" ><i class="mdi mdi-trash-can-outline" ></i></button> </div>
                  <div class="col-6">
                    <div class="fs-1 mb-5 text-center">{{ profileKeep.kept }} <i class="mdi mdi-archive"></i> {{ profileKeep.views }} <i
                      class="mdi mdi-eye"> </i> </div>
                      <div class="fs-1"></div>
                      <h6 class="mb-3">{{ profileKeep.description }}</h6>
                  

                      <div class="row mt-2">
                        <div class="text-end">
                          <img class="col-1 tinyPic img-fluid rounded-circle" :src="profileKeep?.creator.picture" alt="">
                          <div class="col-2">
                            <p>{{ profileKeep?.creator.name }}</p>
                          </div>
                        </div>
                      </div>

                  
                    </div>
                  </div>
                  <div class="modal-footer">
                    <div class="dropdown">
                      <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                      aria-expanded="false">
                      Add to Vault
                    </button>
                    <ul class="dropdown-menu">
                      <li v-if="profileVaults.length == 0" class="dropdown-item">You don't have any vaults</li>
                      <div v-for="vault in profileVaults">
                        <button :profileVault="vault" @click="addToVault(profileVaults.id, profileKeep.id)" class="btn btn-outline-success" >{{ profileVaults.name }}</button>
                      </div>
                    </ul>
                  </div>
              
                  <button type="button" class="btn btn-primary">Save changes</button>
              
              
              
                </div>
              </div>
            
              </div>
            </div>
      </div>




</template>
<script>

import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import KeepsCard from '../components/KeepsCard.vue';
import { useRoute } from 'vue-router';
import { profilesService } from '../services/ProfilesService';
import { keepsService } from '../services/KeepsService';
import { vaultKeepsService } from '../services/VaultKeepsService';

export default {
    setup() {
        const route = useRoute();



        async function profileKeeps() {
            try {
                AppState.profileKeeps = null;
                let profileId = route.params.profileId;
                await profilesService.profileKeeps(profileId);
            }
            catch (error) {
                Pop.error(error.message)
            }
        }
        async function profileVaults() {
            try {
                AppState.profileVaults = null;
                let profileId = route.params.profileId;
                await profilesService.profileVaults(profileId);
            }
            catch (error) {
                Pop.error(error.message)
            }
        }
        async function getProfile() {
            try {
                // debugger
                // logger.log(route.params.id)
                const profileId = route.params.profileId;
                await profilesService.getProfile(profileId);
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        onMounted(() => {
            getProfile();
            profileKeeps();
            profileVaults();
        });
        return {
            profile: computed(() => AppState.profile),
            profileKeeps: computed(() => AppState.profileKeeps),
            profileVaults: computed(() => AppState.profileVaults),
            profileKeep: computed(() => AppState.profileKeep),

             async GetKeepByID(id) {
                try {
                    // debugger
                    // const accountId =
                    await keepsService.GetProfileKeepByID(id)
                } catch (error) {
                    Pop.error(error.message)
                }
            },

            
            async deleteKeep(profileKeepId) {
                try {
                  let keepId = profileKeepId
                    await Pop.confirm('are you sure you want to delete this keep>?')
                    await keepsService.deleteKeep(keepId)
                    // Pop.success('you have deleted your Keep! oh my gosh!')
                } catch (error) {
                    Pop.error(error.message)
                }
            },
            
            
            async addToVault(profileKeepId, profileVaultId) {
                try {

                  let keepId = profileKeepId;
                  let vaultId = profileVaultId;
                    await Pop.confirm('are you sure you want to add this keep to your Vault?')
                    await vaultKeepsService.addToVault(keepId, vaultId)
                    Pop.success('you have a growing vault!')
                } catch (error) {
                    Pop.error(error.message)
                }
            },
            
            
        }
    },
    components: { KeepsCard },
}

</script>


<style lang="scss" scoped>

.backgroundimg {
    border-radius: 5%;
    height: 50vh;
    width: 60vw;
    border: 2px solid white;
    box-shadow: 3px 1px 10px black;
    // background-image: ;
    // position: relative;
}
// .hero-image {
//     position: relative;
//     background-position: center;
//     // background-size: cover;
//     // box-shadow: 5px 5px 5px black;
//     border-radius: 5%;
// }

.textStyle {
    // text-align: end;
    text-shadow: 1px 1px 1px rgb(9, 0, 55);
    // box-shadow: 5px 5px 5px black;
    // color: rgb(252, 252, 252);
    // position: absolute;
}

.imageStyle {
    height: 17vh;
    width: auto;
    border-radius: 100%;
    border: 5px dotted white;
    box-shadow: 1px 1px 10px black;
    // position: absolute;
    // transform: translateX(-15);
    transform: translate3d(0, -50%, 0);
}


.button1{
  box-shadow: 5px 7px 9px black ;
  color: rgb(255, 0, 115)
}

.header1{
  box-shadow: 2px 2px 5px black;
  background-color: wheat;
  color: black;
}

.bricks {
  columns: 30vh;
  column-gap: .9em;

  &>div {
    margin-top: .9em;
    display: inline-block;
  }
}

.modal-img{
  position: absolute;
  position: inherit;
  border-radius: 2%;
}

.modal-button-trash{
  position: relative;
  transform: translate(-0%, -101%);
}

.tinyPic {
  height: 5vh;
  width: auto;
}
</style>