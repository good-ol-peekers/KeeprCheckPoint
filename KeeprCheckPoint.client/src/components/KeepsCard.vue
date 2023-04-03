<template>
    <div class="hero-image">
        <img class="img-fluid backgroundimg" :src="keep.img" alt="">


        <div class="fs-5 textStyle">{{ keep.name }}</div>
        <img class="imageStyle img-fluid" :src="keep.creator.picture" alt="">


    </div>
</template>


<script>


export default {
    props: { keep: { type: Object, required: true, } },
    setup() {
        return {
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
.backgroundimg{
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