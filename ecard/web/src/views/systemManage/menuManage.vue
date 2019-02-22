<template>
<el-container>
  <el-aside width="200px"  style="border: 1px solid #eee;">
      <h5 style="text-align:center;">菜单目录</h5>
      <el-tree :data="menuTreeData" :props="defaultProps" @node-click="handleNodeClick" default-expand-all node-key="id" :expand-on-click-node="false" ></el-tree>
  </el-aside>
  <el-main>    
 <div style="margin-left:10px;margin-right:30px">  
        <el-row :gutter="20" style="text-align: right">       
            <el-col :span="24" style="text-align: right">             
                    <el-button-group>
                        <el-button  size="mini" icon="el-icon-refresh" @click ="refresh" v-has="'search'">刷新</el-button>
                        <el-button  size="mini" icon="el-icon-plus" @click="addMenu" v-has="'add'">新增</el-button>                        
                    </el-button-group>                
            </el-col>
        </el-row>
            <el-table highlight-current-row  :data="menuData" style="width: 100%" @current-change="handleMenuCurrentChange">
                <el-table-column prop="code" label="编码">
                </el-table-column>
                <el-table-column prop="name" label="名称"></el-table-column>
                <el-table-column prop="urlAddress" label="地址"></el-table-column>
                <el-table-column prop="target" label="目标"></el-table-column>               
<el-table-column prop="allowExpand" label="展开">
    <template slot-scope="scope">
       <el-switch v-model="scope.row.allowAutoExpand" active-color="#13ce66" inactive-color="#ff4949" @change='switchChange' ></el-switch>
    </template>
</el-table-column>
<el-table-column prop="isEnabled" label="有效">
    <template slot-scope="scope">
       <el-switch v-model="scope.row.isEnabled" active-color="#13ce66" inactive-color="#ff4949" @change='switchChange'></el-switch>
    </template>
</el-table-column>
<el-table-column prop="description" label="描述"></el-table-column>
<el-table-column label="操作" align="center" width="150px" >
        <div slot-scope="scope">
        <el-button  size="mini"  @click="updateMenu(scope.row)" v-has="'edit'">编辑</el-button>
        <el-button  size="mini" @click="deleteMenu(scope.row)" v-has="'delete'">删除</el-button>              
        </div>
</el-table-column> 
</el-table>  
<!--新增菜单对话框 -->
<el-dialog :visible.sync="dialogMenuFormVisible" :title="menuFormName">
    <el-tabs type="border-card" :value="index">
        <el-tab-pane name="1" :disabled="true">
            <span slot="label"><i class="el-icon-form"></i>系统功能</span>
            <el-form :model="form" ref="form" :rules="rules">
                <el-row>
                    <el-col :span="12">
                        <el-form-item label="编号" :label-width="formLabelWidth"  prop="code">
                            <el-input v-model="form.code" auto-complete="off" placeholder="请输入编号" class="select"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="名称" :label-width="formLabelWidth"  prop="name">
                            <el-input v-model="form.name" auto-complete="off" placeholder="请输入名称" class="select"></el-input>                            
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>                   
                    <el-col :span="12">
                        <el-form-item label="上级" :label-width="formLabelWidth">
                            <el-cascader placeholder="请选择上级" :options="menuTreeData" filterable change-on-select v-model="form.parentIdValue" :props="props" class="select"></el-cascader>                                                                                
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="图标" :label-width="formLabelWidth">
                            <el-select v-model="form.icon" class="select">
                              <el-option
                                v-for="item in icons"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value">
                                <span style="margin-right:10px">{{ item.label }}</span>
                                <icon-svg  :icon-class="item.label" />
                             </el-option>                               
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>                   
                    <el-col :span="12">
                        <el-form-item label="目标" :label-width="formLabelWidth"  prop="target">
                            <el-select v-model="form.target" placeholder="请选择目标" class="select">
                                <el-option label="iframe" value="iframe"></el-option>
                                <el-option label="href" value="href"></el-option>
                                <el-option label="form" value="form"></el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="排序" :label-width="formLabelWidth"  prop="sortCode">
                            <el-input v-model.number="form.sortCode" auto-complete="off" class="select"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>                    
                    <el-col :span="24">
                        <el-form-item label="地址" :label-width="formLabelWidth"  prop="urlAddress">
                            <el-input v-model="form.urlAddress" auto-complete="off"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>                    
                    <el-col :span="24">
                        <el-form-item label="选项" :label-width="formLabelWidth">                                                  
                            展开 &nbsp;&nbsp;  <el-switch
                            v-model="form.allowAutoExpand"
                            active-color="#13ce66"
                            inactive-color="#ff4949">
                            </el-switch>
                            &nbsp;&nbsp; &nbsp;&nbsp;
                            有效 &nbsp;&nbsp;
                              <el-switch
                            v-model="form.isEnabled"
                            active-color="#13ce66"
                            inactive-color="#ff4949">
                            </el-switch>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row>                    
                    <el-col :span="24">
                        <el-form-item label="描述" :label-width="formLabelWidth">
                            <el-input v-model="form.description" auto-complete="off" type="textarea"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
        </el-tab-pane>
        <el-tab-pane name="2" :disabled="true">
            <span slot="label"><i class="el-icon-date"></i> 系统按钮</span>
            <div style="margin-bottom:10px;text-align:right">
                 <el-button-group >
                <el-button  size="mini" icon="el-icon-plus" @click="addButton">新增</el-button>
                <el-button  size="mini" icon="el-icon-edit" @click="updateButton">编辑</el-button>
                <el-button  size="mini" icon="el-icon-delete" @click="deleteButton">删除</el-button>
            </el-button-group>
            </div >           
            <el-table :data="buttonData"  style="width: 100%" @current-change="handleButtonCurrentChange" highlight-current-row>
                <el-table-column prop="name" label="名称" width="180">
                </el-table-column>
                <el-table-column prop="code" label="编号" width="180">
                </el-table-column>
                <el-table-column prop="webAPI" label="地址">
                </el-table-column>                
            </el-table>
        </el-tab-pane>
    </el-tabs>
    <div slot="footer" class="dialog-footer">
        <el-button  @click="goLast" v-show="isShowLast">上一步</el-button>
        <el-button  @click="goNext" v-show="isShowNext">下一步</el-button>
        <el-button type="primary" @click="submitMenu" v-dbClick>确 定</el-button>
    </div>
