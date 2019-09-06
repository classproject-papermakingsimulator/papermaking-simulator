// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import Element from 'element-ui'
import axios from 'axios'
import Qs from 'qs'
import App from './App'
import router from './router'

Vue.config.productionTip = false

Vue.use(Element)
axios.defaults.headers.common['token'] = '1'
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded'
Vue.prototype.axios = axios
Vue.prototype.qs = Qs
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
