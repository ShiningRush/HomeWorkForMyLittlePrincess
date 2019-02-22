<template>
<el-container>
  <el-aside width="300px"  style="border: 1px solid #eee;">

        <h5 style="text-align:center;">字典分类<el-button-group style="margin-left:20px;">
            <el-button icon="el-icon-plus"  size="mini" @click="addDic" v-has="'add'"></el-button>
            <el-button icon="el-icon-edit"  size="mini" @click="upDic" v-has="'edit'"></el-button>
            <el-button icon="el-icon-delete"  size="mini" @click="deleteDic" v-has="'Delete'"></el-button>  
        </el-button-group></h5>
        
    <el-tree
        :props="props"
        :data="treeData"
        @node-click="handleNodeClick"
        default-expand-all node-key="id"
        :expand-on-click-node="false" 
        >
    </el-tree>
  </el-aside>
   <el-main style="padding:0px;">
       <div style="margin-left:10px;margin-right:30px">
           <h5  v-text="headerText" style="text-align:center"></h5>
    <el-form>
      <el-form-item>
        <el-form :inline="true">
            <el-row :gutter="20" style="text-align: right">
                <el-col :span="12" style="text-align: left">
                    <el-form-item prop="keyword" label="关键字">
                        <el-input auto-complete="off" v-model="queryDetail.keyword" placeholder="请输入关键字"></el-input>
                    </el-form-item>
                    <el-form-item>
                         <el-button type="primary" icon="el-icon-search" @click="onSubmit" v-has="'search'">查询</el-button>
                         <el-button icon="el-icon-plus" @click="addList" v-has="'add'">新增</el-button>
                    </el-form-item> 
                </el-col>              
            </el-row>
        </el-form>
        </el-form-item>
        <el-form-item>
            <el-table :data="listData"  style="width: 100%"  v-loading.body="listLoading" highlight-current-row @current-change="handleListCurrentChange">
                <el-table-column prop="itemCode" label="代码"></el-table-column>
                <el-table-column prop="itemValue" label="值"></el-table-column>
                <el-table-column prop="sortCode" label="排序"></el-table-column>
                <el-table-column prop="isDefault" label="默认">
                    <template slot-scope="scope">
                      <el-switch v-model="scope.row.isDefault" disabled active-color="#13ce66" inactive-color="#ff4949" ></el-switch>
                    </template>
                </el-table-column>
                <el-table-column prop="isEnabled" label="有效">
                  <template slot-scope="scope">
                    <el-switch v-model="scope.row.isEnabled" disabled active-color="#13ce66" inactive-color="#ff4949" ></el-switch>
                  </template>
                </el-table-column>
                <el-table-column prop="allowEdit" label="允许编辑">
                  <template slot-scope="scope">
                    <el-switch v-model="scope.row.allowEdit" disabled active-color="#13ce66" inactive-color="#ff4949" ></el-switch>
                  </template>
                </el-table-column>
                <el-table-column prop="allowDelete" label="允许删除">
                  <template slot-scope="scope">
                    <el-switch v-model="scope.row.allowDelete" disabled active-color="#13ce66" inactive-color="#ff4949" ></el-switch>
                  </template>
                </el-table-column>
                <el-table-column prop="description" label="备注"></el-table-column>
                  <el-table-column label="操作" align="center" width="150px">
        <div slot-scope="scope">
        <el-button type="primary" size="mini" :disabled="!scope.row.allowEdit"  @click="upList(scope.row)" v-has="'edit'">编辑</el-button>
        <el-button type="primary" size="mini" :disabled="!scope.row.allowDelete" @click="deleteList(scope.row)" v-has="'delete'">删除</el-button>       
        </div>
      </el-table-column>      
            </el-table>
        </el-form-item>
    </el-form>
           </div>   
    
   
    <!-- 新增字典信息对话框 -->
    <el-dialog :title="FormName" :visible.sync="dialogListFormVisible" width="400px">        
        <el-form :model="listinfo" :rules="dicRules" ref="listinfo">
            <el-row :gutter="20">
                <el-col :span="20">
                   <el-form-item label="代码" :label-width="formLabelWidth"  prop="itemCode">
                            <el-input auto-complete="off" v-model="listinfo.itemCode" placeholder="请输入代码"></el-input>
                        </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="20">
                   <el-form-item label="值" :label-width="formLabelWidth"  prop="itemValue">
                            <el-input auto-complete="off" v-model="listinfo.itemValue" placeholder="请输入名称"></el-input>
                        </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="20">
                   <el-form-item label="排序" :label-width="formLabelWidth"  prop="sortCode">
                            <el-input auto-complete="off" v-model.number="listinfo.sortCode" placeholder="请输入排序"></el-input>
                        </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="10">
                    <el-form-item label="有效" :label-width="formLabelWidth" prop="isEnabled">
                        <el-switch v-model="listinfo.isEnabled" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label="默认" :label-width="formLabelWidth" prop="isDefault">
                        <el-switch v-model="listinfo.isDefault" active-color="#13ce66" inactive-color="#ff4949"></el-switch>
                    </el-form-item>
                </el-col>
            </el-row>            
            <el-row :gutter="20">
                <el-col :span="20">
                    <el-form-item label="描述" :label-width="formLabelWidth" prop="description">
                            <el-input auto-complete="off" v-model="listinfo.description" type="textarea"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
         <div slot="footer" class="dialog-footer">
                <el-button @click="dialogListFormVisible = false">取 消</el-button>  
                <el-button type="primary" @click="submitList" v-dbClick>确 定</el-button>
            </div>
    </el-dialog>
    <!-- 新增字典分类对话框 -->
    <el-dialog :title="FormName" :visible.sync="dialogDicFormVisible" width="500px">        
        <el-form :model="dicinfo" :rules="dicTypes" ref="dicinfo">
            <el-row :gutter="20">
                <el-col :span="20">
                    <el-form-item label="上级" :label-width="formLabelWidth">
                            <el-cascader placeholder="==请选择==" :options="treeData" 
                            filterable change-on-select v-model="dicinfo.parentIdValue" 
                            :props="props" class="select" clearable ></el-cascader>                                                                                
                        </el-form-item>
                </el-col>
            </el-row>
             <el-row :gutter="20">
                <el-col :span="20">
                    <el-form-item label="名称" :label-width="formLabelWidth"  prop="itemName">
                        <el-input auto-complete="off" v-model="dicinfo.itemName" placeholder="请输入名称"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="20">
                    <el-form-item label="代码" :label-width="formLabelWidth"  prop="itemCode">
                        <el-input auto-complete="off" v-model="dicinfo.itemCode" placeholder="请输入代码"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="20">
                    <el-form-item label="排序" :label-width="formLabelWidth"  prop="sortCode">
                        <el-input auto-complete="off" v-model.number="dicinfo.sortCode" placeholder="请输入排序"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row :gutter="20">
                <el-col :span="20">
                    <el-form-item label="描述" :label-width="formLabelWidth">
                        <el-input auto-complete="off" v-model="dicinfo.description" type="textarea"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
         <div slot="footer" class="dialog-footer">
                <el-button @click="dialogDicFormVisible = false">取 消</el-button>  
                <el-button type="primary" @click="submitDic" v-dbClick>确 定</el-button>
            </div>
    </el-dialog>   
  </el-main>
