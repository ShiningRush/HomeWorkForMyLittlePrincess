<template>
<div style="margin-left:10px">
 <el-form :inline="true" :model="listQuery">
  <el-form-item label="卡类型">
     <el-select v-model="listQuery.cardType" placeholder="卡类型"  filterable clearable>
             <el-option v-for="v in cardTypes" :key="v.value" :label="v.value" :value="v.code">               
             </el-option>
      </el-select>
  </el-form-item>
  <el-form-item label="卡号">
    <el-input v-model="listQuery.cardNo" placeholder="卡号"></el-input>
  </el-form-item>
  <el-form-item label="姓名">
    <el-input v-model="listQuery.name" placeholder="姓名"></el-input>
  </el-form-item>
  <el-form-item label="证件号">
    <el-input v-model="listQuery.idCardNo" placeholder="证件号"></el-input>
  </el-form-item>
   <el-form-item label="操作员">
       <el-select v-model="listQuery.creatorId"  filterable placeholder="操作员" clearable>
         <el-option
            v-for="item in users"
            :key="item.id"
            :label="item.userName"
            :value="item.id">
         </el-option>
      </el-select>
  </el-form-item>
  <el-form-item>
    <el-button type="primary" @click="search" icon="el-icon-search" v-has="'Search'">查询</el-button>
  </el-form-item>
  <el-form-item>
  <el-button  @click="addAccount" icon="el-icon-plus" v-has="'Add'" >新增</el-button>
 </el-form-item>
