<template>
  <el-menu mode="vertical" :default-active="$route.path">
    <sidebar-item :modules="modules"></sidebar-item>
  </el-menu>
</template>

<script>
import SidebarItem from './SidebarItem'
import {mapGetters} from 'vuex'

export default {
  components: { SidebarItem },
  computed: {
    ...mapGetters(["modules"])
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.el-menu {
  min-height: 100%;
}
</style>