</el-dialog>
<!--新增菜单按钮对话框 -->
<el-dialog :visible.sync="dialogButtonFormVisible" :title="buttonFormName" width="330px">         
            <el-form :model="button" ref="button" :rules="buttonRules">   
                        <el-form-item label="编号" :label-width="formLabelWidth"  prop="code">
                            <el-input v-model="button.code" auto-complete="off" placeholder="请输入编号"></el-input>
                        </el-form-item>                   
                        <el-form-item label="名称" :label-width="formLabelWidth"  prop="name">
                            <el-input v-model="button.name" auto-complete="off" placeholder="请输入名称"></el-input>                           
                        </el-form-item>                                                                     
                         <el-form-item label="地址" :label-width="formLabelWidth" >
                            <el-input v-model="button.webAPI" auto-complete="off"></el-input>
                        </el-form-item>               
            </el-form>          
    <div slot="footer" class="dialog-footer">        
        <el-button   @click="dialogButtonFormVisible = false" size="mini">取消</el-button>
        <el-button type="primary" @click="submitButton" size="mini" v-dbClick>确认</el-button>
        
    </div>
</el-dialog>
<!--提示对话框 -->
<el-dialog title="提示" :visible.sync="dialogVisible" width="30%" >
  <span>{{this.tips}}</span>
  <span slot="footer" class="dialog-footer">
    <el-button @click="dialogVisible = false">取 消</el-button>
    <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
  </span>
</el-dialog>
</div>
</el-main>
</el-container>

</template>

