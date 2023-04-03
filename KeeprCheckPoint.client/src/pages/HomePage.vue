<template>
<div>{{ keeps }}</div>
</template>

<script>

import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop';
import { keepsService} from '../services/KeepsService.js';

export default {
  setup() {
    onMounted(()=> {
      GetAllKeeps()
    })
    async function GetAllKeeps(){
      try {
        await keepsServices.GetAllKeeps()
      } catch (error) {
        Pop.error(error.message)
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
  
</style>
