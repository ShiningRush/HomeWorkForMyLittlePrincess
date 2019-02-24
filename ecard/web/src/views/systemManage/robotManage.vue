<template>
<div style="margin-left:10px">

            <el-form :inline="true" :model="queryconditions" class="demo-form-inline">
                <el-form-item label="所属机构" >
                    <el-cascader placeholder="所属机构" :options="orgData" filterable change-on-select v-model="queryconditions.organizeIdValue"
                        :props="orgProps" @change = "initialDepartmentData(queryconditions.organizeIdValue)" clearable></el-cascader>
                </el-form-item>
                <el-form-item label="所属部门" >                     
                    <el-cascader placeholder="所属部门" :options="departmentData" filterable
                            change-on-select v-model="queryconditions.departmentId" :props="orgProps" clearable></el-cascader>
                </el-form-item>
                
                <el-form-item label="机器人名称" >
                    <el-input placeholder="机器人名称"  v-model="queryconditions.name" ></el-input>
                </el-form-item>
             
                <el-form-item>
                    <el-button type="primary" icon="el-icon-search"  @click="GetDepartment"   v-has="'search'">查询</el-button>    
                </el-form-item>
                <el-form-item>
                    <el-button  icon="el-icon-plus"  @click="openAddDialog" v-has="'Add'" >新增</el-button>    
                </el-form-item>
            </el-form>  
     <el-table :data="sys_robot"   v-loading.body="listLoading" fit highlight-current-row style="width: 100%">               
                <el-table-column prop="id" label="机器人ID"></el-table-column>
                <el-table-column prop="name" label="姓名"></el-table-column>                           
                <el-table-column prop="departmentId" label="所属部门"></el-table-column>             
                <el-table-column prop="description" label="描述"></el-table-column>
                <el-table-column prop="isValid" label="是否有效">
                  <template slot-scope="scope">
                    <el-switch v-model="scope.row.allowAutoExpand" active-color="#13ce66" inactive-color="#ff4949"  ></el-switch>
                    <!-- @change='switchChange' -->
                  </template>
                </el-table-column>
                <el-table-column label="操作" >
                    <template slot-scope="scope">
                        <el-button  size="mini"  @click="openaUpdateDialog(scope.row)"  v-has="'edit'">编辑</el-button>
                        <el-button  size="mini"  @click="deleteDepartment(scope.row)" v-has="'delete'">删除</el-button>          
                    </template>
                </el-table-column>
            </el-table>

        
            <div class="block pagination" style="text-align: center;padding-bottom:20px;">
                <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="currentPage" :page-sizes="[10, 20, 30, 50]" :page-size="10"
                layout="total, sizes, prev, pager, next, jumper" :total="total">
                </el-pagination>
            </div>
   
   
    <el-dialog :title="dialogTitle" :visible.sync="dialogFormVisible" >        
        <el-form :inline="true" :model="robotform" class="demo-form-inline" ref="robotform" :rules="rules">
            <el-row :gutter="20" style="text-align: right">       
               <el-col :span="10">
                    <el-form-item label="机器人编码"  prop="id">
                        <el-input v-model="robotform.id"   placeholder="机器人编码" class="input_width"></el-input>
                    </el-form-item>
                </el-col>       
                <el-col :span="10">
                    <el-form-item label="机器人名称"  prop="name">
                        <el-input v-model="robotform.name"   placeholder="机器人名称" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="10">                    
                    <el-form-item label="所属部门"  prop="departmentId">                     
                        <el-cascader placeholder="所属部门" :options="dialogDepartmentData" change-on-select v-model="robotform.departmentId" 
                        :props="dialogOrgProps" class="input_width"></el-cascader>
                    </el-form-item>
                </el-col>
                 <el-col :span="10">
                    <el-form-item label="是否有效">
                        <el-switch v-model="robotform.isValid" placeholder="是否有效" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="text-align: right">               
                <el-col :span="10">
                    <el-form-item label="备注">
                        <el-input v-model="robotform.description" placeholder="备注" class="input_width"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">取 消</el-button>
            <el-button type="primary" v-dbClick @click="submitDept">确 定</el-button>
        </div>
    </el-dialog>
