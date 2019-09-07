<template>
  <div id="main" :style="{backgroundImage:'url('+require('../assets/images/lake.jpg')+')'}">
      <div >
        <div class="nav-sub ">
          <div class="nav-sub-wrapper">
            <div class="container">
              <ul class="nav-list">
                <input @change="uploadPhoto($event)" type="file" class="kyc-passin item-blue-btn">
              </ul>
            </div>
          </div>
        </div>
      </div>
    <div class="sku-box store-content " style="width: 90%" >
      <div class="gray-box">
        <header class="title-header">
          <h5 class="title">作品展示&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h5>
        </header>
        <div class="item-box" >
          <div v-for="item in imgList" v-bind:key="item.id">
          <div class="item" v-for="imgid in item.imageid" v-bind:key="imgid">
            <div>
              <div class="item-img"><img  :src="imgid" style="opacity: 1;">
              </div>
              <h6>用户名：{{item.names}}</h6>
              <div class="discount-icon">false</div>
            </div>
          </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import '../assets/css/header.css'
import '../assets/css/reset.css'
import '../assets/css/goodsList.css'
export default {
  name: 'Paper',
  inject: ['reload'],
  data () {
    return {
      msg: 'Welcome to Your Vue.js App',
      imgfile: 'http://localhost:8080/api/show?num=1&token=1',
      imgList: [],
      authors: []
    }
  },
  methods: {
    uploadPhoto (e) {
      let file = e.target.files[0]
      let newfile = new File([file], 'BlackAngle', {
        type: file.type})
      let formData = new FormData()
      formData.append('file', newfile)
      this.axios.post('/api/share?token=1', formData)
        .then((response) => {
          alert(response.data)
        })
        // eslint-disable-next-line handle-callback-err
        .catch((error) => {
          alert('wrong')
        })
      this.imgfile = 'http://localhost:8080/api/show?num=1&token=1'
    },
    // eslint-disable-next-line camelcase
    uploadFile: function (event, author_name) {
      console.log(author_name)
      this.file = event.target.files[0] // 获取input的图片file值
      let param = new FormData() // 创建form对象
      param.append('image', this.file)
      param.append('author_name', author_name) // 对应后台接收图片名
      this.axios.post('/api/file/uploadImage?token=1', param)
        .then((response) => {
          console.log(response.data)
          if (response.data.flag === true) {
            alert('添加成功！')
            this.reload()
          } else {
            alert(response.data.msc)
          }
        })
    }
  },
  created: function () {
    this.axios.get('/api/file/img?token=1')
      .then((response) => {
        console.log(response.data)
        this.imgList = response.data
      })
    // eslint-disable-next-line handle-callback-err
      .catch(error => {
        alert('hhhh')
      })
  }
}
</script>

<style scoped>
  .nav-goods-panel{
    height: 350px;
    position: relative;
    overflow: hidden;
    background: #E7E7E7;
    opacity: 0;
  }
  .active .nav-goods-panel{
    height: 350px;
    position: relative;
    overflow: hidden;
    background: #E7E7E7;
    opacity: 1;
  }
  .nav-goods-list{
    visibility: visible;
    opacity: 1;
    width: 95%;
    height: 270px;
    padding-top: 52px;
    text-align: center;
  }
  .nav-goods-list li{

    position: relative;
    display: inline-block;
    vertical-align: top;

    height: 260px;
    font-size: 14px;
  }
</style>
