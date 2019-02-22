<template>
  
</template>

<script>
  import { ssoBind } from '@/api/ssoAuthenticate'
  import { Message } from 'element-ui';


export default {
  created(){
    if(this.$route.query.userId && this.$route.query.appId){
      this.loading = true
      ssoBind(this.$route.query.userId, this.$route.query.appId).then((resp)=>{
        const res = resp.data
        if(res.code !== '0'){
          Message({
            message: '统一门户帐号绑定失败1',
            type: 'success'
          })
        }
        else{
          Message({
            message: '统一门户帐号绑定成功',
            type: 'success'
          })
        }


        this.loading = false
        this.returnToRoot()

      }).catch((error)=>{
        this.loading = false
        Message({
          message: '统一门户帐号绑定失败123',
          type: 'error'
        })

        this.returnToRoot()
      })
    }
    else{
      this.$router.push({ path: '/' })
    }
  },
  data() {
    return {
    }
  },
  methods: {
    returnToRoot(){
      setTimeout(() => {
        this.$router.push({ path: '/' })
      }, 3000)
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
</style>