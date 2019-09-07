<template>
  <div id="app">
    <router-view v-if="isAlive">
    </router-view>
  </div>
</template>

<script>
import Header from './components/header.vue'
export default {
  name: 'App',
  provide () {
    return {
      reload: this.reload
    }
  },
  data () {
    return {
      isAlive: true
    }
  },
  components: {Header},
  methods: {
    reload () {
      this.isAlive = false
      this.$nextTick(function () {
        this.isAlive = true
      })
    }
  }

}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
