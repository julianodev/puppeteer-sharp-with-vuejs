<template>
    <div class="card-container">
        <div class="card" style="width: 18rem;" :title="post.description">
            <img :src="post.src" class="card-img-top" :alt="post.description">
            <div class="card-body">
                <a :href="post.link" class="btn btn-primary" target="_blank">Go</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    export default {
        name: 'Card',
        props: {
            post: Object,
            default: () => ({ description: '', src: '', link: '' })
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .card-container {
        margin:5px;
    }
</style>
