<template>
  <div v-if="keeps">
    <div v-for="k in keeps">
      <KeepCard :keep="k"/>
    </div>
  </div>
</template>

<script>

import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop';
import { keepsService} from '../services/KeepsService.js';
import { AppState } from '../AppState';
import { KeepCard } from '../components/KeepsCard.vue'

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
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
  
</style>
