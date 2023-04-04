<template>
  <div v-if="keeps" class="bricks">
    <div v-for="k in keeps">
      <div>
        <KeepsCard @click="GetKeepByID(k.id)" :keep="k" />
      </div>
    </div>
  </div>

  <div v-if="keep" class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">{{ keep.name }}</h1>
          <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal" aria-label="Close">Close <i
              class="mdi mdi-close"></i></button>
        </div>
        <div class="modal-body row">
          <div class="col-6"> <img class="img-fluid" :src="keep.img" alt=""> </div>
          <div class="col-6">
            <div class="fs-1 mb-5 text-center">{{ keep.kept }} <i class="mdi mdi-archive"></i> {{ keep.views }} <i
                class="mdi mdi-eye"> </i> </div>
            <div class="fs-1"></div>
            <h6 class="mb-3">{{ keep.description }}</h6>

            <div class="row mt-2">
              <div class="text-end">
                <img class="col-1 tinyPic img-fluid rounded-circle" :src="keep?.creator.picture" alt="">
                <div class="col-2">
                  <p>{{ keep.creator.name }}</p>
                </div>
              </div>
            </div>


          </div>
        </div>
        <div class="modal-footer">
          <div class="dropdown">
            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
              aria-expanded="false" @click="addToVault">
              Add to Vault
            </button>
            <ul class="dropdown-menu">
              <li v-if="myVaults.length == 0" class="dropdown-item">You are not in any clubs</li>
              <div v-for="vault in myVaults">
                <button :vault="vault" @click="addToVault(vault.id, keep.id)" class="btn btn-outline-success" >{{ vault.name }}</button>
              </div>
            </ul>
          </div>

          <button type="button" class="btn btn-primary">Save changes</button>



        </div>
      </div>
    </div>
  </div>
  <!-- <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">

          <h1 class="modal-title fs-5" id="exampleModalLabel">{{ keep?.name }}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body container-fluid">
          <div class="row">
            <div class="col-6">
              <img class="img-fluid" :src="keep.img" alt="">
            </div>
            <div class="col-6">
              <div class="">{{ keep.views }} {{ keep.kept }}</div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div> -->
</template>

<script>

import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService.js';
import { AppState } from '../AppState';
import KeepsCard from '../components/KeepsCard.vue';
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { vaultsServices } from '../services/VaultsServices.js'

export default {
  setup() {

    async function GetAllKeeps() {
      try {
        await keepsService.GetAllKeeps()
      } catch (error) {
        Pop.error(error.message)
      }
    }


    async function getMyVaults() {
      try {
        await vaultsServices.getMyVaults()
      } catch (error) {
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      GetAllKeeps();
      getMyVaults();
    });


    return {
      keeps: computed(() => AppState.keeps),
      keep: computed(() => AppState.keep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),


      async addToVault(keep, vault) {
        try {
          debugger
          const vaultId = this.vault.id
          const keepId = this.keep.id
          await vaultKeepsService.addToVault(keepId, vaultId)
          Pop.success('you have a growing vault!')
        } catch (error) {
          Pop.error(error.message)
        }
      },

      async GetKeepByID(id) {
        try {
          // debugger
          const accountId =
            await keepsService.GetKeepByID(id)
        } catch (error) {
          Pop.error(error.message)
        }
      },

    }
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss">
.bricks {
  columns: 30vh;
  column-gap: .9em;

  &>div {
    margin-top: .9em;
    display: inline-block;
  }
}


.tinyPic {
  height: 5vh;
  width: auto;
}
</style>
