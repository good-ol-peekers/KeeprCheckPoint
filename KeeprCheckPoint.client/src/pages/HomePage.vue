<template>
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
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="exampleModalLabel">{{ keep.name }}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body row">
          <div class="col-6"> <img class="img-fluid" :src="keep.img" alt=""> </div>
          <div class="col-6">
            <div class="fs-1 text-center">{{ keep.kept }} <i class="mdi mdi-archive"></i>  {{ keep.views }} <i class="mdi mdi-eye">  </i>   </div>
            <div class="fs-1"></div>
            <h6>{{ keep.description }}</h6>
            <div><img class="img-fluid rounded-circle tinyPic" :src="keep.creator.picture" alt=""> <p>{{ keep.creator.name }}</p></div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
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
export default {
  setup() {

    async function GetAllKeeps() {
      try {
        await keepsService.GetAllKeeps()
      } catch (error) {
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      GetAllKeeps();
    });


    return {
      keeps: computed(() => AppState.keeps),
      keep: computed(() => AppState.keep),

      async GetKeepByID(id) {
        try {
          // debugger
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
.tinyPic{
  height: 5vh;
  width: auto;
}
</style>
