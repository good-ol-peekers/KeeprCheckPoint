<template>

            <div  v-if="profile"  class="">
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

                <div v-if="profileKeeps" v-for="k in profileKeeps" >
                    <KeepsCard :keep="k"/>
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
        };
    },
    components: { KeepsCard }
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
</style>