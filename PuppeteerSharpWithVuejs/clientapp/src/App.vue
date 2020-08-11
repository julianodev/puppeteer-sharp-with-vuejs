<template>
    <div id="app">
        <nav class="navbar navbar-dark bg-dark">
            <a class="navbar-brand">PuppeteerSharp With Vue</a>
            <form class="form-inline">
                <input class="form-control mr-sm-2"
                       type="search"
                       placeholder="Search"
                       aria-label="Search"
                       v-model="profile">
                <button class="btn btn-outline-primary my-2 my-sm-0" type="button" @click.prevent="getPosts()">Search</button>
            </form>
        </nav>

        <div class="column d-flex justify-content-center mt-5" v-if="loading">
            <div class="spinner-border text-info align-self-center" role="status">
                <span class="sr-only">Loading...</span>
            </div>

            <div class="ml-2">
                <label>Searching posts in </label> <strong>{{profile}}</strong>
            </div>
        </div>

        <div class="container-fluid" v-if="!loading">
            <div class="row" v-if="hasPosts">
                <template v-for="(post,index) in posts">
                    <div :key="index" class="col-md-2 col-sm-3">
                        <Card :post="post" />
                    </div>
                </template>
            </div>

            <div class="d-flex justify-content-center mt-5" v-if="!hasPosts">
                <div class="alert alert-warning " role="alert">
                    No post found for profile :
                    <strong>
                        {{profile}}
                    </strong>
                </div>
            </div>

            <div class="row" v-if="hasError">
                <div class="alert alert-danger d-flex justify-content-center mt-5" role="alert">
                    An error occurred while searching for posts in
                    <strong>
                        {{profile}}
                    </strong>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import Card from './components/Card.vue'
    import httpClient from 'axios'

    const defaultProfile = 'nasa';

    export default {
        name: 'app',
        components: {
            Card
        },
        data() {
            return {
                profile: defaultProfile,
                loading: false,
                hasError: false,
                posts: new Array()
            }
        },
        computed: {
            hasPosts() {
                return this.posts.length > 0;
            }
        },
        methods: {
            getPosts() {
                this.clearPosts();
                this.loading = true;

                this.profile = this.profile ? this.profile : defaultProfile;
                const apiUrl = `api/instagram/${this.profile}`;

                httpClient.get(apiUrl)
                    .then(({ data }) => {
                        this.toggleLoading();

                        if (data.success) {
                            this.posts = data.data;
                        } else {
                            this.clearPosts();
                        }
                    }).catch(error => {

                    });
            },
            toggleLoading() {
                this.loading = !this.loading;
            },
            clearPosts() {
                this.posts = [];
            }
        },
        mounted() {
            this.getPosts();
        }
    }
</script>

<style>
</style>
