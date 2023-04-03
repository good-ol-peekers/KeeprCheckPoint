<template>
<div>{{ keeps }}</div>
</template>

<script>

import { onMounted, computed } from 'vue';
import Pop from '../utils/Pop';
import { KeepsServices } from '../services/KeepsService.js';
kee
export default {
  setup() {
    onMounted(()=> {
      GetAllKeeps()
    })
    async function GetAllKeeps(){
      try {
        await KeepsServices.GetAllKeeps()
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
