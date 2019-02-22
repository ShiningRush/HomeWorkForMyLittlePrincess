<template>
  <div>
    <template v-for="item in modules" >
      <router-link v-if="!item.children || item.children.length == 0" :to="item.urlAddress" :key="item.id">
        <el-menu-item :index="item.urlAddress">
          <icon-svg v-if='item.icon' :icon-class="item.icon" /> {{item.name}}
        </el-menu-item>
      </router-link>
      <el-submenu :index="item.name" v-if="item.children && item.children.length>0" :key="item.id" >
        <template slot="title">
          <icon-svg v-if='item.icon' :icon-class="item.icon" /> {{item.name}}
        </template>
        <template v-for="child of item.children" >
          <sidebar-item class='menu-indent' v-if='child.children&&child.children.length>0' :key="child.id" :modules='[child]'></sidebar-item>
          <router-link v-else class="menu-indent" :to="child.urlAddress" :key="child.id" >
            <el-menu-item :index="child.urlAddress" class="submenu">
              <icon-svg v-if='child.icon' :icon-class="child.icon" />{{ child.name }}
            </el-menu-item>
          </router-link>
        </template>
      </el-submenu>
    </template>
  </div>
</template>

<script>
export default {
  name: 'SidebarItem',
  props: {
    modules: {
      type: Array
    }
  }
}
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.svg-icon {
  margin-right: 10px;
}
.hideSidebar .menu-indent{
  display: block;
  text-indent: 10px;
}
.submenu{
  background-color:#F8F8FF
}
</style>

