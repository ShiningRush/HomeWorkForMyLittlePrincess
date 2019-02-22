-- 初始化模块表系统菜单基础数据，
-- 保留部分字段，有存在级联删除外键关系 不能使用 replace  into语法
INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`)
VALUES ('11e30e8f-3a5c-46af-a5b4-a66914305068',NULL,'SystemManagement','系统管理',NULL,NULL,NULL,1,1,0,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 10:58:31',NULL,'2017-11-22 10:58:31') 
ON DUPLICATE KEY UPDATE  `Code`='SystemManagement' ,`UrlAddress`=NULL , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('12e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','OrganizeManagement','机构管理',NULL,'/systemManage/orgManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='OrganizeManagement' ,`UrlAddress`='/systemManage/orgManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`)
VALUES ('13e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','DepartmentManagement','部门管理',NULL,'/systemManage/deptManage','iframe',0,1,0,2,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:28:41',NULL,'2017-11-22 11:28:43')
ON DUPLICATE KEY UPDATE  `Code`='DepartmentManagement' ,`UrlAddress`='/systemManage/deptManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('14e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','UserManagement','用户管理',NULL,'/systemManage/userManage','iframe',0,1,0,3,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:30:22',NULL,'2017-11-22 11:30:25')
ON DUPLICATE KEY UPDATE  `Code`='UserManagement' ,`UrlAddress`='/systemManage/userManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('15e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','RoleManagement','角色管理',NULL,'/systemManage/rolePermsManage','iframe',0,1,0,4,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:30:22',NULL,'2017-11-22 11:30:25')
ON DUPLICATE KEY UPDATE  `Code`='RoleManagement' ,`UrlAddress`='/systemManage/rolePermsManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('16e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','MenuManagement','菜单管理',NULL,'/systemManage/menuManage','iframe',0,1,0,5,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:30:22',NULL,'2017-11-22 11:30:25')
ON DUPLICATE KEY UPDATE  `Code`='MenuManagement' ,`UrlAddress`='/systemManage/menuManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('17e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','DictionaryManagement','字典管理',NULL,'/systemManage/dicManage','iframe',0,1,0,6,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:30:22',NULL,'2017-11-22 11:30:25')
ON DUPLICATE KEY UPDATE  `Code`='DictionaryManagement' ,`UrlAddress`='/systemManage/dicManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('18e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','LoggerManagement','操作日志',NULL,'/systemManage/logManage','iframe',0,1,0,7,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:30:22',NULL,'2017-11-22 11:30:25')
ON DUPLICATE KEY UPDATE  `Code`='LoggerManagement' ,`UrlAddress`='/systemManage/logManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('19e30e8f-3a5c-46af-a5b4-a66914305068','11e30e8f-3a5c-46af-a5b4-a66914305068','AppManagement','接入应用',NULL,'/systemManage/appManage','iframe',0,1,0,8,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:30:22',NULL,'2017-11-22 11:30:25')
ON DUPLICATE KEY UPDATE  `Code`='AppManagement' ,`UrlAddress`='/systemManage/appManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;


INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`)
VALUES ('20e30e8f-3a5c-46af-a5b4-a66914305068',NULL,'cardUserManage','卡用户管理',NULL,NULL,NULL,1,1,0,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 10:58:31',NULL,'2017-11-22 10:58:31') 
ON DUPLICATE KEY UPDATE  `Code`='cardUserManage' ,`UrlAddress`=NULL , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('21e30e8f-3a5c-46af-a5b4-a66914305068','20e30e8f-3a5c-46af-a5b4-a66914305068','basicInfoManage','账户信息管理',NULL,'/cardUserManage/basicInfoManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='basicInfoManage' ,`UrlAddress`='/cardUserManage/basicInfoManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('22e30e8f-3a5c-46af-a5b4-a66914305068','20e30e8f-3a5c-46af-a5b4-a66914305068','cardStatusManage','卡状态管理',NULL,'/cardUserManage/cardStatusManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='cardStatusManage' ,`UrlAddress`='/cardUserManage/cardStatusManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('23e30e8f-3a5c-46af-a5b4-a66914305068','20e30e8f-3a5c-46af-a5b4-a66914305068','blackWhiteListManage','黑/白名单管理',NULL,'/cardUserManage/blackWhiteListManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='blackWhiteListManage' ,`UrlAddress`='/cardUserManage/blackWhiteListManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('24e30e8f-3a5c-46af-a5b4-a66914305068','20e30e8f-3a5c-46af-a5b4-a66914305068','fundingAccountManage','资金账户管理',NULL,'/cardUserManage/fundingAccountManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='fundingAccountManage' ,`UrlAddress`='/cardUserManage/fundingAccountManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;


INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`)
VALUES ('25e30e8f-3a5c-46af-a5b4-a66914305068',NULL,'businessInquiryManage','业务查询统计',NULL,NULL,NULL,1,1,0,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 10:58:31',NULL,'2017-11-22 10:58:31') 
ON DUPLICATE KEY UPDATE  `Code`='businessInquiryManage' ,`UrlAddress`=NULL , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;


INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('26e30e8f-3a5c-46af-a5b4-a66914305068','25e30e8f-3a5c-46af-a5b4-a66914305068','cardLogManage','卡务日志管理',NULL,'/businessInquiryManage/cardLogManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='cardLogManage' ,`UrlAddress`='/businessInquiryManage/cardLogManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('27e30e8f-3a5c-46af-a5b4-a66914305068','25e30e8f-3a5c-46af-a5b4-a66914305068','cardDetailsManage','用卡明细查询',NULL,'/businessInquiryManage/cardDetailsManage','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='cardDetailsManage' ,`UrlAddress`='/businessInquiryManage/cardDetailsManage' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`)
VALUES ('28e30e8f-3a5c-46af-a5b4-a66914305068',NULL,'dailySettleManage','日结管理',NULL,NULL,NULL,1,1,0,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 10:58:31',NULL,'2017-11-22 10:58:31') 
ON DUPLICATE KEY UPDATE  `Code`='dailySettleManage' ,`UrlAddress`=NULL , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;


INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('29e30e8f-3a5c-46af-a5b4-a66914305068','28e30e8f-3a5c-46af-a5b4-a66914305068','dailySettleReport','日结报表',NULL,'/dailySettleManage/dailySettleReport','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='dailySettleReport' ,`UrlAddress`='/dailySettleManage/dailySettleReport' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

INSERT  INTO `sys_module`(`Id`,`ParentId`,`Code`,`Name`,`Icon`,`UrlAddress`,`Target`,`AllowAutoExpand`,`AllowEdit`,`AllowDelete`,`SortCode`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`LastModifier`,`LastModifyTime`) 
VALUES ('30e30e8f-3a5c-46af-a5b4-a66914305068','28e30e8f-3a5c-46af-a5b4-a66914305068','dailySettleDetails','日结明细查询',NULL,'/dailySettleManage/dailySettleDetails','iframe',0,1,0,1,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 11:26:42',NULL,'2017-11-22 11:26:48')
ON DUPLICATE KEY UPDATE  `Code`='dailySettleDetails' ,`UrlAddress`='/dailySettleManage/dailySettleDetails' , `Target`='iframe' , `AllowEdit` =1, `AllowDelete` = 0,`IsEnabled`=1;

-- 初始化模块权限表基础数据，
-- 数据更新机制是存在全部替换 
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('10496b59-cf38-11e7-8379-00155d1b9f02','12e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'organize/GetOrganize;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('11496b59-cf38-11e7-8379-00155d1b9f02','12e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'organize/AddOrganize;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('12496b59-cf38-11e7-8379-00155d1b9f02','12e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改',3,'organize/UpdateOrganize;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('13496b59-cf38-11e7-8379-00155d1b9f02','12e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除',4,'organize/DeleteOrganize;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('20496b59-cf38-11e7-8379-00155d1b9f02','13e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'department/GetDepartment;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('21496b59-cf38-11e7-8379-00155d1b9f02','13e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'department/AddDepartment;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('22496b59-cf38-11e7-8379-00155d1b9f02','13e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改',3,'department/UpdateDepartment;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('23496b59-cf38-11e7-8379-00155d1b9f02','13e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除',4,'department/DeleteDepartment;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('30496b59-cf38-11e7-8379-00155d1b9f02','14e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'user/GetUsers;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('31496b59-cf38-11e7-8379-00155d1b9f02','14e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'user/CreateUser;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('32496b59-cf38-11e7-8379-00155d1b9f02','14e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改',3,'user/UpdateUser;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('33496b59-cf38-11e7-8379-00155d1b9f02','14e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除',4,'user/DeleteUser;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('34496b59-cf38-11e7-8379-00155d1b9f02','14e30e8f-3a5c-46af-a5b4-a66914305068','Reset','重置密码',5,'user/RestUserPassword;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('40496b59-cf38-11e7-8379-00155d1b9f02','15e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'role/GetRoles;role/GetRoleUser;role/GetRoleModuleAuth;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('41496b59-cf38-11e7-8379-00155d1b9f02','15e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'role/AddRole;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('42496b59-cf38-11e7-8379-00155d1b9f02','15e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改',3,'role/UpdateRole;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('43496b59-cf38-11e7-8379-00155d1b9f02','15e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除',4,'role/DeleteRole;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('44496b59-cf38-11e7-8379-00155d1b9f02','15e30e8f-3a5c-46af-a5b4-a66914305068','Auth','角色授权',5,'role/SaveRoleModuleAuth;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('45496b59-cf38-11e7-8379-00155d1b9f02','15e30e8f-3a5c-46af-a5b4-a66914305068','Member','角色成员',6,'role/SaveRoleUser;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('50496b59-cf38-11e7-8379-00155d1b9f02','16e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'module/GetAllModules;module/GetModuleAuths;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('51496b59-cf38-11e7-8379-00155d1b9f02','16e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'module/InsertModuleAuth;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('52496b59-cf38-11e7-8379-00155d1b9f02','16e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改',3,'module/UpdateModuleAuth;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('53496b59-cf38-11e7-8379-00155d1b9f02','16e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除',4,'module/DeleteModule;module/DeleteModuleAuth;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('60496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'dataItem/GetDataItem;dataItem/GetDataItemDetail;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('61496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增分类',2,'dataItem/AddDataItem;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('62496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改分类',3,'dataItem/UpdateDataItem;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('63496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除分类',4,'dataItem/DeleteDataItem;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('64496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','AddDetail','新增字典',5,'dataItem/AddDataItemDetail;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('65496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','EditDetail','修改字典',5,'dataItem/UpdateDataItemDetail;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('66496b59-cf38-11e7-8379-00155d1b9f02','17e30e8f-3a5c-46af-a5b4-a66914305068','DeleteDetail','删除字典',6,'dataItem/DeleteDataItemDetail;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('70496b59-cf38-11e7-8379-00155d1b9f02','18e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'oprLog/GetOprLogs;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('80496b59-cf38-11e7-8379-00155d1b9f02','19e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'authorization/GetAllApp;');
-- 初始化一卡通模块权限
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('81496b59-cf38-11e7-8379-00155d1b9f02','21e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'account/GetAccounts;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('82496b59-cf38-11e7-8379-00155d1b9f02','21e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'account/CreateAccount;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('83496b59-cf38-11e7-8379-00155d1b9f02','21e30e8f-3a5c-46af-a5b4-a66914305068','Edit','编辑',3,'account/UpdateAccount;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('84496b59-cf38-11e7-8379-00155d1b9f02','21e30e8f-3a5c-46af-a5b4-a66914305068','Freeze','冻结',3,'account/FreezeAccount;account/ThawAccount;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('85496b59-cf38-11e7-8379-00155d1b9f02','22e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'card/GetCards;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('86496b59-cf38-11e7-8379-00155d1b9f02','22e30e8f-3a5c-46af-a5b4-a66914305068','Loss','挂失',2,'card/ReleaseLoss;card/Loss;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('87496b59-cf38-11e7-8379-00155d1b9f02','22e30e8f-3a5c-46af-a5b4-a66914305068','ReplaceCard','补换卡',3,'card/ReplaceCard;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('88496b59-cf38-11e7-8379-00155d1b9f02','22e30e8f-3a5c-46af-a5b4-a66914305068','CancellationCard','退卡',4,'card/CancellationCard;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('001eeb59-2354-1253-8379-23155d1b9f03','23e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'blacklist/GetBlacklist;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('002eeb59-2354-1253-8379-23155d1b9f03','23e30e8f-3a5c-46af-a5b4-a66914305068','Add','新增',2,'blacklist/AddBlacklist;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('003eeb59-2354-1253-8379-23155d1b9f03','23e30e8f-3a5c-46af-a5b4-a66914305068','Edit','修改',3,'blacklist/UpdateBlacklist;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('004eeb59-2354-1253-8379-23155d1b9f03','23e30e8f-3a5c-46af-a5b4-a66914305068','Delete','删除',4,'blacklist/DeleteBlacklist;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('005eeb59-2354-1253-8379-23155d1b9f03','24e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'account/GetAccountByCard;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('006eeb59-2354-1253-8379-23155d1b9f03','24e30e8f-3a5c-46af-a5b4-a66914305068','Recharge','充值',2,'account/AccountRecharge;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('007eeb59-2354-1253-8379-23155d1b9f03','24e30e8f-3a5c-46af-a5b4-a66914305068','Refund','退费',3,'account/Refund;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('008eeb59-2354-1253-8379-23155d1b9f03','24e30e8f-3a5c-46af-a5b4-a66914305068','BillingList','资金明细',4,'billingRecord/GetBillingRecordList;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('009eeb59-2354-1253-8379-23155d1b9f03','26e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'cardLog/GetCardLogList;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('010eeb59-2354-1253-8379-23155d1b9f03','27e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'billingRecord/GetBillingRecordList;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('011eeb59-2354-1253-8379-23155d1b9f03','27e30e8f-3a5c-46af-a5b4-a66914305068','Export','导出',2,'billingRecord/DeriveBillingRecordList;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('012eeb59-2354-1253-8379-23155d1b9f03','29e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'settlement/GetNotSettledBillingGraphData;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('013eeb59-2354-1253-8379-23155d1b9f03','29e30e8f-3a5c-46af-a5b4-a66914305068','SettledBilling','结算',2,'settlement/SettleBillingRecord;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('014eeb59-2354-1253-8379-23155d1b9f03','29e30e8f-3a5c-46af-a5b4-a66914305068','Print','打印',3,'');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('015eeb59-2354-1253-8379-23155d1b9f03','30e30e8f-3a5c-46af-a5b4-a66914305068','Search','查询',1,'settlement/GetSettlementList;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('016eeb59-2354-1253-8379-23155d1b9f03','30e30e8f-3a5c-46af-a5b4-a66914305068','Detail','查看明细',2,'settlement/GetSingleSettlementDetail;');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('017eeb59-2354-1253-8379-23155d1b9f03','30e30e8f-3a5c-46af-a5b4-a66914305068','Print','打印',3,'');
REPLACE  INTO `sys_moduleauth`(`Id`,`ModuleId`,`Code`,`Name`,`SortCode`,`WebAPI`) VALUES ('018eeb59-2354-1253-8379-23155d1b9f03','30e30e8f-3a5c-46af-a5b4-a66914305068','Export','导出',4,'');

