<template>
    <div class="row">
        <div class="col-12">
            {{ keep.name }}
        </div>
    </div>
</template>


<script>


export default {
    props: { keep: {type: Object, required: true, } },
    setup(){
        return {}
    }
}
</script>


<style lang="scss" scoped>

</style>