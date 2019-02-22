<template>
    <div style="margin-left:10px;margin-right:20px;margin-top:30px">
    <!-- 查询控件 -->
        <el-form :inline="true" :model="queryconditions" class="demo-form-inline">
            <el-row :gutter="24">
                <el-form-item label="黑白名单" >
                    <el-select v-model="queryconditions.type" placeholder="请选择" clearable>
                        <el-option v-for="item in blackOrWhiteListOptions" :key="item.code" :label="item.value" :value="item.code"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="就诊卡号">
                    <el-input v-model="queryconditions.cardNo" placeholder="就诊卡号"></el-input>
                </el-form-item>
                <el-form-item label="姓名">
                    <el-input v-model="queryconditions.name" placeholder="姓名"></el-input>
                </el-form-item>
                <el-form-item label="身份证号">
                    <el-input v-model="queryconditions.idCardNo" placeholder="身份证号"></el-input>
                </el-form-item>
                <el-button type="primary" @click="searchClick" icon="el-icon-search" v-has="'Search'" >查询</el-button>
                <el-button  @click="openAddDialog" icon="el-icon-plus" v-has="'Add'" >新增</el-button>
            </el-row>
            
            <el-row :gutter="24">
                <el-form-item label="查询类型" >
                    <el-select v-model="queryconditions.dateType" placeholder="请选择" clearable>
                        <el-option v-for="item in Options" :key="item.value" :label="item.label" :value="item.value"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="开始日期">
                    <el-date-picker v-model="queryconditions.beginTime" type="date" placeholder="选择日期" value-format="yyyy-MM-dd" format="yyyy-MM-dd"> </el-date-picker>
                </el-form-item>
                <el-form-item label="结束日期">
                    <el-date-picker v-model="queryconditions.endTime" type="date" placeholder="选择日期" value-format="yyyy-MM-dd" format="yyyy-MM-dd"> </el-date-picker>
                </el-form-item>
            </el-row>
        </el-form>
    <!-- Table展示 -->
        <el-table :data="blackOrWhiteLists" v-loading.body="listLoading" fit highlight-current-row style="width: 100%">  
            <el-table-column fixed type="index" width="50" label="序号"></el-table-column>
            <el-table-column prop="typeName" label="名单类型"></el-table-column>
            <el-table-column prop="cardNo" label="就诊卡号"></el-table-column>
            <el-table-column prop="name" label="姓名" ></el-table-column>
            <el-table-column prop="idCardNo" label="身份证号"></el-table-column>
            <el-table-column prop="cardStatus" label="卡状态"></el-table-column>
            <el-table-column prop="beginValidDate" label="有效开始时间"></el-table-column>
            <el-table-column prop="endValidDate" label="有效结束时间"></el-table-column>
            <el-table-column prop="remark" label="备注"></el-table-column>
            <el-table-column  label="操作" width="200">
                <template slot-scope="scope">
                    <el-button @click="openaUpdateDialog(scope.row)"  size="mini" v-has="'Edit'" > 修改</el-button>
                    <el-button @click="DeleteblickClick(scope.row)"  size="mini"  v-has="'Delete'">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <div class="block pagination" style="text-align: center;padding-bottom:20px;">
            <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="queryconditions.pageIndex" :page-sizes="[10,20,30,50]" :page-size="queryconditions.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="queryconditions.total"></el-pagination>
        </div>
    <!-- 新增修改弹窗 -->
        <el-dialog :title="dialogTitle" :visible.sync="dialogFormVisible" width="30%" >        
            <el-form :inline="true" :model="blacklist" class="demo-form-inline" ref="orgform">
                <el-row>
                    <el-form-item label="就诊卡号" required :label-width="formLabelWidth">
                        <el-input v-model="blacklist.cardNo" required  placeholder="卡号" class="input_width"></el-input>
                    </el-form-item>
                </el-row>
                <el-row>
                    <el-form-item label="有效开始时间" required :label-width="formLabelWidth">
                        <div class="block">
                            <span class="demonstration"></span>
                            <el-date-picker v-model="blacklist.beginValidDate" type="date" placeholder="选择日期" value-format="yyyy-MM-dd" format="yyyy-MM-dd" class="input_width" ></el-date-picker>
                        </div>
                    </el-form-item>
                </el-row>
                <el-row>
                    <el-form-item label="有效结束时间" required :label-width="formLabelWidth">
                        <div class="block">
                            <span class="demonstration"></span>
                            <el-date-picker v-model="blacklist.endValidDate" type="date" placeholder="选择日期" value-format="yyyy-MM-dd" format="yyyy-MM-dd" class="input_width"></el-date-picker>
                        </div>
                    </el-form-item>
                </el-row>
                <el-row>
                    <el-form-item label="名单类型" required :label-width="formLabelWidth">
                        <el-select v-model="blacklist.type" placeholder="请选择" class="input_width">
                            <el-option v-for="item in blackOrWhiteListOptions" :key="item.code" :label="item.value" :value="item.code"></el-option>
                        </el-select>
                    </el-form-item>
                </el-row>
                <el-row>
                    <el-form-item label="备注" :label-width="formLabelWidth">
                        <el-input v-model="blacklist.remark" required  placeholder="备注" class="input_width"></el-input>
                    </el-form-item>                  
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogFormVisible = false">取 消</el-button>
                <el-button type="primary" @click="submitBlk" v-dbClick>确 定</el-button>
            </div>
        </el-dialog>   
    </div>