<script>
    import {isvalidUsername} from '@/utils/validate'
    import * as moduleApi from '@/api/module'
    import { getTreeObj,getParent } from "@/utils/treeListHandle"
    import  { NewGuid }  from '@/utils/guid'

    export default {
        created(){       
          this.getModules();
        }, 
        data() {
            return {
                //编辑菜单模型
                isShowNext:true,
                isShowLast:false,
                menuFormName: "",
                buttonFormName:"",
                dialogMenuFormVisible: false,
                dialogButtonFormVisible:false,    
                menuCurrentRow:undefined,
                buttonCurrentRow:undefined,           
                index: "1",                
                buttonData:[],               
                button:{
                    id:"",                   
                    code: "",
                    name: "",
                    webAPI: "",
                   
                },
                props:{
                    value: "id",
                    label: "name",
                    children: "children",
               },
                form: {
                    id:"",
                    code: "",
                    name: "",
                    urlAddress: "",
                    parentId:"",
                    parentIdValue:[],
                    target: "",
                    allowAutoExpand: "",
                    isEnabled: "",
                    isMenu: true,
                    description: "",
                    moduleAuths:[],
                },
                //菜单表单校验
                 rules:{
                    name:[{ 
                        required: true, message: '请输入名称',trigger: 'blur'
                        }],
                    code:[{
                        required: true, message: '请输入编号', trigger: 'blur'
                    }],
                     target:[{
                        required: true, message: '请选择目标', trigger: 'blur'
                    }],
                    urlAddress:[{
                        required: true, message: '请输入地址', trigger: 'blur'
                    }],
                     sortCode:[
                      { required: true, message: '请输入排序'},
                      { type: 'number', message: '排序必须为数字值'}
                    ]
                    },
                //按钮表单校验
                buttonRules:{
                   name:[{ 
                        required: true, message: '请输入名称'
                        }],
                    code:[{
                        required: true, message: '请输入编号'
                    }],                   
                     
                    },
                menuData: [],
                defaultProps: {
                    children: 'children',
                    label: 'name'
                },
                menuTreeData: [],
                menuOriginalData:null,
               //查询条件
                formInline: {
                    code: "",
                    name: ""
                },
                tips:"",
                dialogVisible:false,
                formLabelWidth: "80px",    
                icons:[{
                    label:'业务查询统计',
                    value:'业务查询统计',
                },{
                    label:'卡务日志管理',
                    value:'卡务日志管理',
                },{
                    label:'卡状态管理',
                    value:'卡状态管理',
                },{
                    label:'卡用户管理',
                    value:'卡用户管理',
                },{
                    label:'字典管理',
                    value:'字典管理',
                   
                },{
                    label:'帐户信息管理',
                    value:'帐户信息管理',

                },{
                    label:'接入应用',
                    value:'接入应用',

                },{
                    label:'操作日志',
                    value:'操作日志',

                },{
                    label:'日结报表',
                    value:'日结报表',

                },{
                    label:'日结明细查询',
                    value:'日结明细查询',

                },{
                    label:'日结管理',
                    value:'日结管理',

                },{
                    label:'机构管理',
                    value:'机构管理',

                },{
                    label:'用卡明细查询',
                    value:'用卡明细查询',
                },{
                    label:'用户管理',
                    value:'用户管理',
                },{
                    label:'系统管理',
                    value:'系统管理',
                },{
                    label:'菜单管理',
                    value:'菜单管理',
                },{
                    label:'角色管理',
                    value:'角色管理',
                },{
                    label:'资金帐户管理',
                    value:'资金帐户管理',
                },{
                    label:'部门管理',
                    value:'部门管理',
                },{
                    label:'黑白名单管理',
                    value:'黑白名单管理',
                }]          
            }
        },
        methods: {
            handleNodeClick(data) {             
                //点击菜单树
                let modules = this.menuOriginalData.filter(p=>p.parentId == data.id || p.id == data.id);
                this.menuData = modules;
            },                        
            goLast() {
                this.index = "1";
                this.isShowNext = true;
                this.isShowLast = false;
            },
            goNext() {
              
                this.$refs.form.validate((valid) => {
                    if (valid) { 
                        this.index = "2";    
                        this.isShowNext = false;
                        this.isShowLast = true;             
                    }else{
                        this.$message({
                            type: 'warning',
                            message: '数据校验出错!'
                        });           
                    return false;
                    }
                });
            },
            submitButton(){
                this.$refs.button.validate((valid) => {
                    if (valid) { 
                        
                        if(this.buttonFormName == "新增按钮"){
                            let addButton = Object.assign({},this.button);
                            addButton.id = NewGuid();
                            this.buttonData.push(addButton); 
                        }   
                        else{
                            let updateButton = Object.assign({},this.button); 
                            let copy = [];
                            if (this.buttonData instanceof Array) {                               
                                for (var i = 0, len = this.buttonData.length; i < len; i++) {
                                    if(this.buttonData[i].id == updateButton.id){
                                         copy[i] =  updateButton;      
                                    }else{
                                          copy[i] = this.buttonData[i];
                                    }
                                     
                                }                              
                            }                           
                            this.buttonData = copy;
                        }   
                        this.dialogButtonFormVisible = false;               
                    } else {
                    this.$message({
                        type: 'warning',
                        message: '数据校验出错!'
                    });           
                    return false;
                    }
                });
            },
            switchChange(){
                 var data = this.menuCurrentRow;
                moduleApi.updateModuleAuth(data).then(() => {                            
                            }).catch(error => {
                            });                                       
            },
            submitMenu() {
                this.$refs.form.validate((valid) => {
                    if (valid) {  
                        if(this.form.parentIdValue && this.form.parentIdValue.length > 0){
                            let id = this.form.parentIdValue[this.form.parentIdValue.length -1];
                            this.form.parentId = id;
                        }else{
                            this.form.parentId ="";
                        }
                        let request =  this.form;
                        request.moduleAuths = this.buttonData;
                        if(this.menuFormName == "新增菜单"){
                                moduleApi.insertModuleAuth(request).then(()=>{
                                    this.getModules();
                                    this.menuData = [];
                                    this.$message({type: 'success',message: '保存菜单成功!'
                                });                                 
                            }).catch(error =>{
                                this.$message({
                                    type: 'error',message: '保存菜单失败!'
                                });      
                            });                    
                        } else{
                            moduleApi.updateModuleAuth(request).then(()=>{
                                            this.getModules();
                                                this.menuData = [];
                                                this.$message({
                                                    type: 'success',
                                                    message: '保存菜单成功!'
                                    });          
                                }).catch(error =>{
                                });                    
                        }                       
                        
                    } 
                    else {
                        this.$message({type: 'warning',message: '数据校验出错!'});           
                        return false;
                    }
                    this.dialogMenuFormVisible = false; 
                });
            },
            addMenu(){
                this.index = "1";
                if(this.$refs.form){
                    this.$refs.form.resetFields();
                }
                this.dialogMenuFormVisible = true;  
                this.menuFormName  = "新增菜单";  
                this.buttonData = [];
                this.form = {}; 
                this.form.parentIdValue =[];             
                this.isShowNext = true;
                this.isShowLast = false;
            },
            updateMenu(row){               
                this.index = "1";             
                this.dialogMenuFormVisible = true;
                this.isShowNext = true;
                this.isShowLast = false;
                this.form =  Object.assign(this.form,row);
                this.menuFormName  = "编辑菜单"; 
                let parentId = [];
                if(this.form.parentId && this.form.parentId !="" ) {                     
                   parentId =  getParent(this.menuOriginalData,this.form.parentId)
                }
                this.form.parentIdValue = parentId;
                this.getButtonList(row);
                
            },
            addButton(){
                this.dialogButtonFormVisible = true;
                if(this.$refs.button){
                    this.$refs.button.resetFields();
                }
                this.button = {};               
                this.buttonFormName = "新增按钮";
            },
            updateButton(){
                if(!this.buttonCurrentRow){
                    this.$message({type: 'warning',message: '请选择一行数据!'});
                    return ;
                }
                this.dialogButtonFormVisible = true;                                   
                this.button = Object.assign(this.button,this.buttonCurrentRow);
                this.buttonFormName = "编辑按钮";
            },            
            handleMenuCurrentChange(val){
                this.menuCurrentRow = val;
            },
             handleButtonCurrentChange(val){
                this.buttonCurrentRow = val;
            },          
            deleteMenu(row){              
                let idArray =  [];
                idArray.push(row.id);
                moduleApi.deleteModule(idArray).then(()=>{    
                    this.getModules();
                    this.menuData = [];
                    this.$message({type: 'success',message: '删除菜单成功!'});      
                }).catch(error =>{
                });                    
            },
            deleteButton(){
                if(!this.buttonCurrentRow){
                    this.$message({type: 'warning',message: '请选择一行数据!'});
                    return ;
                }              
                this.buttonData = this.buttonData.filter(p=>p.id!= this.buttonCurrentRow.id);
            },
            //获取菜单按钮
            getButtonList(row){
                let request = [];
                request.push(row.id);
                var data = {
                    moduleIds:request,
                    isIncludeModule:false
                };
                moduleApi.getModuleAuths(data).then((response)=>{                                 
                   this.buttonData= response;
                }).catch(error =>{
                this.$message({
                    type: 'error',
                    message: '获取菜单按钮失败!'
                });      
             });                    
            },                    
            refresh(){
               this.getModules(); 
            },
             //获取菜单树
            getModules(){
                moduleApi.getAllModules().then((response)=>{                                 
                    this.menuTreeData  = getTreeObj(response);
                    this.menuOriginalData = response;
                }).catch(error =>{
                });                    
            },   
            
                
           
        }
    }
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
    .select {
        width: 215px;
    }
</style>