</div>
</template>

<script>
import * as departmentApi from "../../api/department";
import * as organizeApi from "@/api/organize";
import * as robotApi from "../../api/robot";
import { getTreeObj, getParent } from "@/utils/treeListHandle";
import * as useApi from "../../api/user";

export default {
  created() {
    this.getCurrentUserOrganizes();
    this.GetRobot();
  },
  methods: {
    handleSizeChange(val) {
      this.queryconditions.pageSize = val;
      this.GetRobot();
    },
    handleCurrentChange(val) {
      // log(`当前页: ${val}`);
      this.queryconditions.currentPage = val;
      this.queryconditions.pageIndex = val;
      this.GetRobot();
    },
    //查询
    GetRobot() {
      this.listLoading = true;
      this.queryconditions.isAsc = false;
      this.queryconditions.orderBy = "id";
      robotApi
        .getRobot(this.queryconditions)
        .then(resp => {         
          this.sys_robot = resp.result;
          this.total = resp.totalCount;
          this.listLoading = false;
        })
        .catch(error => {
          this.listLoading = false;
        });
    },
     GetDepartment() {
      this.listLoading = true;
      var organizeId = this.queryconditions.organizeIdValue;
      if (organizeId && organizeId.length > 0) {
        this.queryconditions.organizeId = organizeId[organizeId.length - 1];
      }
      var departmentId = this.queryconditions.departmentIdValue;
      if (departmentId && departmentId.length > 0) {
        this.queryconditions.parentId = departmentId[departmentId.length - 1];
      }
      this.queryconditions.isAsc = false;
      this.queryconditions.orderBy = "createTime";
      departmentApi
        .GetDepartment(this.queryconditions)
        .then(resp => {
          resp.result.forEach(element => {
            if (element.createTime && element.createTime != "") {
              element.createTime = new Date(
                element.createTime
              ).toLocaleDateString();
            }
            if (element.lastModifyTime && element.lastModifyTime != "") {
              element.lastModifyTime = new Date(
                element.lastModifyTime
              ).toLocaleDateString();
            }
          });
          this.sys_department = resp.result;
          this.total = resp.totalCount;
          this.listLoading = false;
        })
        .catch(error => {
          this.listLoading = false;
        });
    },
    //删除
    deleteDepartment(row) {
      this.$confirm("此操作将永久删除该记录, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var result = [row.id];
          robotApi
            .deleteRobot(result)
            .then(resp => {
              this.GetRobot();
              this.$message({ type: "success", message: "删除成功!" });
            })
            .catch(error => {
              this.$message({ type: "success", message: "删除失败!" });
            });
        })
        .catch(() => {
          this.$message({ type: "info", message: "取消删除" });
        });
    },
    //打开添加弹窗
    openAddDialog() {
      this.dialogTitle = "添加机器人信息";
      if (this.$refs.robotform) {
        this.$refs.robotform.resetFields();
      }
      this.options = [];
      this.robotform = {};
      this.dialogFormVisible = true;
    },
    //打开修改弹窗
    openaUpdateDialog(row) {
      this.dialogTitle = "修改机器人信息";
      this.deptform = Object.assign(this.deptform, row);
      //机构特殊处理
      let organizeId = [];
      if (this.deptform.organizeId && this.deptform.organizeId != "") {
        // organizeId.push(this.deptform.organizeId);
        organizeId = getParent(this.orgList, this.deptform.organizeId);
        this.dialogInitialDepartmentData(organizeId);
      }
      this.deptform.organizeIdValue = organizeId;

      //部门特殊处理
      let parentId = [];
      let self = this;
      // alert(this.deptform.parentId);
      if (this.deptform.parentId && this.deptform.parentId != "") {
        setTimeout(function() {
          parentId = getParent(self.departmentList, self.deptform.parentId);
          // self.deptform.parentIdValue = parentId;
          self.dialogFormVisible = true;
        }, 500);
      } else {
        self.dialogFormVisible = true;
      }
    },
    //提交弹窗信息
    submitDept() {
      this.$refs.robotform.validate(valid => {
        if (valid) {
          
          if (this.dialogTitle === "添加机器人信息") {
            robotApi
              .updateRobot(this.robotform)
              .then(resp => {
                this.$message({ type: "success", message: "创建成功!" });
                this.GetRobot();
                this.dialogFormVisible = false;
              })
              .catch(error => {
                this.$message({ type: "success", message: "创建失败!" });
              });
          } else {
            robotApi
              .updateRobot(this.robotform)
              .then(resp => {
                this.$message({ type: "success", message: "修改成功!" });
                this.GetRobot();
                this.dialogFormVisible = false;
              })
              .catch(error => {
                reject(error);
                this.$message({ type: "success", message: "修改失败!" });
              });
          }
        } else {
          this.$message({
            type: "error",
            message: "数据校验出错!"
          });
          return false;
        }
      });
    },
    //获取机构下拉内容
    getCurrentUserOrganizes() {
      organizeApi
        .getCurrentUserOrganizes()
        .then(response => {
          this.orgData = getTreeObj(response);
          this.dialogOrgData = getTreeObj(response);
          this.orgList = response;
        })
        .catch(error => {
          this.$message({
            type: "error",
            message: "获取机构失败!"
          });
        });
    },
    //查询界面获取部门下拉内容
    initialDepartmentData(id) {
      this.queryconditions.departmentId = [];
      if (id && id.length > 0) {
        let organizeId = {
          organizeId: id[id.length - 1]
        };
        departmentApi
          .GetDepartmentByOrganizeId(organizeId)
          .then(response => {
            this.departmentData = getTreeObj(response);
            this.departmentList = response;
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
    //弹框获取部门下拉内容
    dialogInitialDepartmentData(id) {
      this.deptform.parentIdValue = [];
      if (id && id.length > 0) {
        let organizeId = {
          organizeId: id[id.length - 1]
        };
        departmentApi
          .GetDepartmentByOrganizeId(organizeId)
          .then(response => {
            this.dialogDepartmentData = getTreeObj(response);
            this.departmentList = response;

            useApi
              .getSimpleUsers(" ")
              .then(response => {
                this.options = getTreeObj(response);
              })
              .catch(error => {
                this.$message({
                  type: "error",
                  message: "xxxxx!"
                });
              });
          })
          .catch(error => {
            this.$message({
              type: "error",
              message: "sssss!"
            });
          });
      } else {
        this.dialogDepartmentData = [];
      }
    }
  },
  data() {
    return {
      //查询条件实体
      queryconditions: {
        name: "string",
        orderBy: "string",
        isAsc: true,
        pageIndex: 1,
        pageSize: 10,
      },
      //弹窗实体
      robotform: {
          id: "string",
          name: "string",
          departmentId: "00000000-0000-0000-0000-000000000000",
          description: "string",
          isValid: true,
          parentId:"",
          parentIdValue:""
      },
      //列表
      sys_robot: [
        {
          id: "string",
          name: "string",
          departmentId: "00000000-0000-0000-0000-000000000000",
          description: "string",
          isValid: true
        }
      ],
      dialogFormVisible: false,
      total: 0,
      currentPage: 1,
      dialogTitle: "",
      //查询级联变量
      orgList: [],
      orgData: [],
      departmentData: [],
      departmentList: [],
      orgProps: {
        value: "id",
        label: "name",
        children: "children"
      },
      //弹窗级联变量
      dialogOrgData: [],
      dialogDepartmentData: [],
      dialogOrgProps: {
        value: "id",
        label: "name",
        children: "children"
      },

      options: [],
      listLoading: false,
      rules: {
        name: [
          {
            required: true,
            message: "请输入部门名称",
            trigger: "blur"
          }
        ],
        organizeIdValue: [
          {
            type: "array",
            required: true,
            message: "请选择所属机构",
            trigger: "change"
          }
        ]
      }
    };
  }
};
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
.input_width {
  width: 215px;
}
body {
  margin: 0;
}
</style>