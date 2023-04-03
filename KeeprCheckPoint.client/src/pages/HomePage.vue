<template>
  <div class="bricks">
    <div v-for="k in keeps">
      <div>
        <KeepsCard :keep="k"/>
      </div>
    </div>
  </div>
</template>

<script>

import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop';
import { keepsService} from '../services/KeepsService.js';
import { AppState } from '../AppState';
import KeepsCard from '../components/KeepsCard.vue';
export default {
  setup() {

    async function GetAllKeeps(){
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
    }
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss">
  .bricks {
    columns:30vh;
    column-gap: .9em;

    &>div{
      margin-top: .9em;
      display: inline-block;
    }
  }
  
</style>
