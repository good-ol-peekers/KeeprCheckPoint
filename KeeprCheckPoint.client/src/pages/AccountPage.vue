<template>
  <div v-if="account" >

    <img class="img-fluid" :src="account.coverimg" alt="">
    <div class="d-flex justify-content-center">
      <h1>Welcome {{ account.name }}</h1>
      <img class="img-fluid profileImg" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
    </div>
  </div>


  <div v-if="account" class="container-fluid">
      <div class="row">
        <div class="col-12 mt-5">
          <form @submit.prevent="editAccount()">
            <div class="form-floating mb-2">
              <input v-model="editable.picture" :placeholder="account.picture" required type="text" class="form-control"
                id="picture">
              <label for="floatingInput" class="form-label">Picture</label>
            </div>
            <div class="form-floating mb-2">
              <input v-model="editable.name" :placeholder="account.name" required type="text" class="form-control"
                id="name">
              <label for="floatingInput" class="form-label">Name</label>
            </div>
            <div class="form-floating mb-2">
              <input v-model="editable.coverImg" :placeholder="account.coverimg" required type="text" class="form-control" id="bio">
              <label for="floatingInput" class="form-label">cover image</label>
            </div>
            <button class="btn bg-danger mb-4" type="submit">Save Changes</button>
          </form>
        </div>
      </div>
    </div>





</template>

<script>
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { accountService } from '../services/AccountService'
export default {
  setup() {
    
const editable = ref({})
    // watchEffect(() => {
    //   if (AppState.profile.id) {
    //     editable.value = { ...AppState.profile }
    //   }
    // })


    return {
      editable,
      async editAccount() {
        try {
          const formData = editable.value
          await accountService.editAccount(formData)
        } catch (error) {
          Pop.error(error.message)
          logger.error(error)
        }
      },
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped>
.profileImg{
  height: 15vh;
  width: auto;
  border-radius: 35%;
}
</style>
