
-- 初始化数据字典表基础数据，
-- 数据更新机制是先临时存储旧数据中可以修改的字段信息 其它字段全部重置

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1054bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'OrgNature','机构性质',0,'机构性质','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:16:04',1,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='OrgNature' ,`ItemName`='机构性质';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1154bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'ProfessionalLevel','用户职级',1,'用户职级','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:19:41',1,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='ProfessionalLevel' ,`ItemName`='用户职级';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1254bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'UserDuty','用户职务',2,'用户职务','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:20:44',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='UserDuty' ,`ItemName`='用户职务';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1354bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'Sex','用户性别',3,'用户性别','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='Sex' ,`ItemName`='用户性别';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1454bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'AccountType','账户类型',4,'账户类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='AccountType' ,`ItemName`='账户类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1554bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'IDCardType','证件类型',5,'证件类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='IDCardType' ,`ItemName`='证件类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1654bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'Nationality','国籍',6,'国籍','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='Nationality' ,`ItemName`='国籍';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1754bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'Nation','民族',7,'民族','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='Nation' ,`ItemName`='民族';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1854bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'MaritalStatus','婚姻状况',8,'婚姻状况','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='MaritalStatus' ,`ItemName`='婚姻状况';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('1954bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'BloodType','血型',9,'血型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='BloodType' ,`ItemName`='血型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2054bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'Education','学历',9,'学历','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='Education' ,`ItemName`='学历';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2154bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'CardType','卡类型',10,'卡类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='CardType' ,`ItemName`='卡类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2254bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'CapitalType','资金类型',11,'资金类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='CapitalType' ,`ItemName`='资金类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2354bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'PayType','支付方式',12,'支付方式','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='PayType' ,`ItemName`='支付方式';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2454bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'TerminalType','终端类型',13,'终端类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='TerminalType' ,`ItemName`='终端类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2554bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'BusinessType','业务类型',14,'业务类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='BusinessType' ,`ItemName`='业务类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2654bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'BlacklistType','黑白名单类型',15,'黑白名单类型','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='BlacklistType' ,`ItemName`='黑白名单类型';

INSERT  INTO `sys_dataitem`(`Id`,`ParentId`,`ItemCode`,`ItemName`,`SortCode`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) 
VALUES ('2754bfb4-cf6d-11e7-8379-00155d1b9f02',NULL,'Relation','联系人关系',16,'联系人关系','dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:21:39',0,0)
ON DUPLICATE KEY UPDATE  `ItemCode`='Relation' ,`ItemName`='联系人关系';




CREATE TEMPORARY TABLE temp_sys_dataitemdetail SELECT Id,SortCode,IsDefault,IsEnabled,IsDelete,Description FROM sys_dataitemdetail;
-- 用户性别
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('101aa5c5-cf6f-11e7-8379-00155d1b9f02','1354bfb4-cf6d-11e7-8379-00155d1b9f02','M','男',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('111aa5c5-cf6f-11e7-8379-00155d1b9f02','1354bfb4-cf6d-11e7-8379-00155d1b9f02','F','女',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('121aa5c5-cf6f-11e7-8379-00155d1b9f02','1354bfb4-cf6d-11e7-8379-00155d1b9f02','O','未知',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 账户类型
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('131aa5c5-cf6f-11e7-8379-00155d1b9f02','1454bfb4-cf6d-11e7-8379-00155d1b9f02','Perpetual','永久账户',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('141aa5c5-cf6f-11e7-8379-00155d1b9f02','1454bfb4-cf6d-11e7-8379-00155d1b9f02','Temporary','临时账户',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 证件类型
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('151aa5c5-cf6f-11e7-8379-00155d1b9f02','1554bfb4-cf6d-11e7-8379-00155d1b9f02','ID','身份证',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('161aa5c5-cf6f-11e7-8379-00155d1b9f02','1554bfb4-cf6d-11e7-8379-00155d1b9f02','HKMPassport','港澳通行证',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('171aa5c5-cf6f-11e7-8379-00155d1b9f02','1554bfb4-cf6d-11e7-8379-00155d1b9f02','Passport','护照',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 国籍
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('181aa5c5-cf6f-11e7-8379-00155d1b9f02','1654bfb4-cf6d-11e7-8379-00155d1b9f02','1000','中国',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('191aa5c5-cf6f-11e7-8379-00155d1b9f02','1654bfb4-cf6d-11e7-8379-00155d1b9f02','1001','中国香港',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('201aa5c5-cf6f-11e7-8379-00155d1b9f02','1654bfb4-cf6d-11e7-8379-00155d1b9f02','1002','中国澳门',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('211aa5c5-cf6f-11e7-8379-00155d1b9f02','1654bfb4-cf6d-11e7-8379-00155d1b9f02','1003','中国台湾',NULL,0,4,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 民族
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('221aa5c5-cf6f-11e7-8379-00155d1b9f02','1754bfb4-cf6d-11e7-8379-00155d1b9f02','00','汉族',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('231aa5c5-cf6f-11e7-8379-00155d1b9f02','1754bfb4-cf6d-11e7-8379-00155d1b9f02','01','壮族',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 婚姻状况
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('241aa5c5-cf6f-11e7-8379-00155d1b9f02','1854bfb4-cf6d-11e7-8379-00155d1b9f02','1','未婚',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('251aa5c5-cf6f-11e7-8379-00155d1b9f02','1854bfb4-cf6d-11e7-8379-00155d1b9f02','2','已婚',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('261aa5c5-cf6f-11e7-8379-00155d1b9f02','1854bfb4-cf6d-11e7-8379-00155d1b9f02','3','保密',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 血型
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('271aa5c5-cf6f-11e7-8379-00155d1b9f02','1954bfb4-cf6d-11e7-8379-00155d1b9f02','A','A',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('281aa5c5-cf6f-11e7-8379-00155d1b9f02','1954bfb4-cf6d-11e7-8379-00155d1b9f02','B','B',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('291aa5c5-cf6f-11e7-8379-00155d1b9f02','1954bfb4-cf6d-11e7-8379-00155d1b9f02','O','O',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('301aa5c5-cf6f-11e7-8379-00155d1b9f02','1954bfb4-cf6d-11e7-8379-00155d1b9f02','AB','AB',NULL,0,4,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 学历
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('311aa5c5-cf6f-11e7-8379-00155d1b9f02','2054bfb4-cf6d-11e7-8379-00155d1b9f02','01','本科',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('321aa5c5-cf6f-11e7-8379-00155d1b9f02','2054bfb4-cf6d-11e7-8379-00155d1b9f02','02','专科',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('331aa5c5-cf6f-11e7-8379-00155d1b9f02','2054bfb4-cf6d-11e7-8379-00155d1b9f02','03','硕士',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('341aa5c5-cf6f-11e7-8379-00155d1b9f02','2054bfb4-cf6d-11e7-8379-00155d1b9f02','04','博士',NULL,0,4,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('351aa5c5-cf6f-11e7-8379-00155d1b9f02','2054bfb4-cf6d-11e7-8379-00155d1b9f02','05','高中',NULL,0,5,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('361aa5c5-cf6f-11e7-8379-00155d1b9f02','2054bfb4-cf6d-11e7-8379-00155d1b9f02','06','初中',NULL,0,6,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 卡类型
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('371aa5c5-cf6f-11e7-8379-00155d1b9f02','2154bfb4-cf6d-11e7-8379-00155d1b9f02','MedicalCard','就诊卡',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('381aa5c5-cf6f-11e7-8379-00155d1b9f02','2154bfb4-cf6d-11e7-8379-00155d1b9f02','SocialSCard','社保卡',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',1,1);
-- 支付方式
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('391aa5c5-cf6f-11e7-8379-00155d1b9f02','2354bfb4-cf6d-11e7-8379-00155d1b9f02','Cash','现金',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('401aa5c5-cf6f-11e7-8379-00155d1b9f02','2354bfb4-cf6d-11e7-8379-00155d1b9f02','UnionPay','银联',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('411aa5c5-cf6f-11e7-8379-00155d1b9f02','2354bfb4-cf6d-11e7-8379-00155d1b9f02','WeChat','微信',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('421aa5c5-cf6f-11e7-8379-00155d1b9f02','2354bfb4-cf6d-11e7-8379-00155d1b9f02','Alipay','支付宝',NULL,0,4,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
-- 黑白名单类型
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('431aa5c5-cf6f-11e7-8379-00155d1b9f02','2654bfb4-cf6d-11e7-8379-00155d1b9f02','Black','黑名单',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('441aa5c5-cf6f-11e7-8379-00155d1b9f02','2654bfb4-cf6d-11e7-8379-00155d1b9f02','White','白名单',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);

-- 联系人关系
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('451aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','01','父女',NULL,0,1,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('461aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','02','母女',NULL,0,2,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('471aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','03','父子',NULL,0,3,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('481aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','04','母子',NULL,0,4,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('491aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','05','夫妻',NULL,0,5,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('501aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','06','同事',NULL,0,6,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);
REPLACE  INTO `sys_dataitemdetail`(`Id`,`ItemId`,`ItemCode`,`ItemValue`,`SimpleSpelling`,`IsDefault`,`SortCode`,`IsDelete`,`IsEnabled`,`Description`,`Creator`,`CreateTime`,`AllowEdit`,`AllowDelete`) VALUES ('511aa5c5-cf6f-11e7-8379-00155d1b9f02','2754bfb4-cf6d-11e7-8379-00155d1b9f02','07','其他',NULL,0,7,0,1,NULL,'dbd724ae-e178-42a2-a8b2-bf6f91ea0ab9','2017-11-22 18:26:06',0,0);



UPDATE sys_dataitemdetail a,temp_sys_dataitemdetail b SET a.SortCode=b.SortCode,a.IsDefault = b.IsDefault,a.IsEnabled=b.IsEnabled,a.IsDelete =b.IsDelete,a.Description=b.Description   WHERE a.Id = b.Id;
DROP TABLE temp_sys_dataitemdetail;