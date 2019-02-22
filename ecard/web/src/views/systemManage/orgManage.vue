<template>
<div style="margin-left:10px;">
<el-form :inline="true" :model="listQuery">
  <el-form-item label="机构名称">
    <el-input v-model="listQuery.name" placeholder="机构名称"></el-input>
  </el-form-item>
  <el-form-item label="机构代码">
    <el-input v-model="listQuery.code" placeholder="机构代码"></el-input>
  </el-form-item>
  <el-form-item label="负责人">
    <el-input v-model="listQuery.manager" placeholder="负责人"></el-input>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="search" icon="el-icon-search" v-has="'search'">查询</el-button>
  </el-form-item>
  <el-form-item>
  <el-button  @click="add" icon="el-icon-plus" v-has="'add'">新增</el-button>
 </el-form-item>
</el-form>
<el-table :data="list"  v-loading.body="listLoading" element-loading-text="拼命加载中" fit highlight-current-row  style="width: 100%"> 

      <el-table-column align="center" label='机构名称'>
        <template slot-scope="scope">
        {{scope.row.name}}
        </template>
      </el-table-column>

      <el-table-column label="机构代码">
        <template slot-scope="scope">
          {{scope.row.code}}
        </template>
      </el-table-column>

      <el-table-column label="成立日期">
        <template slot-scope="scope">
          {{scope.row.foundedTime}}
        </template>
      </el-table-column>

      <el-table-column label="负责人">
        <template slot-scope="scope">
          {{scope.row.manager}}
        </template>
      </el-table-column>
      <el-table-column label="上级机构" align="center">
        <template slot-scope="scope">
          {{scope.row.parentName}}
        </template>
      </el-table-column>

      <el-table-column label="备注"  align="center">
        <template slot-scope="scope">
          <span>{{scope.row.description}}</span>
        </template>
      </el-table-column>
        <el-table-column label="操作" align="center" >
        <div slot-scope="scope">
        <el-button  size="mini"  @click="update(scope.$index,scope.row)" v-has="'edit'">编辑</el-button>
        <el-button  size="mini" @click="deleteOrg(scope.row)" v-has="'delete'">删除</el-button>       
        </div>
      </el-table-column>      
    </el-table>
    <div v-show="!listLoading" class="block pagination" style="text-align: center;padding-bottom:20px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </div>
