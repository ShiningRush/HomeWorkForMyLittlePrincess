<template>
<div style="margin-left:10px">
<el-form :inline="true" :model="listQuery">
  <el-form-item label="角色名称">
    <el-input v-model="listQuery.name" placeholder="角色名称"></el-input>
  </el-form-item>
  <el-form-item label="所属机构">
    <el-input v-model="listQuery.organizeName" placeholder="所属机构"></el-input>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="search" icon="el-icon-search" v-has="'search'">查询</el-button>
  </el-form-item>
  <el-form-item>
  <el-button  @click="add" icon="el-icon-plus" v-has="'add'">新增</el-button>
 </el-form-item>
</el-form>
<el-table :data="list" style="width:100%" v-loading.body="listLoading" element-loading-text="拼命加载中" fit highlight-current-row @current-change="handleMenuCurrentChange">
      <el-table-column label="角色名称">
        <template slot-scope="scope">
          {{scope.row.name}}
        </template>
</el-table-column>

<el-table-column label="所属机构">
    <template slot-scope="scope">
          {{scope.row.organizeName}}
        </template>
</el-table-column>

<el-table-column label="创建时间">
    <template slot-scope="scope">
          {{scope.row.createTime}}
        </template>
</el-table-column>

<el-table-column label="有效性" align="center">
    <template slot-scope="scope">
       <el-switch v-model="scope.row.isEnabled" active-color="#13ce66" inactive-color="#ff4949" @change='switchChange'></el-switch>
      </template>
</el-table-column>

<el-table-column label="角色描述" align="center">
    <template slot-scope="scope">
          {{scope.row.description}}
        </template>
</el-table-column>
<el-table-column label="操作" align="center" width="400px">
    <div slot-scope="scope">
        <el-button  size="mini"  @click="authorize(scope.row)" v-has="'edit'">角色授权</el-button>
        <el-button  size="mini"  @click="selectUser(scope.row)" v-has="'edit'">角色成员</el-button>
        <el-button  size="mini"  @click="update(scope.$index,scope.row)" v-has="'edit'">编辑</el-button>
        <el-button  size="mini"  @click="deleteRole(scope.row)" v-has="'edit'">删除</el-button>        
    </div>
</el-table-column>
</el-table>
<div v-show="!listLoading" class="block pagination" style="text-align: center;padding-bottom:20px;">
    <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="total">
    </el-pagination>
</div>
<!---编辑角色弹出框 -->
<el-dialog :visible.sync="dialogFormVisible" :title="title">
    <el-form :model="form" :rules="rules" ref="form">
        <el-form-item label="所属机构" :label-width="formLabelWidth" required prop="organizeId">
            <el-select v-model="form.organizeId" filterable placeholder="请选择所属机构" class="form-input">
                <el-option v-for="item in orgData" :key="item.id" :label="item.name" :value="item.id">
                </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="角色名称" :label-width="formLabelWidth" required prop="name">
            <el-input v-model="form.name" auto-complete="off" class="form-input"></el-input>
        </el-form-item>

        <el-form-item label="是否有效" :label-width="formLabelWidth" >           
                <el-switch
                    v-model="form.isEnabled"
                    active-color="#13ce66"
                    inactive-color="#ff4949">
                </el-switch>
        </el-form-item>

        <el-form-item label="角色描述" :label-width="formLabelWidth">
            <el-input v-model="form.description" auto-complete="off" type="textarea" class="form-input"></el-input>
        </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="submitRole" v-dbClick>确 定</el-button>

    </div>
</el-dialog>
<!---授权弹出框 -->
<el-dialog :visible.sync="dialogAuthorFormVisible" :title="titleName">
    <el-tabs type="border-card" :value="index">
        <el-tab-pane name="1" :disabled="true">
            <span slot="label"><i class="el-icon-menu"></i>系统功能</span>
            <div>
                <el-tree :data="menuData" show-checkbox node-key="id" ref="menuTree" default-expand-all highlight-current :props="defaultProps" :default-checked-keys="defaultMenu" >
                </el-tree>
            </div>
        </el-tab-pane>
        <el-tab-pane name="2" :disabled="true">
            <span slot="label"><i class="el-icon-date"></i> 系统按钮</span>
            <div>
                <el-tree :data="buttonData" show-checkbox node-key="id" ref="buttonTree" default-expand-all highlight-current :props="defaultProps" :default-checked-keys="defaultButton">
                </el-tree>
            </div>
        </el-tab-pane>
    </el-tabs>
    <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="goLast" v-show="isShowLast">上一步</el-button>
        <el-button type="primary" @click="goNext" v-show="isShowNext">下一步</el-button>
        <el-button type="primary" @click="submitAuthority" v-dbClick>确 定</el-button>
    </div>
