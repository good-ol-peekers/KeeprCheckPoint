<template>
  <div v-if="account?.id" class="container-fluid">
    <div class="row">
      <div  class="col-12 mb-3 d-flex justify-content-around">
        <button class="btn btn-outline-success button1" type="button" data-bs-toggle="offcanvas"
          data-bs-target="#offcanvasExample" aria-controls="offcanvasExample">
          Create a Keep <i class="mdi mdi-plus"></i>
        </button>

        <button class="btn btn-outline-success button1" type="button" data-bs-toggle="offcanvas"
          data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">Create a Vault <i
            class="mdi mdi-plus"></i></button>

      </div>
    </div>
  </div>




  <div v-if="keeps" class="bricks">
    <div v-for="k in keeps">
      <div>
        <KeepsCard @click="GetKeepByID(k.id)" :keep="k" />
      </div>
    </div>
  </div>

  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div v-if="keep">

          <div class="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">{{ keep?.name }}</h1>
            <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal" aria-label="Close"><i
                class="mdi mdi-close-box-multiple-outline"></i></button>
          </div>
          <div class="modal-body row">
            <div  class="col-6"> <img class="img-fluid modal-img" :src="keep.img" alt=""> <button   v-if="account?.id == keep.creatorId"  type="button"
                data-bs-dismiss="modal" @click="deleteKeep(keep.id)" class="modal-button-trash btn btn-outline-danger"><i
                  class="mdi mdi-trash-can-outline"></i></button> </div>
            <div class="col-6">
              <div class="fs-1 mb-5 text-center">{{ keep.kept }} <i class="mdi mdi-archive"></i> {{ keep.views }} <i
                  class="mdi mdi-eye"> </i> </div>
              <div class="fs-1"></div>
              <h6 class="mb-3">{{ keep.description }}</h6>

              <router-link class="selectable" :to="{ name: 'ProfileDetails', params: { profileId: keep.creatorId } }">
                <div class="row mt-2">
                  <div class="text-end">
                    <img class="col-1 tinyPic img-fluid rounded-circle" :src="keep?.creator.picture" alt="">
                    <div class="col-2">
                      <p>{{ keep.creator.name }}</p>
                    </div>
                  </div>
                </div>
              </router-link>

            </div>
          </div>
          <div class="modal-footer">
            <div v-if="account?.id" class="dropdown">
              <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                aria-expanded="false">
                Add to Vault
              </button>
              <ul class="dropdown-menu">
                <li v-if="myVaults.length == 0" class="dropdown-item">You don't have any vaults</li>
                <div v-for="vault in myVaults">
                  <button :vault="vault" @click="addToVault(vault.id, keep.id)" class="btn btn-outline-success">{{
                    vault.name }}</button>
                </div>
              </ul>
            </div>

            <button type="button" class="btn btn-primary">Save changes</button>



          </div>
        </div>

      </div>
    </div>
  </div>

  <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasRight" aria-labelledby="offcanvasRightLabel">
    <div class="offcanvas-header header1">
      <h5 class="" id="offcanvasRightLabel">Create a Vault </h5>
      <button type="button" class="btn btn-outline-danger button1" data-bs-dismiss="offcanvas" aria-label="Close"><i
          class="mdi mdi-close-box-multiple-outline"></i></button>
    </div>
    <div class="offcanvas-body">
      ...
      <form @submit.prevent="createVault()">
        <div>
          <input v-model="editable.Name" required type="text" class="form-control" id="name" maxlength="50">
          <label for="floatingInput" class="form-label"> Name </label>
        </div>
        <div>
          <input v-model="editable.Description" required type="text" class="form-control" id="Description"
            maxlength="500">
          <label for="floatingInput" class="form-label"> Description </label>
        </div>
        <div>
          <input v-model="editable.Img" required type="text" class="form-control" id="Img" maxlength="500">
          <label for="floatingInput" class="form-label"> Image </label>
        </div>
        <div class="form-check mt-4">
          <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
          <label class=" form-check-label" for="flexCheckDefault">
            Make this Vault Private
          </label>
        </div>

        <button data-bs-dismiss="offcanvas" class="mt-4 col-12 btn btn-outline-success"> create Vault <i
            class="mdi mdi-plus"></i> </button>

      </form>






    </div>
  </div>





  <div class="offcanvas offcanvas-start" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
    <div class="offcanvas-header header1">
      <h5 class="offcanvas-title" id="offcanvasExampleLabel"> Create your Keep here!</h5>
      <button type="button" class="button1 btn btn-outline-danger" data-bs-dismiss="offcanvas" aria-label="Close"><i
          class="mdi mdi-close-box-multiple-outline"></i></button>
    </div>
    <div class="offcanvas-body">
      <div>
        <form @submit.prevent="createKeep()">
          <div>
            <input v-model="editable.Name" required type="text" class="form-control" id="name" maxlength="50">
            <label for="floatingInput" class="form-label"> Name </label>
          </div>
          <div>
            <input v-model="editable.Description" required type="text" class="form-control" id="Description"
              maxlength="500">
            <label for="floatingInput" class="form-label"> Description </label>
          </div>
          <div>
            <input v-model="editable.Img" required type="text" class="form-control" id="Img" maxlength="500">
            <label for="floatingInput" class="form-label"> Image </label>
          </div>
          <button class="btn btn-outline-success"> create keep <i class="mdi mdi-plus"></i> </button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>