<el-dialog  :visible.sync="dialogFormVisible"  :title="title" >
  <el-form :model="form" :rules="rules" ref="orgform" :label-width="formLabelWidth">
  <el-row>
    <el-col :span="12"> <el-form-item label="机构名称"   prop="name">
      <el-input v-model="form.name" auto-complete="off" class="input_width"></el-input>
    </el-form-item></el-col>
    <el-col :span="12"> 
      <el-form-item label="机构性质"  prop="nature">
        <el-select v-model="form.nature" placeholder="请选择机构性质" class="input_width">      
      </el-select>
    </el-form-item>
  </el-col>
  </el-row>
   <el-row>
    <el-col :span="12"> 
      <el-form-item label="机构代码"   prop="code">
        <el-input v-model="form.code" auto-complete="off" class="input_width"></el-input>
    </el-form-item>
  </el-col>
   <el-col :span="12"> <el-form-item label="上级机构">
     <el-cascader placeholder="请选择所属机构" :options="orgData" filterable change-on-select v-model="form.parentIdValue" :props="props" class="input_width"></el-cascader>
    </el-form-item></el-col>
  </el-row>
  <el-row>
    <el-col :span="12"> 
      <el-form-item label="成立日期"  prop="foundedTime">       
        <el-date-picker v-model="form.foundedTime"  type="date" placeholder="请选择日期" value-format="yyyy-MM-dd" format="yyyy-MM-dd">
    </el-date-picker>
    </el-form-item>
  </el-col>
  <el-col :span="12"> <el-form-item label="负责人"  >
       <el-select v-model="form.managerId" placeholder="请选择负责人" class="input_width">
          <el-option
          v-for="item in users"
          :key="item.id"
          :label="item.userName"
          :value="item.id">
    </el-option>
      </el-select>
    </el-form-item></el-col>
  </el-row>
  <el-row>   
    <el-col :span="12"> 
      <el-form-item label="电话" prop="phone" >
        <el-input v-model="form.phone" auto-complete="off" class="input_width" ></el-input>
    </el-form-item>
  </el-col>
  <el-col :span="12"> <el-form-item label="邮编" prop="postalcode">
       <el-input v-model="form.postalcode" auto-complete="off" class="input_width" ></el-input>
    </el-form-item></el-col>
  </el-row>
   <el-row>
    <el-col :span="12"> <el-form-item label="电子邮箱" prop="email">
       <el-input v-model="form.email" auto-complete="off" class="input_width"></el-input>
    </el-form-item></el-col>
    <el-col :span="12"> 
      <el-form-item label="传真" >
        <el-input v-model="form.fax" auto-complete="off" class="input_width"></el-input>
    </el-form-item>
  </el-col>
  </el-row>
   <el-row>   
    <el-col :span="12"> 
      <el-form-item label="官网">
        <el-input v-model="form.webAddress" auto-complete="off" class="input_width"></el-input>
    </el-form-item>
  </el-col>
  <el-col :span="12"> <el-form-item label="备注">
       <el-input v-model="form.description" auto-complete="off" class="input_width" ></el-input>
    </el-form-item></el-col>    
  </el-row>
   <el-row>
    <el-col :span="15"> <el-form-item label="详细地址" prop="address">  
   <el-input v-model="form.address" auto-complete="off"  placeholder="请填写详细地址" ></el-input>
    </el-form-item>
    </el-col>       
  </el-row>
 
  </el-form>
  <div slot="footer" class="dialog-footer">   
    <el-button @click="dialogFormVisible = false">取消</el-button>
    <el-button @click="submit" type="primary" v-dbClick>确 定</el-button>    
  </div>
</el-dialog>
</div>
</template>

<script>
import * as organizeApi from '@/api/organize'
import { getTreeObj,getParent } from "@/utils/treeListHandle"
import * as userApi from  '@/api/user'