</el-form> 
<el-table :data="list"  v-loading.body="listLoading" element-loading-text="拼命加载中" fit highlight-current-row  style="width: 100%" @current-change="handleAccountCurrentChange">     
        <el-table-column align="center" label='姓名'>
        <template slot-scope="scope">
        {{scope.row.name}}
        </template>
      </el-table-column>
      <el-table-column align="center" label='性别'>
        <template slot-scope="scope">
        {{scope.row.sexName}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='年龄'>
        <template slot-scope="scope">
        {{scope.row.ageDisplay}}
        </template>
      </el-table-column>      
      <el-table-column align="center" label='证件类型'>
        <template slot-scope="scope">
        {{scope.row.idCardTypeName}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='证件号'>
        <template slot-scope="scope">
        {{scope.row.idCardNo}}
        </template>
      </el-table-column>
       <el-table-column align="center" label='手机号'>
        <template slot-scope="scope">
        {{scope.row.mobile}}
        </template>
      </el-table-column>
        <el-table-column align="center" label='状态'>
        <template slot-scope="scope">
        {{scope.row.statusName}}
        </template>
      </el-table-column>
       <el-table-column align="center" label='操作员'>
        <template slot-scope="scope">
        {{scope.row.creatorName}}
        </template>
      </el-table-column>
        <el-table-column label="操作" align="center" width="300px">
        <div slot-scope="scope">
        <el-button  size="mini"  @click="preview(scope.row)" v-has="'Search'" >详情</el-button>
        <el-button  size="mini"  @click="updateInfo(scope.row)" v-has="'Edit'" >编辑</el-button>       
        </div>
      </el-table-column>                  
  </el-table>
    <div v-show="!listLoading" class="block pagination" style="text-align: center;padding-bottom:20px;">
      <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page.sync="listQuery.pageIndex" :page-sizes="[10,20,30,50]" :page-size="listQuery.pageSize" layout="total, sizes, prev, pager, next, jumper" :total="total">
      </el-pagination>
    </div>  
 <el-dialog :visible.sync="accountDialogFormVisible" :title="'帐户信息管理'" width="80%" center >
  <el-container>
   <el-main>
    <el-form :model="userInfoForm" :rules="rules" ref="userInfoForm" :label-width="formLabelWidth" >
    <el-row>
      <el-col :span="8"> 
        <el-form-item label="帐户类型"  prop="accountType" >
        <el-select v-model="userInfoForm.accountType"  filterable placeholder="账户类型" clearable>
         <el-option
            v-for="item in accountTypes"
            :key="item.value"
            :label="item.value"
            :value="item.code">
         </el-option>
      </el-select>
     </el-form-item>
   </el-col>

    <el-col :span="6"> 
    <el-form-item label="姓名"  prop="name">
        <el-input v-model="userInfoForm.name"  auto-complete="off"  placeholder="姓名" ></el-input>
    </el-form-item>
  </el-col>

  <el-col :span="4"> 
    <el-form-item label="性别"  prop="sex">
      <el-select v-model="userInfoForm.sex" filterable placeholder="" clearable>
         <el-option
            v-for="item in sexs"
            :key="item.value"
            :label="item.value"
            :value="item.code">
         </el-option>
      </el-select>
    </el-form-item>
  </el-col>

  <el-col :span="6"> <el-form-item label="年龄" prop="age">
    <el-col :span="8"><el-input v-model="userInfoForm.age" auto-complete="off"></el-input></el-col>
    <el-col :span="12"><el-select v-model="userInfoForm.ageType" filterable clearable placeholder="" style="margin-left:10px;">
         <el-option
            v-for="item in ageTypes"
            :key="item.value"
            :label="item.label"
            :value="item.value">
         </el-option>
      </el-select></el-col>
      </el-form-item>   
  </el-col>
  </el-row> 
  <el-row>
    <el-col :span="8">
      <el-form-item label="证件类型" prop="idCardType">  
        <el-col :span="16"><el-select v-model="userInfoForm.idCardType" filterable placeholder="证件类型" clearable>
         <el-option
            v-for="item in idCardTypes"
            :key="item.value"
            :label="item.value"
            :value="item.code">
         </el-option>
      </el-select>
      </el-col>
       <el-col :span="4">
        <el-button type="primary" size="small" @click="readIDCard">读证件</el-button> 
       </el-col>        
    </el-form-item>  
  </el-col>
     
  <el-col :span="6"> <el-form-item label="证件号"  prop="idCardNo">
        <el-input v-model="userInfoForm.idCardNo" auto-complete="off"  placeholder="证件号" @change="setBirthday"></el-input>
    </el-form-item></el-col> 
     <el-col :span="8"> <el-form-item label="出生日期" prop="birthDay">
        <el-date-picker v-model="userInfoForm.birthDay"  type="date" placeholder="出生日期" value-format="yyyy-MM-dd"  :picker-options="pickerOptions" @change ="setAge('-')" ></el-date-picker>
    </el-form-item></el-col>
  </el-row> 

  <el-row>   
    <el-col :span="8"> 
      <el-form-item label="手机号码"  prop="mobile">
        <el-input v-model="userInfoForm.mobile" auto-complete="off"  placeholder="手机号码"></el-input>
    </el-form-item>
  </el-col>
  <el-col :span="14"> <el-form-item label="现住地址"  prop="address">
       <el-input v-model="userInfoForm.address" auto-complete="off"  placeholder="现住地址" ></el-input>
    </el-form-item></el-col>
  </el-row>

   <el-row>
    <el-col :span="8"> <el-form-item label="联系人" prop="linkman">
       <el-input v-model="userInfoForm.linkman" auto-complete="off"  placeholder="联系人"></el-input>
    </el-form-item></el-col>
    <el-col :span="6"> 
      <el-form-item label="关系" prop="linkmanRelation" >       
         <el-select v-model="userInfoForm.linkmanRelation" filterable placeholder="" clearable>
         <el-option
            v-for="item in relationTypes"
            :key="item.value"
            :label="item.value"
            :value="item.code">
         </el-option>
      </el-select>
    </el-form-item>
  </el-col>
  <el-col :span="8"> 
      <el-form-item label="联系电话" prop="linkmanTel">
        <el-input v-model="userInfoForm.linkmanTel" auto-complete="off"  placeholder="联系电话"></el-input>
    </el-form-item>
  </el-col>
  </el-row>
  </el-form>
    </el-main>
    <el-aside width="160px" ><div class="user_Picture">
      <img :src="userInfoForm.avatar"/>
      </div></el-aside>
</el-container>
<el-container>
  <el-tabs type="border-card" class="el-tabs">
  <el-tab-pane label="绑卡管理">
    <div style="width:100%">
   <el-form  :model="cardForm"  :label-width="formLabelWidth"   ref="cardForm">
    <el-row  v-for ="(item, index) in cardForm.cardItems"   :key ="item.id" >
     <el-col :span="4"  >
        <el-form-item ><el-button  icon="el-icon-minus" size="small" @click="remove(item)" :disabled="isPreview || item.isInitial" ></el-button></el-form-item>    
    </el-col> 
     <el-col :span="6">
         <el-form-item  label="卡类型" :prop="'cardItems.' + index + '.cardType'"  :rules="{ required: true, message: '卡类型不能为空', trigger: 'blur,change' }">
           <el-col :span="16">
             <el-select v-model="item.cardType" placeholder="卡类型" :disabled="isPreview || item.isInitial" filterable clearable>
             <el-option v-for="v in cardTypes" :key="v.value" :label="v.value" :value="v.code">               
             </el-option>
            </el-select>
            </el-col>
            <el-col :span="4" style="margin-left:10px;" >
            <el-button type="primary" size="small" @click="readCard(item)" :disabled="isPreview || item.isInitial">读卡</el-button>
            </el-col>          
          </el-form-item>        
    </el-col> 
     
     <el-col :span="8" >
        <el-form-item  label="卡号"  :prop="'cardItems.' + index + '.cardNo'"  :rules="{ required: true, message: '卡号不能为空', trigger: 'blur,change' }">
           <el-input v-model="item.cardNo" auto-complete="off"  placeholder="卡号" :disabled="isPreview || item.isInitial" ></el-input>
        </el-form-item>
        
    </el-col> 
       <el-col :span="6">
          <el-form-item label="密码验证" prop="isPasswordAuth" >
           <el-col :span="12" >
           <el-switch
              v-model="item.isPasswordAuth"
              active-color="#13ce66"
              inactive-color="#ff4949" :disabled="isPreview">
              </el-switch>
         </el-col>
            <el-col :span="12" v-show="isUpdate">
            {{item.statusName}}
     </el-col>
            </el-form-item>     
    </el-col>
     
   </el-row>
  <el-row>
  <el-col :span="4" v-show="isUpdate || isAdd">
    <el-form-item > <el-button  icon="el-icon-plus" size="small" @click="addCard"></el-button></el-form-item>    
  </el-col>   
  </el-row>
   </el-form>
  </div>
  </el-tab-pane>
  <el-tab-pane label="综合信息">
  <div style="width:100%">
     <el-form :model="userInfoForm"  ref="otherInfoForm"  :rules="rules" :label-width="formLabelWidth" >
     <el-row>
        <el-col :span="8"> <el-form-item label="国籍"  prop="nationality">
            <el-select v-model="userInfoForm.nationality" filterable placeholder=""  clearable>
            <el-option
                v-for="item in nationalities"
                :key="item.value"
                :label="item.value"
                :value="item.code">
            </el-option>
          </el-select>
        </el-form-item></el-col>
        <el-col :span="4"> 
          <el-form-item label="民族" prop="nation">       
            <el-select v-model="userInfoForm.nation" filterable placeholder="" clearable >
             <el-option
                v-for="item in nations"
                :key="item.value"
                :label="item.value"
                :value="item.code">
            </el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="4"> 
          <el-form-item label="婚姻状况" prop="maritalStatus">
            <el-select v-model="userInfoForm.maritalStatus" filterable placeholder="" clearable>
           <el-option
                v-for="item in maritalStatus"
                :key="item.value"
                :label="item.value"
                :value="item.code">
            </el-option>
          </el-select>
        </el-form-item>
      </el-col>
        <el-col :span="8"> 
          <el-form-item label="籍贯" prop="nativePlace">
            <el-input v-model="userInfoForm.nativePlace" auto-complete="off"  placeholder=""></el-input>
        </el-form-item>
      </el-col>
    </el-row><el-row>
        <el-col :span="8"> <el-form-item label="职业"  prop="occupation">
            <el-input v-model="userInfoForm.occupation" auto-complete="off"  placeholder=""></el-input>          
        </el-form-item></el-col>
        <el-col :span="4"> 
          <el-form-item label="血型"  prop="bloodType">       
            <el-select v-model="userInfoForm.bloodType" filterable placeholder="" clearable>
           <el-option
                v-for="item in bloodTypes"
                :key="item.value"
                :label="item.value"
                :value="item.code">
            </el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="4"> 
          <el-form-item label="学历" prop="education">
            <el-select v-model="userInfoForm.education" filterable placeholder="" clearable>
            <el-option
                v-for="item in educations"
                :key="item.value"
                :label="item.value"
                :value="item.code">
            </el-option>
          </el-select>
        </el-form-item>
      </el-col>
        <el-col :span="8"> 
          <el-form-item label="联系人地址" prop="linkmanAddress">
            <el-input v-model="userInfoForm.linkmanAddress" auto-complete="off"  placeholder=""></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
        <el-col :span="8"> <el-form-item label="家庭住址"  prop="homeAddress">
          <el-input v-model="userInfoForm.homeAddress" auto-complete="off"  placeholder="家庭住址" ></el-input>       
        </el-form-item></el-col>
        <el-col :span="8"> 
          <el-form-item label="电子邮箱" prop="email">  
            <el-input v-model="userInfoForm.email" auto-complete="off"  placeholder="电子邮箱" ></el-input>                    
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
        <el-col :span="8"> <el-form-item label="单位名称" prop="companyName">
          <el-input v-model="userInfoForm.companyName" auto-complete="off"  placeholder="单位名称"  ></el-input>       
        </el-form-item></el-col>   
      <el-col :span="8"> 
          <el-form-item label="单位电话" prop="companyTel" >  
            <el-input v-model="userInfoForm.companyTel" auto-complete="off"  placeholder="单位电话" ></el-input>                    
        </el-form-item>
      </el-col>
      <el-col :span="8"> 
          <el-form-item label="单位地址" prop="companyAddress" >  
            <el-input v-model="userInfoForm.companyAddress" auto-complete="off"  placeholder="单位地址"></el-input>                    
        </el-form-item>
      </el-col>
    </el-row>
     </el-form>
  </div>
  </el-tab-pane>
</el-tabs>
</el-container>
<div slot="footer" class="dialog-footer" v-show="!isPreview">  
    <el-row>
      <el-col :span="3" v-show="!isAdd"><span style="line-height:30px">录档人员：{{userInfoForm.creatorName}}</span></el-col>
      <el-col :span="4" v-show="!isAdd"><span style="line-height:30px">录档时间：{{userInfoForm.createTime}}</span></el-col>
      <el-col :span="2" v-show="isUpdate"><el-button @click="freeze" v-has="'Freeze'" >{{userInfoForm.status == 0?"冻结":"解冻"}}</el-button></el-col>
      <el-col :span="12" style="margin-left:600px;"><el-button  @click="saveAccountAndContinue(true)" type="primary"  v-show="isUpdate">保存并继续</el-button>
    <el-button @click="saveAccountAndContinue(false)" type="primary">保存</el-button> 
    <el-button @click="accountDialogFormVisible=false">取消</el-button>   </el-col>
    </el-row>   
  </div>
</el-dialog>
</div>
</template>

<script>
import img_User from '@/assets/user.png'
import  * as ageUtils  from '@/utils/age'
import  { NewGuid }  from '@/utils/guid'
import * as dataItemApi from  '@/api/dataItem'
import cardInfo  from '@/json/ReadCardInfo.json'
import certificateInfo  from '@/json/ReadCertificateInfo.json'
import * as accountApi from  '@/api/account'
import * as userApi from  '@/api/user'
import * as dateTimeApi from "@/utils/index"

export default {
    created(){
         this.getList();
         this.getAllUsers();
         this.getAccountType();
         this.getIdCardType();
         this.getCardType();
         this.getSex();
         this.getRelationType();
         this.getNationality();
         this.getNation();
         this.getMaritalStatus();       
         this.getBloodType();
         this.getEducation();
        },
    data() {
      var validateIdCardNo = (rule, value, callback) => {
       
          if(this.userInfoForm.idCardType && this.userInfoForm.idCardType == "ID" ){
              if(value && value != ""){
                    var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
                    if(!reg.test(value)){
                        callback(new Error('请输入正确的身份证号码'));
                    }
              }
            }
             callback();
      };
     
      return {       
        isAdd:true, 
        isUpdate:false,  
        isPreview:false,       
        list: [],
        total: null,
        listLoading: false,     
        listQuery:{
            pageIndex:1,
            pageSize:10,
            cardNo:"",         
            name:"",
            idCardNo:"",
            creatorId:"",
            cardType:""
        },
        cardForm:{
           cardItems:[],
        },
        accountDialogFormVisible:false,   
        //账户表单模型      
        userInfoForm:{          
          avatar:"",
          accountType:"",
          name:"",
          sex:"",  
          age:"",   
          ageType:"",
          ageDisplay:"",    
          idCardType:"",
          idCardNo:"",
          birthDay:"",
          mobile:"",
          address:"",
          linkman:"",
          linkmanRelation:"",
          linkmanTel:"",  
          status:"", 
          nationality:"",
          nation:"",
          maritalStatus:"",
          nativePlace:"",
          occupation:"",
          bloodType:"",
          education:"",
          linkmanAddress:"",
          homeAddress:"",
          email:"",
          companyName:"",
          companyAddress:"",
          companyTel:"",       
          createCards:[],
          cards:[],
          updateCards:[],
          creatorName:"",
          createTime:"",
        },
        //校验规则
        rules:{
            //帐户
            accountType:[{ 
            required: true, message: '请选择帐户类型', trigger: 'blur' 
            }],
            //姓名
            name:[
              { required: true, message: '请输入姓名', trigger: 'blur' },
            ],
            //性别
            sex:[{ 
            required: true, message: '请选择性别', trigger: 'blur' 
            }],
            //手机号码
            mobile:[
              { required: true, message: '请输入手机号码', trigger: 'blur' },
              { pattern: /^(((1[3456789][0-9]{1})|(15[0-9]{1}))+\d{8})$/, message: '请输入正确的手机号码', trigger: 'blur' }
            ],
            //联系人号码
            linkmanTel:[{ pattern: /^(((1[3456789][0-9]{1})|(15[0-9]{1}))+\d{8})$/, message: '请输入正确的联系电话', trigger: 'blur' 
            }],
            //年龄
            age:[{ pattern: /^[1-9]\d*|0$/, message: '年龄必须为数字值', trigger: 'blur'}],

            //出生日期
            birthDay:[{ 
            required: true, message: '请输入出生日期', trigger: 'blur' 
            }], 

            //邮箱
            email:[{ 
             type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' 
            }],
            //现住地址
            address:[{
              pattern:/^[A-Za-z0-9\u4e00-\u9fa5]+$/,message: '请输入正确的地址', trigger: 'blur' 
            }],
            //联系人地址
            linkmanAddress:[{
              pattern:/^[A-Za-z0-9\u4e00-\u9fa5]+$/,message: '请输入正确的地址', trigger: 'blur' 
            }],  
             //家庭地址
            homeAddress:[{
              pattern:/^[A-Za-z0-9\u4e00-\u9fa5]+$/,message: '请输入正确的地址', trigger: 'blur' 
            }],
            //公司地址
            companyAddress:[{
              pattern:/^[A-Za-z0-9\u4e00-\u9fa5]+$/,message: '请输入正确的地址', trigger: 'blur' 
            }],
            //单位电话   
            companyTel:[{
                pattern:/^[A-Za-z0-9\u4e00-\u9fa5]+$/,message: '请输入正确的单位电话', trigger: 'blur' 
            }],
            //证件号   
            idCardNo:[{
                validator:validateIdCardNo,trigger: 'blur' 
            }]
        },       
        formLabelWidth:"100px",         
        ageTypes:[{
          value: 'Y',
          label: '岁'
        },{
          value: 'M',
          label: '月'
        },{
          value: 'D',
          label: '天'
        }],
        initialCard:{
            id:"",
            cardNo:"",
            cardType:null,
            isPasswordAuth:false,
            status:"",            
        },      
       pickerOptions: {
         disabledDate(time) {
         return time.getTime() > Date.now() 
       }      
     },      
     users:[],
     accountTypes:[],
     idCardTypes:[],
     cardTypes:[],
     relationTypes:[],   
     nationalities:[],
     nations:[],
     maritalStatus:[],
     bloodTypes:[],
     educations:[],
     sexs:[],
     accountCurrentRow:undefined,
     cardValidate:false,
     accountValidate:false,
    }
  },
  methods: {
      search(){
         this.getList();
      },
      //获取账户类型下拉列表
      getAccountType(){
         dataItemApi.getDetailByCode("AccountType").then(resp=>{
         this.accountTypes = resp;
       }).catch(error=>{
          console.debug(error)
       })
      },
      //获取证件类型下拉列表
      getIdCardType(){
           dataItemApi.getDetailByCode("IdCardType").then(resp=>{
            this.idCardTypes = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
      //获取卡类型下拉列表
       getCardType(){
           dataItemApi.getDetailByCode("CardType").then(resp=>{
            this.cardTypes = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
      //获取性别下拉列表
      getSex(){
           dataItemApi.getDetailByCode("Sex").then(resp=>{
            this.sexs = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
      //获取关系下拉列表
      getRelationType(){
            dataItemApi.getDetailByCode("Relation").then(resp=>{
            this.relationTypes = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
      //获取国籍下拉列表
        getNationality(){
            dataItemApi.getDetailByCode("Nationality").then(resp=>{
            this.nationalities = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
        //获取民族下拉列表
        getNation(){
            dataItemApi.getDetailByCode("Nation").then(resp=>{
            this.nations = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
       //获取婚姻下拉列表
        getMaritalStatus(){
            dataItemApi.getDetailByCode("MaritalStatus").then(resp=>{
            this.maritalStatus = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
      //获取血型下拉列表
        getBloodType(){
            dataItemApi.getDetailByCode("BloodType").then(resp=>{
            this.bloodTypes = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },
        //获取学历下拉列表
        getEducation(){
            dataItemApi.getDetailByCode("Education").then(resp=>{
            this.educations = resp;
          }).catch(error=>{
              console.debug(error)
          })
      },     
      handleAccountCurrentChange(val){
         this.accountCurrentRow = val;
      },
      addAccount(){         
         this.accountDialogFormVisible = true;       
         this.isUpdate = false;
         this.isAdd = true;
         this.isPreview = false;
         let id = NewGuid();
         this.initialCard.id = id;
         this.initialCard.cardType = "";
         this.initialCard.cardNo = "";
         this.initialCard.isPasswordAuth = false;
         if(this.$refs.userInfoForm){
          this.$refs.userInfoForm.resetFields();
        }
         if(this.$refs.otherInfoForm){
           this.$refs.otherInfoForm.resetFields();
         }         
         Object.assign(this.$data.userInfoForm, this.$options.data.call(this).userInfoForm);
         this.userInfoForm.avatar = img_User;
         this.cardForm.cardItems = [];
         this.cardForm.cardItems.push(this.initialCard);
      },
      setAge(operation){                 
              let age = ageUtils.getAge(this.userInfoForm.birthDay,operation);
              let ageArray = age.split('+');
              this.userInfoForm.age = ageArray[0]; 
              this.userInfoForm.ageType = ageArray[1]; 
      },
      setBirthday(){
          if(this.userInfoForm.idCardType && this.userInfoForm.idCardType == "ID" ){
             let idCardNo = this.userInfoForm.idCardNo;
             let  birthday;            
              if(idCardNo && idCardNo != ""){
                  if(idCardNo.length == 15){  
                  birthday = "19"+idCardNo.substr(6,6);  
            } else if(idCardNo.length == 18){  
                  birthday = idCardNo.substr(6,8);  
            }           
              birthday = birthday.replace(/(.{4})(.{2})/,"$1-$2-");  
              this.userInfoForm.birthDay = birthday;
              this.setAge("-");
              }
            }     
      },
      freeze(){
        let accountId = this.accountCurrentRow.id;
        var request = {
                    accountId: accountId
                  }
          
         if(this.userInfoForm.statusName == "冻结"){
          this.$confirm("此操作将冻结卡用户恢复正常, 是否继续?", "提示", {
              confirmButtonText: "确定",
              cancelButtonText: "取消",
              type: "warning"
        }).then(()=>{ 
                 accountApi.thawAccount(request).then((response)=>{                                  
                  this.getList();    
                  this.accountDialogFormVisible = false;         
                  this.$message({
                  type: 'success',
                  message: '解冻成功!'
                });                                          
             }).catch(error =>{
                 console.log(error)
             })          
        })
         }else{
              this.$confirm("此操作将冻结卡用户所有使用权限，各子系统应用层提示用户冻结状态, 是否继续?", "提示", {
              confirmButtonText: "确定",
              cancelButtonText: "取消",
              type: "warning"
             }).then(()=>{
                  
                  accountApi.freezeAccount(request).then((response)=>{                                  
                  this.getList();    
                  this.accountDialogFormVisible = false;           
                  this.$message({
                    type: 'success',
                    message: '冻结成功!'
                });                                          
             }).catch(error =>{
                 console.log(error)
             })     
             });
         }
        
      },
      saveAccountAndContinue(isContinue){                                
          if (this.validate()) {
                  let request = this.userInfoForm;
                  if(this.userInfoForm.avatar && this.userInfoForm.avatar != ""){
                    this.userInfoForm.avatar = this.userInfoForm.avatar.replace("data:image/png;base64,","")
                  }
                  if(this.isAdd){
                       this.userInfoForm.createCards = this.cardForm.cardItems;  
                       accountApi.createAccount(request).then((response)=>{                                  
                      this.getList();               
                      this.$message({
                        type: 'success',
                        message: '新增账户信息成功!'
                    });  
                    if(!isContinue){
                       this.accountDialogFormVisible = false;
                    }                                        
                }).catch(error =>{
                    console.log(error)
                })     

               }else{
                      this.userInfoForm.updateCards = this.cardForm.cardItems;                     
                      accountApi.updateAccount(request).then((response)=>{                                  
                      this.getList();               
                      this.$message({
                        type: 'success',
                        message: '保存账户信息成功!'
                    }); 
                     if(!isContinue){
                       this.accountDialogFormVisible = false;
                    }                                                 
                }).catch(error =>{
                    console.log(error)
                })      
               }                 
                                          
              }else{
                    this.$message({
                    type: 'error',
                    message: '数据校验出错!'
                });             
              }                                                         
       },
       validate(){       
            this.$refs.userInfoForm.validate((valid) => {
                if(valid){
                    this.accountValidate = true;
                }else{
                     this.accountValidate = false;
                }
            });
                 
             this.$refs.cardForm.validate((valid) => {
              if(valid){
                this.cardValidate = true;
              }else{
                 this.cardValidate = false;
              }
            })                          
             if( this.cardValidate && this.accountValidate){
               return true;
             }
             return false;
       },
       preview(data){            
            this.isUpdate = false;
            this.isAdd = false;
            this.isPreview = true;
            this.accountDialogFormVisible = true;
            this.bindData(data);
       },
      updateInfo(data){
         this.accountDialogFormVisible = true;
          if(this.$refs.userInfoForm){
          this.$refs.userInfoForm.resetFields();
        }
         if(this.$refs.otherInfoForm){
           this.$refs.otherInfoForm.resetFields();
         }         
         this.isUpdate = true;
         this.isAdd = false;
         this.isPreview = false;
         this.bindData(data);
      },  
      bindData(data){
         this.userInfoForm = Object.assign(this.userInfoForm, data);
         this.cardForm.cardItems = [];
         data.cards.forEach(p=>{
              let item = p;
              item.isInitial = true;
              this.cardForm.cardItems.push(item);
          })
         if(this.userInfoForm.avatar &&  this.userInfoForm.avatar != ""){
             this.userInfoForm.avatar = "data:image/png;base64," + data.avatar;
         }else{
           this.userInfoForm.avatar = img_User;
         }   
         this.userInfoForm.birthDay =  new Date(data.birthDay).toLocaleDateString();                            
         this.setAge("/");
         this.userInfoForm.createTime = dateTimeApi.parseTime(data.createTime);
                       
      }, 
      handleSizeChange(val) {
      this.listQuery.pageSize = val;
      this.getList();
     },
     handleCurrentChange(val) {
      this.listQuery.pageIndex = val;
      this.getList();
    },
     getList(){        
       this.listLoading = true;
       let request = this.listQuery;
       //校验卡号和卡类型
       if((request.cardType != "" && request.cardNo =="") || (request.cardType == "" && request.cardNo !="")){
                   this.$message({
                    type: 'error',
                    message: '请填写卡类型和卡号!'
                });  
                this.listLoading = false;
                return false;       
           }
       this.list = [];
       accountApi.getAccounts(request).then((response)=>{    
                  response.result.forEach(p=>{
                        p.ageDisplay = ageUtils.displayAge(p.birthDay);
                        this.list.push(p);                        
                       })                                  
                       this.total = response.totalCount;  
                       this.listLoading = false;                              
                      }).catch(error =>{
                         this.listLoading = false;    
                          console.log(error)  
                      })     
                      
      },     
      addCard(){
        let id = NewGuid();
        let card = {
            id:id,
            cardNo:"",
            cardType:null,  
                  
        }
        this.cardForm.cardItems.push(card);
      },
    
      remove(item){
         this.cardForm.cardItems = this.cardForm.cardItems.filter(p=>p.id != item.id);
      },
      readIDCard(){
          let isReturn = this.checkBrowser();
          if(isReturn){
            //读身份证信息
            let idcardType = this.userInfoForm.idCardType;
            if(!idcardType || this.userInfoForm.idCardType == ""){
               this.$message({
                    type: 'error',
                    message: '请选择证件类型!'
                });  
                return false;           
            }
            let identityInfo = JSON.parse(ReadCertificateInfo(idcardType));
            //let identityInfo = certificateInfo;
            if(identityInfo.Code == 0){
                //绑定基本信息
               this.userInfoForm.name = identityInfo.Data.Name;
               this.userInfoForm.idCardNo = identityInfo.Data.IDCardNo;
               this.userInfoForm.sex = identityInfo.Data.Sex;
               this.userInfoForm.address = identityInfo.Data.Address;
               let birthday = new Date(identityInfo.Data.BirthDay).toLocaleDateString();            
               this.userInfoForm.birthDay = birthday;
               this.userInfoForm.avatar = "data:image/png;base64," + identityInfo.Data.Avatar;
               this.setAge("/");


            }else{
               this.$message({
                    type: 'error',
                    message: data.message
                });                  
              return false;
            }
          }else{
            return false;
          }
        
      },
      readCard(data){
            let isReturn = this.checkBrowser();
            if(isReturn){
            //读身份证信息
             let cardType = data.cardType;
            if(!cardType || this.userInfoForm.cardType == ""){
               this.$message({
                    type: 'error',
                    message: '请选择卡类型!'
                });  
                return false;           
            }
            let cardInfoData = JSON.parse(ReadCardInfo(cardType));
            //let cardInfoData = cardInfo;
            if(cardInfoData.Code == 0){
                //绑定卡号            
              let card = this.cardForm.cardItems.filter(p=>p.id == data.id)[0];                                          
              card.cardNo = cardInfoData.Data;
            }else{
               this.$message({
                    type: 'error',
                    message: data.message
                });                  
              return false;
            }
          }else{
            return false;
          }

      },
     
      checkBrowser(){          
           let requestUrl = process.env.BASE_API + '/ClientPack/GetClientPackFile?clientIp=' + location.protocol + "//" + location.host;
           let userAgent = navigator.userAgent;
           if (userAgent.indexOf("ClearWebBrowser") == -1){
               this.$message({
                    type: 'warn',
                    dangerouslyUseHTMLString: true,
                    message: '<span style="margin-right:20px">未检测到一卡通客户端程序,点击<a href="'+ requestUrl +'" style="color:red;text-decoration:underline;">下载最新客户端程序</a></span>',
                    duration:0,
                    showClose: true,
               
                });                  
              return false;
          }else{
            return true;
          }
      },
       getAllUsers(request) {
                var request = {

                }
                userApi.getSimpleUsers(request).then((response) => {
                    this.users = response;
                }).catch(error => {
                    this.$message({
                        type: 'error',
                        message: '获取操作员失败!'
                    });
                });
            },
   }

  }
</script>
<style rel="stylesheet/scss" lang="scss" scoped>
  .input_width{
   width:150px;
 }

 .user_Picture{
  margin-top:10px;
  padding-top: 40px;
  padding-left: 30px;
  border:1px solid;
  height:200px;
  width:160px;
 } 
 
.el-dialog__wrapper /deep/ .el-dialog__header  {
    padding:0px;
    background-color: #394eff;
    height:40px;
    text-align: center;
   
    
 }
 .el-dialog__wrapper /deep/ .el-dialog__title{      
   color:white;
   line-height: 40px;
 }

 .download_link{
   color:red;
   text-decoration:underline
 }
   body {
    margin: 0;
  }
  .el-tabs{
    margin-left: 2%;
  }
 
</style>


