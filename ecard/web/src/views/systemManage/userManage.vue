<template>
<div style="margin-left:10px">
    <el-form>
        <!-- 查询条件区域 Begin-->
         <el-form :inline="true" :model="listQuery">
                <el-form-item label="所属机构"  >
                    <el-cascader  placeholder="所属机构" :options="orgData" filterable change-on-select v-model="listQuery.organizeIdValue"
                            :props="orgProps" @change = "queryInitialDepartmentData(listQuery.organizeIdValue)" clearable></el-cascader>
                </el-form-item>
                <el-form-item label="所属部门" >                     
                    <el-cascader placeholder="所属部门" :options="queryDepartmentData" filterable
                            change-on-select v-model="listQuery.departmentIdValue" :props="orgProps" clearable></el-cascader>
                </el-form-item>
                <el-form-item label="登录名">
                    <el-input v-model="listQuery.loginName" placeholder="登录名"></el-input>
                </el-form-item>
                <el-form-item label="用户名">
                    <el-input v-model="listQuery.userName" placeholder="用户名"></el-input>
                </el-form-item>
                <el-form-item label="联系电话">
                    <el-input v-model="listQuery.mobile" placeholder="联系电话"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" icon="el-icon-search" @click="onQuery" v-has="'Search'">查询</el-button>
                </el-form-item>
                <el-form-item>
                    <el-button  icon="el-icon-plus" @click="openAddDialog" v-has="'Add'">新增</el-button>
                </el-form-item>
            </el-form>
        <!-- 查询条件区域 End-->
        <!-- 表格区域 Begin-->
        <div>
            <el-table :data="list"   v-loading.body="listLoading"  element-loading-text="拼命加载中" fit highlight-current-row style="width: 100%">              
                <el-table-column prop="organizeName" label="所属机构"></el-table-column>
                <el-table-column prop="loginName" label="登录名"></el-table-column>
                <el-table-column prop="userName" label="用户名"></el-table-column>
                <el-table-column prop="jobNo" label="工号"></el-table-column>
                <el-table-column prop="mobile" label="联系电话"></el-table-column>
                <el-table-column prop="eMail" label="邮箱"></el-table-column>            
                <el-table-column prop="remark" label="备注"></el-table-column>
                <el-table-column label="操作" width="300">
                    <template slot-scope="scope">
                        <el-button  size="mini"  @click="openaUpdateDialog(scope.row)" v-has="'edit'">编辑</el-button>
                        <el-button  size="mini"  @click="delUserById(scope.row)" v-has="'delete'">删除</el-button>
                        <el-button  size="mini"  @click="resetPassword(scope.row)" v-has="'delete'">重置密码</el-button>                       
                    </template>
                </el-table-column>
            </el-table>
        </div>
        <!-- 表格区域 End-->
        <div v-show="!listLoading">
            <div class="block pagination" style="text-align: center;padding-bottom:20px;">
               <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.Pagesize" layout="total, sizes, prev, pager, next, jumper" :total="total">
               </el-pagination>
            </div>
        </div>
    </el-form>
    <el-dialog :title="dialogTitle" :visible.sync="dialogFormVisible">        
        <el-form :inline="true" :model="form"  ref="orgform">
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">
                    <el-form-item label="所属机构" required>
                        <el-cascader placeholder="所属机构" :options="orgData" change-on-select v-model="form.organizeIdValue"
                          :props="orgProps" @change = "initialDepartmentData(form.organizeIdValue,true)" class="input_width"></el-cascader>
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label="所属部门" required>                     
                        <el-cascader placeholder="所属部门" :options="departmentData" change-on-select v-model="form.parentIdValue" 
                          :props="orgProps" class="input_width"></el-cascader>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">
                    <el-form-item label="登录名" required>
                        <el-input v-model="form.loginName" placeholder="登录名" class="input_width"></el-input>
                    </el-form-item>                    
                </el-col>
                <el-col :span="10">
                    <el-form-item label="用户名" required>
                        <el-input v-model="form.userName" placeholder="用户名" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">
                    <el-form-item label="职级">
                        <el-select v-model="form.professionalLevel" placeholder="职级" class="input_width">
                            <el-option v-for="item in professionalLevelOptions" :key="item.code" :label="item.value" :value="item.code">
                            </el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="10">                    
                    <el-form-item label="职务" >
                        <el-select v-model="form.duty" placeholder="职务" class="input_width">
                            <el-option v-for="item in dutyOptions" :key="item.code" :label="item.value" :value="item.code">
                            </el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">                    
                    <el-form-item label="工号">
                        <el-input v-model="form.jobNo" placeholder="工号" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label="邮件">
                        <el-input v-model="form.eMail" placeholder="邮件" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">
                    <el-form-item label="联系电话">
                        <el-input v-model="form.mobile" placeholder="联系电话" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">
                    <el-form-item label="备注">
                            <el-input  v-model="form.remark" placeholder="备注" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
        
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">取 消</el-button>
            <el-button type="primary" @click="submitUser" v-dbClick>确 定</el-button>
        </div>
    </el-dialog>