export default {
  created(){
     this.getList();
     this.getCurrentOrganizesUsers();
     this.getCurrentUserOrganizes();
  },
  data() {
      return {    
        //列表返回参数
        list: [],
        total: null,
        listLoading: true,
        //查询条件
        listQuery: {
            pageIndex: 1,
            pageSize: 20,            
            name:'',
            code:'',
            manager: ''
        },
        //对话框属性
        title: '',    
        dialogFormVisible: false,
        //表单实体
        form: {
            id:"",
            name: "",
            code: "",
            foundedTime:"",
            nature:"",
            parentId:"",
            parentIdValue:[],
            managerId:"",
            managerId:"",
            phone:"",
            postalcode:"",
            email:"",
            fax:"",
            webAddress:"",
            description:"",
            address:"",  
            detailAddress:"",                              
        },
        //机构初始化数据
        orgData:[],
        orgList:[],
         props:{
              value: "id",
              label: "name",
              children: "children",
             },
        //表单校验
        rules:{
          //机构名称
          name:[{ 
            required: true, message: '请输入机构名称', trigger: 'blur' 
            }],
          //机构代码
          code:[{
            required: true, message: '请输入机构代码', trigger: 'blur'
          }], 
            //电子邮箱
          email:[{ 
             type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' 
            }],
             //电话
          phone:[{ pattern: /^(((1[3456789][0-9]{1})|(15[0-9]{1}))+\d{8})$/, message: '请输入正确的电话', trigger: 'blur' 
            }],
            //邮编
          postalcode:[{
              pattern:/^[0-9][0-9]{5}$/, message: '请输入正确的邮编', trigger: 'blur'
          }],
           //详细地址
         address:[{
              pattern:/^[A-Za-z0-9\u4e00-\u9fa5]+$/,message: '请输入正确的详细地址', trigger: 'blur' 
            }],        

        },
        
        formLabelWidth: '120px',
        users:[]
        
       
    }
  },
  methods: {
    search(){
      this.getList();
    },
    add(){
       this.dialogFormVisible = true;
       this.title = "添加机构";
       this.getCurrentUserOrganizes();
       if(this.$refs.orgform){
          this.$refs.orgform.resetFields();
       }
       this.form ={}; 
       this.form.parentIdValue = [];      
       this.form.detailAddress = "";
       
    },
    submit(){
          this.$refs.orgform.validate((valid) => {        
          if (valid) {
              //机构值特殊处理
              let parentId = this.form.parentIdValue;
              if( parentId && parentId.length > 0){
                    this.form.parentId = parentId[parentId.length-1];
              }              
            if( this.title == "添加机构"){                   
              organizeApi.addOrganize(this.form).then(()=>{
                  this.getList();
                  this.dialogFormVisible = false;
                  this.$message({
                    type: 'success',
                    message: '添加机构成功!'
                });          
             }).catch(error =>{
             })
            }else{
                organizeApi.updateOrganize(this.form).then(()=>{
                  this.getList();
                  this.dialogFormVisible = false;
                  this.$message({
                    type: 'success',
                    message: '修改机构成功!'
                });          
             }).catch(error =>{
              });
            }                      
          } else {
            this.$message({
                    type: 'error',
                    message: '数据校验出错!'
                });                  
            return false;
          }
        });
    },
     update(index,data){
        this.dialogFormVisible = true;
        this.getCurrentUserOrganizes();
        this.title = "修改机构";
        if(this.$refs.orgform){
          this.$refs.orgform.resetFields();
       }
        this.form = Object.assign(this.form,data);
        let parentId = [];
        if(this.form.parentId && this.form.parentId!=""){
          parentId = getParent(this.orgList,this.form.parentId);
           //parentId.push(this.form.parentId);          
        }
        this.form.parentIdValue = parentId;            
        
    },
    deleteOrg(data){
       let request = [];
       request.push(data.id);      
       organizeApi.deleteOrganize(request).then(()=>{
                  this.getList();
                  this.$message({
                    type: 'success',
                    message: '删除机构成功!'
                });          
             }).catch(error =>{
             })
    },
    getList(){
       this.listLoading = true;
       let request = this.listQuery;
       this.list = [];
       organizeApi.getOrganize(request).then((response)=>{                 
                 response.result.forEach(element => {
                     if(element.foundedTime && element.foundedTime != ""){
                         element.foundedTime = new Date(element.foundedTime).toLocaleDateString();                       
                     }
                     this.list.push(element) ;
                  });
                  this.total = response.totalCount; 
                  this.listLoading = false;                            
             }).catch(error =>{
                  this.listLoading = false;
             })       
    },
    handleSizeChange(val) {
      this.listQuery.pageSize = val;
      this.getList();
    },
    handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.getList();
    },
     getCurrentUserOrganizes(){
              organizeApi.getCurrentUserOrganizes().then((response)=>{                              
                   this.orgData = getTreeObj(response);   
                   this.orgList = response;             
             }).catch(error =>{
                 this.$message({
                    type: 'error',
                    message: '获取机构失败!'
                });      
             });
    },
    getCurrentOrganizesUsers(){
       let request = {
              departmentId: "",
              loginName: "",
              userName: ""
       }
       userApi.getSimpleUsers(request).then((response)=>{          
           this.users = response
       }).catch(error =>{
          this.$message({
                    type: 'error',
                    message: '获取负责人失败!'
                });      
       })
    }

  }
  
}

</script>

<style rel="stylesheet/scss" lang="scss" scoped>
 .input_width{
   width:220px;
 }
 body {
    margin: 0;
  }
</style>


