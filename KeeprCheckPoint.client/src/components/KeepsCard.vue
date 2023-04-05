<template>
    <div class="hero-image">

        <div data-bs-toggle="modal" data-bs-target="#exampleModal">
            <img class="img-fluid backgroundimg" :src="keep.img" alt="">
            <div class="fs-5 textStyle">{{ keep.name }}</div>

            <router-link :to="{ name: 'ProfileDetails', params: { profileId: keep.creatorId } }">
                <img class="imageStyle" :src="keep.creator.picture" alt="">
            </router-link>
        </div>

    </div>
</template>


<script>
// import { computed } from 'vue';

// import { keepsService } from '../services/KeepsService';
// import Pop from '../utils/Pop';

// import { AppState } from '../AppState';




export default {
    props: { keep: { type: Object, required: true, } },
    setup() {
        return {
            // activeKeep: computed(() => AppState.keeps.find(keep.id)),

            // async GetKeepByID(id) {
            //     try {
            //         // let id = this.activeKeep.id
            //         await keepsService.GetKeepByID(id)
            //     } catch (error) {
            //         Pop.error(error.message)

            //     }
            // }
        }
    }
}
</script>


<style lang="scss" scoped>
.imageStyle {
    height: 7vh;
    width: auto;
    border-radius: 100%;
    position: absolute;
    transform: translate(-120%, 10%);
}

.backgroundimg {
    border-radius: 5%;
}

.textStyle {
    // text-align: end;
    text-shadow: 5px 5px 5px black;
    color: rgb(238, 229, 213);
    position: absolute;
    transform: translate(10%, -120%);
}


.hero-image {
    position: relative;
    background-position: center;
    background-size: cover;
    box-shadow: 5px 5px 5px black;
    border-radius: 5%;
}
</style>