</div>
</template>

<script>
import * as useApi from "../../api/user";
import { getTreeObj } from "@/utils/treeListHandle";
import * as organizeApi from "@/api/organize";
import * as departmentApi from "@/api/department";
import * as dataItemApi from "@/api/dataItem";
import * as treeListHandleApi from "@/utils/treeListHandle"
 
export default {
  created() {
    this.getprofessionalLevels();
    this.getCurrentUserOrganizes();
    this.getList();
  },
  //获取部门下拉
  data() {
    return {
      
     
      //查询条件
      listLoading: true,
      //查询条件
      listQuery: {
        organizeIdValue: "",
        organizeId: "",
        pageIndex: 1,
        pagesize: 10,       
        departmentIdValue: "",
        departmentId: "",
        loginName: "",
        userName: "",
        mobile: "",
        isAsc: true,
      },

      //返回列表
      list: [],
      total: null,

      //是否弹窗
      dialogFormVisible: false,
      //表单实体
      form: {       
        id: "", //用户id（提交字段）
        loginName: "string", //登录名（提交字段）
        userName: "string", //用户名（提交字段）
        jobNo: "string", //工号（提交字段）
        mobile: "string", //手机（提交字段）
        eMail: "string", //邮件（提交字段）
        professionalLevel: "string", //职级（提交字段）
        duty: "string", //职务（提交字段）
        remark: "string", //备注（提交字段）

        departmentIds:[],//所属部门（提交字段）
        organizeId: "", //所属机构id（提交字段）
        
        parentIdValue:[],//选中departmentIds
        organizeIdValue: [],//选中
      },
      
      //查询条件下拉
      queryOrgData: [],//机构下拉框数据
      queryOrgList:[],
      queryDepartmentData: [],//部门下拉数据
      queryDepertmentList:[],//指定机构下所有部门

      //弹窗下拉
      orgData: [],//机构下拉框数据
      orgList:[],
      departmentData: [],//部门下拉数据
      depertmentList:[],//指定机构下所有部门
      orgProps: {
        value: "id",
        label: "name",
        children: "children"
      },
      dialogTitle: "",//弹窗标题
      professionalLevelOptions:[],//弹框职级下拉内容
      dutyOptions:[],//弹框职务下拉内容
    };
  },

  methods: {
    //分页方法
    handleSizeChange(val) {
      this.listQuery.Pagesize = val;
      this.getList();
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.getList();
    },
    //获取数据列表
    getList() {
      this.listLoading = true;
      let request = this.listQuery;
      var organizeId = this.listQuery.organizeIdValue;
      if (organizeId && organizeId.length > 0) {
        request.organizeId = organizeId[organizeId.length - 1];
      }
      var departmentId = this.listQuery.departmentIdValue;
      if (departmentId && departmentId.length > 0) {
        request.departmentId = departmentId[departmentId.length - 1];
      }
      this.list = [];
      useApi
        .getUsers(request)
        .then(response => {
          response.result.forEach(element => {
            this.list.push(element);
          });
          this.total = response.totalCount;
        })
        .catch(error => {
          this.$message({
            type: "error",
            message: "获取用户失败!"
          });
        });
      this.listLoading = false;
    },
    //查询
    onQuery() {
      this.getList();
    },
    //打开添加用户对话框新增
    openAddDialog() {
      this.dialogFormVisible = true;
      this.dialogTitle = "新增用户信息";
      if (this.$refs.orgform) {
        this.$refs.orgform.resetFields();
      }
      this.form = {};     
      
      var aa=this.professionalLevelOptions.filter(s => s.isDefault == true);
      if(aa!=null)   
        this.form.professionalLevel=aa[0].name;

      var bb=this.dutyOptions.filter(s => s.isDefault == true);
      if(bb!=null)   
        this.form.duty=bb[0].name;
    },
    //打开修改用户对话框新增
    openaUpdateDialog(row) {
      this.dialogTitle = "修改用户信息";
      this.form  = Object.assign(this.form,row);
      //机构特殊处理
      let organizeId = [];
      if(this.form.organizeId && this.form.organizeId != ""){
          organizeId = treeListHandleApi.getParent(this.orgList,this.form.organizeId);                         
      }  
      this.form.organizeIdValue = organizeId;          
      this.initialDepartmentData(organizeId,null); 
      //部门特殊处理
      let parentId = [];
      let self = this;
      // alert(self.depertmentList,Object.keys(self.form.departments)[0]);
      if(this.form.departments ){    
        setTimeout(function(){          
          parentId = treeListHandleApi.getParent(self.depertmentList,Object.keys(self.form.departments)[0]);  
          self.form.parentIdValue = parentId;
          self.dialogFormVisible = true;        
        },500)                               
      }     
    },
    //删除
    delUserById(val) {
      this.$confirm("此操作将永久删除该记录, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          useApi
            .delUserById(val.id)
            .then(resp => {
              this.getList();
              this.$message({ type: "success", message: "删除成功!" });
            })
            .catch(error => {
              console.log(error) 
              this.$message({ type: "error", message: "删除失败!" });
            });
        })
        .catch(() => {
          this.$message({ type: "info", message: "取消删除" });
        });
    },
    //重置密码
    resetPassword(val) {
      this.$confirm("此操作将密码重置, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          useApi
            .restUserPassword(val.id)
            .then(resp => {
              this.$message({ type: "success", message: "密码重置成功!" });
            })
            .catch(error => {
              console.log(error) 
              this.$message({ type: "error", message: "密码重置失败!" });
            });
        })
        .catch(() => {
          this.$message({ type: "info", message: "取消密码重置" });
        });
    },
    //提交用户表单
    submitUser() {
      if(this.form.organizeIdValue == null ||this.form.organizeIdValue.length == 0)
      {
        this.$message.error('请选择机构！！！');     
      }
      else if(this.form.parentIdValue == null ||this.form.parentIdValue.length == 0)
      {
        this.$message.error('请选择部门！！！');     
      }
      else if(this.form.loginName == null ||this.form.loginName == "")
      {
        this.$message.error('请填写登录名！！！');     
      }
      else if(this.form.userName == null ||this.form.userName == "")
      {
        this.$message.error('请填写用户名！！！');     
      }
      else{
        let organizeId = this.form.organizeIdValue;
        if (organizeId && organizeId.length > 0) {
          this.form.organizeId = organizeId[organizeId.length - 1];
        }
        let parentId = this.form.parentIdValue;
        if (parentId && parentId.length > 0) {
          this.form.departmentIds = [parentId[parentId.length - 1]];
        }

        console.log(this.form);
        if (this.dialogTitle == "新增用户信息") {
          useApi
            .createUser(this.form)
            .then(resp => {              
              this.$message({ type: "success", message: "添加用户成功!" });
              this.getList();
              this.dialogFormVisible = false;
            })
            .catch(error => {
              console.log(error) 
              this.$message({ type: "error", message: "添加用户失败!" });
              console.debug(error);
            });
        } else {
          useApi
            .updateUser(this.form)
            .then(resp => {
              this.$message({ type: "success", message: "修改用户成功!" });
              this.getList();
              this.dialogFormVisible = false;
            })
            .catch(error => {
              console.log(error) 
              this.$message({ type: "error", message: "修改用户失败!" });
              console.debug(error);
            });
        }
      }
    },
    //获取机构下拉内容
    getCurrentUserOrganizes() {
      organizeApi
        .getCurrentUserOrganizes()
        .then(response => {
          this.orgData = getTreeObj(response);
          this.orgList = response;
        })
        .catch(error => {
          this.$message({
            type: "error",
            message: "获取机构失败!"
          });
        });
    },
    //获取弹窗部门下拉内容
    initialDepartmentData(id,ischange) {
      if (ischange)
       this.form.parentIdValue=[];
      if (id && id.length > 0) {
        let data = {
          organizeId: id[id.length - 1]
        };
        departmentApi
          .GetDepartmentByOrganizeId(data)
          .then(response => {
            this.departmentData = getTreeObj(response);
            this.depertmentList = response;
          })
          .catch(error => {
            this.$message({
              type: "error",
              message: "获取机构部门失败!"
            });
          });
      } else {
        this.departmentData = [];
      }
    },

    //获取查询条件部门下拉内容
    queryInitialDepartmentData(id) {
      this.listQuery.departmentIdValue=[];
      if (id && id.length > 0) {
        let data = {
          organizeId: id[id.length - 1]
        };
        departmentApi
          .GetDepartmentByOrganizeId(data)
          .then(response => {
            console.log(response);
            this.queryDepartmentData = getTreeObj(response);
            this.queryDepertmentList = response;
          })
          .catch(error => {
            this.$message({
              type: "error",
              message: "获取机构部门失败!"
            });
          });
      } else {
        this.queryDepartmentData = [];
      }
    },


    //获取职级，职务
    getprofessionalLevels(){
      //职级
      dataItemApi.getDetailByCode("ProfessionalLevel").then(resp=>{
        this.professionalLevelOptions = resp;
      }).catch(error=>{
          console.debug(error)
      });

      //职务
      dataItemApi.getDetailByCode("UserDuty").then(resp=>{
        this.dutyOptions = resp;
      }).catch(error=>{
          console.debug(error)
      });
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.input_width{
   width:215px;
 }
</style>