import { onMounted, computed, ref } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService.js';
import { AppState } from '../AppState';
import KeepsCard from '../components/KeepsCard.vue';
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { vaultsServices } from '../services/VaultsServices.js'
import { logger } from '../utils/Logger';

export default {
  setup() {

    const editable = ref({})

    async function GetAllKeeps() {
      try {
        await keepsService.GetAllKeeps()
      } catch (error) {
        Pop.error(error.message)
      }
    }




    async function getMyVaults() {
      try {
        // if(account?.id){
        await vaultsServices.getMyVaults()
        // }
      } catch (error) {
        // Pop.error(error.message)
      }
    }


    onMounted(() => {
      GetAllKeeps();
      getMyVaults();
    });


    return {
      editable,
      keeps: computed(() => AppState.keeps),
      keep: computed(() => AppState.keep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),


      async createVault() {
        try {
          const formData = editable.value
          await vaultsServices.createVault(formData)
          // editable = {}
        } catch (error) {
          Pop.error(error.message)
        }
      },


      async createKeep() {
        try {
          const formData = editable.value
          await keepsService.createKeep(formData)
          // editable = {}
        } catch (error) {
          Pop.error(error.message)
        }
      },


      async deleteKeep(keepId) {
        try {
          await Pop.confirm('are you sure you want to delete this keep>?')
          await keepsService.deleteKeep(keepId)
          // Pop.success('you have deleted your Keep! oh my gosh!')
        } catch (error) {
          Pop.error(error.message)
        }
      },

      async addToVault(vaultId, keepId) {
        try {
          let vaultKeepData = {};
          vaultKeepData.keepId = keepId;
          vaultKeepData.vaultId = vaultId;
          await Pop.confirm('are you sure you want to add this keep to your Vault?')
          await vaultKeepsService.addToVault(vaultKeepData)
          Pop.success('you have a growing vault!')
        } catch (error) {
          Pop.error(error.message)
        }
      },

      async GetKeepByID(id) {
        try {
          // debugger
          // const accountId =
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
.button1 {
  box-shadow: 5px 7px 9px black;
  color: rgb(255, 0, 115)
}

.header1 {
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

.modal-img {
  position: absolute;
  position: inherit;
  border-radius: 2%;
}

.modal-button-trash {
  position: relative;
  transform: translate(-0%, -101%);
}

.tinyPic {
  height: 5vh;
  width: auto;
}
</style>