</template>
<script>
import * as blacklistApi from '../../api/blacklist';
import * as dataItemApi from '../../api/dataItem';
export default {
    created(){
            this.search();
            this.getBlacklistCode();
        },
    data() {
        return {
        //查询条件实体
            queryconditions:{
                type:'',//黑白名单
                cardNo:'',//卡号
                name:'',//姓名
                idCardNo:'',//身份证
                beginTime:'',//开始日期
                endTime:'',//结束日期
                pageIndex:1,//当前页码
                pageSize: 10,//页码大小
                orderBy: 'cardNo',//排序字段
                isAsc: true,//降序或升序
                total:10,//总条数
                dateType:0,//查询类型
            },
        //日期控件的快捷键
            pickerOptions: {
                shortcuts: [{
                    text: '最近一周',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                        picker.$emit('pick', [start, end]);
                    }
                }, 
                {
                    text: '最近一个月',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                        picker.$emit('pick', [start, end]);
                    }
                }, 
                {
                    text: '最近三个月',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                        picker.$emit('pick', [start, end]);
                    }
                }]
            },
        //日期选择变量
            datePickerValue: '',
        //黑白名单表格对象
            blackOrWhiteLists: [],
        //弹窗实体
            blacklist:{
                    id:'1',
                    appId:'1',
                    cardNo:'1',
                    beginValidDate: '',
                    endValidDate: '',
                    type: '1',
                    remark:'1',
            },
        //查询控件黑白名单下拉选择项
            blackOrWhiteListOptions: [],
        //查询类型下拉选项
            Options: [
                {
                    label: '有效开始时间',
                    value: 0
                }, 
                {
                    label: '有效结束时间',
                    value: 1
                }
            ],
        //新增修改弹出变量
            dialogTitle:'',//窗体标题
            dialogFormVisible:false,//是否打开窗体
        //label宽度
            formLabelWidth:'190px',
            listLoading: false, 
            
        }
  },
  methods: {
    //查询
        search(){
            this.listLoading = true;
            blacklistApi.GetBlacklist(this.queryconditions).then(resp=>{
                resp.result.forEach(element => {
                     if(element.beginValidDate && element.beginValidDate != ""){
                         element.beginValidDate = new Date(element.beginValidDate).toLocaleDateString();                
                     }
                     if(element.endValidDate && element.endValidDate != ""){
                         element.endValidDate = new Date(element.endValidDate).toLocaleDateString();                    
                     }
                     if(element.createTime && element.createTime != ""){
                         element.createTime = new Date(element.createTime).toLocaleDateString();                    
                     }
                });
                this.blackOrWhiteLists = resp.result;
                this.queryconditions.total = resp.totalCount;
                this.listLoading = false;
            }).catch(error=>{
                this.listLoading = false;
            });
        },
    //删除
        DeleteblickClick(row){
                this.$confirm('此操作将永久删除该记录, 是否继续?', '提示', {confirmButtonText: '确定',cancelButtonText: '取消',type: 'warning'}).then(() => {
                blacklistApi.DeleteBlacklist(row.id).then(resp=>{
                    this.$message({type: 'success',message: '删除成功!'});
                    this.search();
                }).catch(error=>{
                    this.$message({type: 'success',message: '删除失败!'});                         
                });
            }).catch(() => {
                this.$message({type: 'info',message: '取消删除'});          
            });
        },
    //账户管理页面大小修改方法
        handleSizeChange(val) {
            this.queryconditions.pageSize=val;            
            this.search();
        },
    //账户管理页码跳转方法
        handleCurrentChange(val) {
            this.queryconditions.pageIndex=val;
            this.search();
        },
    //新增
        openAddDialog(){
            this.dialogTitle="添加黑白名单";
            this.blacklist ={};
            this.dialogFormVisible = true;
        },
    //修改
        openaUpdateDialog(row){
            this.dialogTitle="修改黑白名单信息";
            this.blacklist.id=row.id;
            this.blacklist.cardNo=row.cardNo;
            this.blacklist.beginValidDate=row.beginValidDate;
            this.blacklist.endValidDate=row.endValidDate;
            this.blacklist.type=row.type;
            this.blacklist.remark=row.remark;
            this.dialogFormVisible = true;        
        },
    //提交弹窗信息
        submitBlk(){
            if(this.blacklist.cardNo == undefined){
                this.$message.error('卡号不能为空！！！'); 
            }
            else if(this.blacklist.beginValidDate == undefined){
                this.$message.error('开始时间！！！'); 
            }
            else if(this.blacklist.endValidDate == undefined){
                this.$message.error('结束时间！！！'); 
            }
            else if(this.blacklist.type == undefined){
                this.$message.error('请选择名单类型！！！'); 
            }
            else if(new Date(this.blacklist.beginValidDate) >= new Date(this.blacklist.endValidDate)){
                this.$message.error('结束时间必须大于开始时间！！！'); 
            }
            else if(this.dialogTitle==="添加黑白名单"){
                blacklistApi.AddBlacklist(this.blacklist).then(resp=>{
                    this.$message({type: 'success',message: '创建成功!'});                    
                    this.search();
                    this.dialogFormVisible = false;
                }).catch(error=>{
                    // this.$message({type: 'error',message: '创建失败!'});
                });
            }
            else{
                blacklistApi.UpdateBlacklist(this.blacklist).then(resp=>{
                    this.$message({type: 'success',message: '修改成功!'});
                    this.search();
                    this.dialogFormVisible = false;
                }).catch(error=>{
                    // this.$message({type: 'error',message: '修改失败!'});
                });
            }
        },
    //刷新查询方法
        searchClick(){
            this.search();
        },
    //获取黑白名单下拉框
        getBlacklistCode(){
            dataItemApi.getDetailByCode("BlacklistType").then(resp=>{
                this.blackOrWhiteListOptions = resp;
            }).catch(error=>{
            });
        },

  }
}
</script>
<style rel="stylesheet/scss" lang="scss">
    .input_width{
    width:220px;
    }
</style>