</el-dialog>
<!---角色成员弹出框 -->
<el-dialog :visible.sync="dialogRoleFormVisible" :title="roleName">
    <el-container style="height: 500px; border: 1px solid #eee">
        <el-aside width="200px" style="border: 1px solid #eee">
            <h5 style="text-align:center;">部门数据</h5>
            <el-tree :data="departmentData" :props="defaultProps" @node-click="handleNodeClick" default-expand-all></el-tree>
        </el-aside>
        <el-container>
            <el-header style="padding:0px">
                <el-input v-model="userInput" placeholder="请输入要查询用户" suffix-icon="el-icon-search" @keyup.native="searchUser"></el-input>
            </el-header>
            <el-main>
                <el-checkbox-group v-model="checkeditems">
                    <el-checkbox v-for="item in allUserItems" :label="item.id" :key="item.id" border style="margin-top:10px;margin-left:10px;width:165px;height:80px" @change="roleUserChange(item.id)" >
                        工号：{{item.jobNo?maxSlice(item.jobNo):item.jobNo}}<br/>
                        用户：{{item.userName?maxSlice(item.userName):item.userName}}<br/>
                        部门：{{item.departmentName?maxSlice(item.departmentName):item.departmentName}}
                    </el-checkbox>
                </el-checkbox-group>
            </el-main>
        </el-container>
    </el-container>
    <div slot="footer" class="dialog-footer">
        <el-button @click="dialogRoleFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="submitUser" v-dbClick>确 定</el-button>
    </div>