</el-container>
</template>

<script>
  import * as dataItemApi from "@/api/dataItem"
  import { getTreeObj,getParent } from "@/utils/treeListHandle"

  export default {
     created(){
        dataItemApi.getDataItem(this.queryinfo).then((response)=>{
            this.dicData = response
            this.treeData = getTreeObj(this.dicData)
        }).catch(error =>{
           this.$message({
              type: 'error',
              message: '获取菜单失败!'
           })  
        }),

        dataItemApi.getDataItemDetail(this.queryDetail).then((response)=>{
            this.listData = response
        }).catch(error =>{
           this.$message({
              type: 'error',
              message: '获取列表信息失败!'
           })
        })
    },
    data() {       
      return {
            isAdd:false,
            headerText: "字典数据—未选择分类",
            dicRow:undefined,
            dictCurrentRow:undefined,
            listCurrentRow:undefined,        
            tips:"",
            props: {
                value:"id",
                label: 'itemName',
                children: 'children'
            },
            treeData:[],
            dialogListFormVisible: false,
            dialogDicFormVisible: false,
            FormName : "新增菜单",
            Name:"",         
            formLabelWidth: "80px",
            count: 1,  
            queryinfo:{
                id: "",
                itemName: "",
                parentId: ""
            },
            queryDetail:{
                id: "",
                itemId: "",
                keyword: ""
            },
            dicData:[],
            dicList:[],
            listData:[],
            dicinfo:{
                parentId: "",
                parentIdValue:[],
                itemCode: "",
                itemName: "",
                sortCode: 0,
                description: ""
            },
            dicRules: {             
                itemValue: [
                      { required: true, message: '请输入值', trigger: 'blur' }
                ],
                itemCode: [
                    { required: true, message: '请输入代码', trigger: 'blur' },
                    { pattern:/^[A-Za-z0-9]+$/, message: '输入代码只能包含数字和字母', trigger: 'blur' }
                ],
                sortCode: [
                    { required: true, message: '请输入排序' },
                    { type: 'number', message: '排序必须为数字值'}
                ]                
            },
            dicTypes:{
                 itemName: [
                      { required: true, message: '请输入名称', trigger: 'blur' }
                ],
                itemCode: [
                    { required: true, message: '请输入代码', trigger: 'blur' },
                    { pattern:/^[A-Za-z0-9]+$/, message: '输入代码只能包含数字和字母', trigger: 'blur' }
                ],
                sortCode: [
                    { required: true, message: '请输入排序' },
                    { type: 'number', message: '排序必须为数字值'}
                ]                
            },
            listinfo:{ 
                id: "",
                creator: "",
                createTime: "",
                lastModifier: "",
                lastModifyTime: "",
                itemId: "",
                itemCode: "",
                itemValue: "",
                isDefault: false,
                sortCode: 0,
                isEnabled: true,
                description: ""
            },
            items:[],
            listLoading:false,
        };
    },
    methods: {
        addDic(){
            this.dialogDicFormVisible = true;
            this.FormName  = "添加字典分类";
            this.isAdd = true;
            if(this.$refs.dicinfo){
                  this.$refs.dicinfo.resetFields();
            }
            this.dicinfo = {};
        },  
        upDic(){
            //判断是否选中行
            if(!this.dicRow){               
                 this.$message({
                    type: 'info',
                    message: '请选择一行数据!'
                });
                return ;
            }           
            this.dialogDicFormVisible = true;  
            this.FormName  = "编辑字典分类";
            this.isAdd = false;
             if(this.$refs.dicinfo){
                  this.$refs.dicinfo.resetFields();
            }
            this.dicinfo = Object.assign(this.dicinfo,this.dicRow);
            let parentId = [];
            if(this.dicinfo.parentId && this.dicinfo.parentId !="" ) {
               // parentId.push(this.dicinfo.parentId);
               parentId = getParent(this.dicData,this.dicinfo.parentId);
            }
            this.dicinfo.parentIdValue = parentId;
        },
        deleteDic(){
            //判断是否选中行
            if(!this.dicRow){               
                 this.$message({
                    type: 'info',
                    message: '请选择一行数据!'
                });
                return ;
            }       
            let idArray = [];
            idArray.push(this.dicRow.id);
            dataItemApi.deleteDataItem(idArray).then(()=>{
                this.getModules();
                this.$message({
                    type: 'success',
                    message: '删除字典分类成功!'
                });
             }).catch(error =>{
            })
        },
        addList(){
               //判断是否选中行
            if(!this.dicRow){               
                this.$message({
                    type: 'info',
                    message: '请选择字典分类!'
                });
                return ;
            }
            this.dialogListFormVisible = true;  
            this.FormName  = "添加字典信息";
            this.isAdd = true; 
            if(this.$refs.listinfo){
                this.$refs.listinfo.resetFields();
            }
             Object.assign(this.$data.listinfo, this.$options.data.call(this).listinfo);
            this.listinfo.isEnabled = true;           
        },  
        upList(row){           
            this.dialogListFormVisible = true;  
            this.FormName  = "编辑字典"; 
            this.isAdd = false;
            this.listinfo = Object.assign(this.listinfo,row);
        },
        deleteList(row){           
            let idArray = [];
            idArray.push(row.id);
            dataItemApi.deleteDataItemDetail(idArray).then(()=>{
                this.getDetail();
                this.$message({
                    type: 'success',
                    message: '删除字典成功!'
                });
             }).catch(error =>{
                this.$message({
                    type: 'error',
                    message: '删除字典失败!'
                });
            })
        },
        handleListCurrentChange(val){
            this.listCurrentRow = val;
        },
        handleNodeClick(data) { 
            this.dicRow = data;
            this.headerText = "字典数据—" + data.itemName;
            if(this.$refs.queryDetail){
                this.$refs.queryDetail.resetFields();
            }
            this.queryDetail.itemId = data.id;
            this.getDetail();
        },
        submitDic(dicinfo) {
            this.$refs.dicinfo.validate((valid) => {
              if (valid) {
                     let id  = "";
                    if(this.dicinfo.parentIdValue && this.dicinfo.parentIdValue.length > 0){
                        id = this.dicinfo.parentIdValue[this.dicinfo.parentIdValue.length -1];                      
                    }
                    this.dicinfo.parentId = id;
                    if(this.isAdd){
                        dataItemApi.addDataItem(this.dicinfo).then(()=>{
                            this.getModules();
                                this.$message({
                                    type: 'success',
                                    message: '保存字典分类成功!'
                                }); 
                            this.dialogDicFormVisible = false;
                        }).catch(error =>{
                        }).catch(error =>{
                            this.$message({
                                type: 'error',
                                message: '保存字典分类失败!'
                            });
                        });
                    }else{
                        dataItemApi.updateDataItem(this.dicinfo).then(()=>{
                            this.getModules();
                                this.$message({
                                    type: 'success',
                                    message: '修改字典分类成功!'
                                }); 
                            this.dialogDicFormVisible = false;
                        }).catch(error =>{
                        this.$message({
                            type: 'error',
                            message: '修改字典分类失败!'
                        });
                    });                      
                  }
                   
                } else {
                    return false;
                }
            });
        },
        submitList(listinfo) {
            this.$refs.listinfo.validate((valid) => {
              if (valid) {
                  this.listinfo.itemId = this.dicRow.id;
                    if(this.isAdd){
                        dataItemApi.addDataItemDetail(this.listinfo).then(()=>{
                            this.getDetail();
                                this.$message({
                                    type: 'success',
                                    message: '保存字典成功!'
                                }); 
                            this.dialogListFormVisible = false;
                        }).catch(error =>{
                        }).catch(error =>{
                            this.$message({
                                type: 'error',
                                message: '保存字典失败!'
                            });
                        });
                    }else{
                        dataItemApi.updateDataItemDetail(this.listinfo).then(()=>{
                            this.getDetail();
                                this.$message({
                                    type: 'success',
                                    message: '修改字典成功!'
                                }); 
                            this.dialogListFormVisible = false;
                        }).catch(error =>{
                        this.$message({
                            type: 'error',
                            message: '修改字典失败!'
                        });
                    });                      
                  }
                   
                } else {
                    return false;
                }
            });
        },
        switchChange(){
             var data = this.listCurrentRow;
             dataItemApi.updateDataItemDetail(data).then(() => {                            
                            }).catch(error => {
                            });
            },
        onSubmit(){
            this.getDetail();
        },
        //获取菜单树
        getModules(){
            dataItemApi.getDataItem(this.queryinfo).then((response)=>{
                this.dicData = response
                this.treeData = getTreeObj(this.dicData)
            }).catch(error =>{
                this.$message({
                    type: 'error',
                    message: '获取菜单失败!'
                });      
            });                    
        },     
        getDetail(){
        this.listLoading = true;
        dataItemApi.getDataItemDetail(this.queryDetail).then((response)=>{
                this.listData = response;
                this.listLoading = false;
            }).catch(error =>{
            this.$message({
                type: 'error',
                message: '获取列表信息失败!'
            });
            this.listLoading = false;
            })
        }
    },
  };
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
 .el-header {
    text-align: center,left;
    line-height: 30px;    
    color: #5a5e66;
  }
  
 .select {
        width: 300px;
    }
  body {
    margin: 0;
  }
</style>