</el-dialog>
</div>
</template>
<script>
    import {
        isvalidUsername
    } from '@/utils/validate'
    import * as roleApi from '@/api/role'
    import * as organizeApi from '@/api/organize'
    import * as moduleApi from '@/api/module'
    import {
        getTreeObj
    } from "@/utils/treeListHandle"
    import * as departmentApi from "@/api/department"
    import * as userApi from "@/api/user"

    export default {
        data() {
            return {
                isShowNext:true,
                isShowLast:false,
                userInput: '',
                roleName: '',
                //查询条件
                listQuery: {
                    pageIndex: 1,
                    pageSize: 20,
                    name: '',
                    organizeName: ''
                },
                //表格数据
                list: [],
                total: null,
                listLoading: false,
                //编辑角色模型
                form: {
                    id: "",
                    name: '',
                    organizeId: "",
                    isEnabled: "",
                    description: "",
                },
                orgData: [],
                //表单校验
                rules: {
                    organizeId: [{
                        required: true,
                        message: '请选择所属机构',
                        trigger: 'blur'
                    }],
                    name: [{
                        required: true,
                        message: '请输入角色名称',
                        trigger: 'blur'
                    }]
                },
                dialogFormVisible: false,
                title: '',
                //角色授权模型
                dialogAuthorFormVisible: false,
                index: '1',
                titleName: '',
                roleCurrentRow: undefined,
                defaultMenu: [],
                defaultButton: [],
                //菜单权限树数据
                menuData: null,
                //菜单列表数据
                menuListData: [],
                buttonData: [],
                //角色成员模型
                dialogRoleFormVisible: false,
                //所有用户
                allUserItems: [],
                //当前选中用户
                allSelectedUsers: [],
                checkeditems: [],
                departmentData: [],
                defaultProps: {
                    children: 'children',
                    label: 'name',
                    value: 'id'
                },
                formLabelWidth: '120px',
                //用户部门长度最大值
                maxLength:5,
                isChecked:false,
            }
        },
        created() {
            this.getList();
            this.getCurrentUserOrganizes();

        },

        methods: {
            search() {
                this.getList();
            },
            add() {
                this.dialogFormVisible = true;
                this.title = "添加角色";
                if (this.$refs.form) {
                    this.$refs.form.resetFields();
                }
                this.form = {};                                                             
                                      
            },
            update(index, data) {
                this.dialogFormVisible = true;
                this.title = "修改角色";
                this.form = Object.assign(this.form,data);
            },
            switchChange(){
                var data = this.roleCurrentRow;
               roleApi.updateRole(data).then(() => {                            
                            }).catch(error => {
                            });
            },
            getList() {
                this.listLoading = true;
                let request = this.listQuery;
                this.list = [];
                roleApi.getRole(request).then((response) => {
                    response.result.forEach(element => {
                        if (element.createTime && element.createTime != "") {
                            element.createTime = new Date(element.createTime).toLocaleDateString();
                        }
                        this.list.push(element);
                    });
                    this.total = response.totalCount;
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取角色失败!'
                    });
                });
                this.listLoading = false;
            },
            handleSizeChange(val) {
                this.listQuery.pageSize = val;
                this.getList();
            },
            handleCurrentChange(val) {
                this.listQuery.pageIndex = val;
                this.getList();
            },
            authorize(row) {
                this.index = "1";
                this.isShowNext = true;
                this.isShowLast = false;
                this.dialogAuthorFormVisible = true;
                this.defaultMenu = [];
                this.titleName = "角色授权——" + row.name;
                if (this.$refs.menuTree) {
                    this.$refs.menuTree.setCheckedKeys([]);
                }                
                this.getModules();
                let request = {
                    roleId: row.id
                };
                this.getRoleModuleAuth(request);  
            },
            selectUser(row) {
                this.allSelectedUsers = []; 
                this.checkeditems = [];
                this.dialogRoleFormVisible = true;
                this.roleName = "角色成员——" + row.name;
                //获取机构部门数据
                this.getdepartmentsByOrganizeId(row.organizeId);
                //获取所有用户               
                let query = {
                    departmentId: "",
                    loginName: "",
                    userName: "",
                    organizeId:row.organizeId
                };
                this.getAllUsers(query);
                //获取角色用户
                let request = {
                    roleId: row.id
                };
                roleApi.getRoleUser(request).then((response) => {

                    if (response && response.length > 0) {

                        response.forEach(p => {
                            
                           this.allSelectedUsers.push(p.userId);    
                           this.checkeditems.push(p.userId);                                                   
                        });
                    }
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取角色用户失败!'
                    });
                });
                this.userInput = '';
            },
            goLast() {
                this.index = "1";
                this.isShowNext = true;
                this.isShowLast = false;

            },
            goNext() {
              
                    this.index = "2";
                    this.isShowNext = false;
                    this.isShowLast = true;
                    let checkedMenu = this.$refs.menuTree.getCheckedNodes();                 
                    let moduleIds = [];
                    checkedMenu.forEach(element => {
                        moduleIds.push(element.id);  
                        if(element.parentId){
                            moduleIds.push(element.parentId);
                        }                                           
                    });
                    moduleIds = Array.from(moduleIds);
                    this.getAuthTree(moduleIds);               

            },
            submitRole() {
                this.$refs.form.validate((valid) => {
                    if (valid) {
                        if (this.title == "添加角色") {
                            roleApi.addRole(this.form).then(() => {
                                this.getList();
                                this.dialogFormVisible = false;
                                this.$message({
                                    type: 'success',
                                    message: '添加角色成功!'
                                });
                            }).catch(error => {
                            });

                        } else {
                            roleApi.updateRole(this.form).then(() => {
                                this.getList();
                                this.dialogFormVisible = false;
                                this.$message({
                                    type: 'success',
                                    message: '修改角色成功!'
                                });
                            }).catch(error => {
                            });
                        }
                    } else {
                        this.$message({
                            type: 'warning',
                            message: '数据校验出错!'
                        });
                        return false;
                    }
                });
            },
            submitAuthority() {
                let modulesIds = [];
                let checkedMenu = this.$refs.menuTree.getCheckedNodes();
                let moduleIds = [];
                checkedMenu.forEach(element => {
                    if(element.parentId && !element.children){
                          moduleIds.push(element.id);
                    }                   
                });
                let checkedbutton = this.$refs.buttonTree.getCheckedNodes();
                let moduleAuthIds = [];
                checkedbutton.forEach(element => {
                    if(element.parentId && !element.children){
                        moduleAuthIds.push(element.id);
                    }                   
                });
                let data = {
                    roleID: this.roleCurrentRow.id,
                    moduleIds: moduleIds,
                    moduleAuthIds: moduleAuthIds
                };
                roleApi.saveRoleModuleAuth(data).then(() => {
                    this.dialogAuthorFormVisible = false;
                    this.$message({
                        type: 'success',
                        message: '保存角色授权成功!'
                    });
                }).catch(error => {
                });



            },
            roleUserChange(userId) {
                //判断userId是否选中             
                if (this.checkeditems.indexOf(userId) != -1) {
                    this.allSelectedUsers.push(userId);
                } else {
                    this.allSelectedUsers = this.allSelectedUsers.filter(p => p != userId);
                }
            },
            submitUser() {
                let request = {
                    roleID: this.roleCurrentRow.id,
                    userIds: this.allSelectedUsers
                };
                roleApi.saveRoleUser(request).then(() => {
                    this.dialogRoleFormVisible = false;
                    this.getList();
                    this.$message({
                        type: 'success',
                        message: '保存角色用户成功!'
                    });
                }).catch(error => {
                });

            },
            handleNodeClick(row) {
                let departmentId = row.id;
                let query = {
                    departmentId: departmentId,
                    loginName: "",
                    userName: "",
                    organizeId:row.organizeId
                };
                this.getAllUsers(query);

            },
            searchUser() {
                let userName = "";
                if (this.userInput && this.userInput != "") {
                    userName = this.userInput;
                }
                let query = {
                    departmentId: "",
                    loginName: "",
                    userName: userName,
                    organizeId:this.roleCurrentRow.organizeId
                };
                this.getAllUsers(query);

            },
            getCurrentUserOrganizes() {
                organizeApi.getCurrentUserOrganizes().then((response) => {
                    this.orgData = response;
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取机构失败!'
                    });
                });
            },
            deleteRole(row) {
                let request = [];
                request.push(row.id);
                roleApi.deleteRole(request).then(() => {
                    this.getList();
                    this.$message({
                        type: 'success',
                        message: '删除角色成功!'
                    });
                }).catch(error => {
                });
            },
            //获取菜单树
            getModules(row) {
                moduleApi.getAllModules().then((response) => {
                    this.menuData = getTreeObj(response);
                    this.menuListData = this.menuData;
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取菜单失败!'
                    });
                });
            },
            //获取权限按钮树
            getAuthTree(idArray) {
                var request = {
                    moduleIds: idArray,
                    isIncludeModule:true
                };
                moduleApi.getModuleAuths(request).then((response) => {
                    this.buttonData = getTreeObj(response);
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取权限失败!'
                    });
                });
            },
            handleMenuCurrentChange(val) {
                this.roleCurrentRow = val;
            },
            //获取机构部门树
            getdepartmentsByOrganizeId(id) {
                let data = {
                    organizeId: id
                };
                departmentApi.GetDepartmentByOrganizeId(data).then((response) => {
                    if (response.length > 0) {
                        this.departmentData = getTreeObj(response);
                    }
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取机构部门失败!'
                    });
                });
            },
            //获取角色授权
            getRoleModuleAuth(id) {
                roleApi.getRoleModuleAuth(id).then((response) => {                                    
                    this.defaultMenu = response.moduleIds;
                    this.defaultButton = response.moduleAuthIds;
                    if (this.$refs.menuTree) {                  
                     this.$refs.menuTree.setCheckedKeys(this.defaultMenu);
                  }
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取角色授权失败!'
                    });
                });
            },
            //获取所有用户
            getAllUsers(request) {
                userApi.getSimpleUsers(request).then((response) => {
                    this.allUserItems = response;
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取角色授权失败!'
                    });
                });
            },
            maxSlice(v){
                return v &&v.length > this.maxLength ? v.slice(0,this.maxLength)+"...":v;
            }

        }
    }
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
    .form-input {
        width: 400px;
    }
</style>