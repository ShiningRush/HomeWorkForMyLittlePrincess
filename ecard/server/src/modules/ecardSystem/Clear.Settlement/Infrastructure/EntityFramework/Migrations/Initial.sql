ALTER TABLE ec_billingrecord ENGINE=MyISAM;
ALTER TABLE ec_billingrecord DROP PRIMARY KEY, ADD PRIMARY KEY (Id, CreateTime);
ALTER TABLE ec_billingrecord PARTITION BY RANGE (TO_DAYS(CreateTime))
(
     PARTITION Part_Default VALUES LESS THAN(TO_DAYS('2017-01-01'))
);
DROP PROCEDURE IF EXISTS `SP_CreateNextYearPartitionByQuarter`;
CREATE PROCEDURE `SP_CreateNextYearPartitionByQuarter`(in tableName VARCHAR(50),in currentYear INT)
COMMENT '按照季度创建分区'
BEGIN
  DECLARE NextYear INT DEFAULT 0;
  DECLARE i_flag INT DEFAULT 0;
  IF currentYear=0 THEN
     SET  currentYear = Year(NOW());
  END IF;
  SET NextYear = currentYear + 1;
  select count(*) into i_flag from information_schema.PARTITIONS where TABLE_SCHEMA = schema() and TABLE_NAME = tableName and partition_name = CONCAT('Part_', cast(NextYear as char(4)), '01');
  IF i_flag = 0 THEN
      SET @v_add1 = CONCAT('ALTER TABLE ', tableName, ' ADD PARTITION (PARTITION Part_', cast(NextYear as char(4)), '01', ' VALUES LESS THAN (to_days(''', cast(NextYear as char(4)), '-04-01''', ')));');
      SET @v_add2 = CONCAT('ALTER TABLE ', tableName, ' ADD PARTITION (PARTITION Part_', cast(NextYear as char(4)), '02', ' VALUES LESS THAN (to_days(''', cast(NextYear as char(4)), '-07-01''', ')));');
      SET @v_add3 = CONCAT('ALTER TABLE ', tableName, ' ADD PARTITION (PARTITION Part_', cast(NextYear as char(4)), '03', ' VALUES LESS THAN (to_days(''', cast(NextYear as char(4)), '-10-01''', ')));');
      SET @v_add4 = CONCAT('ALTER TABLE ', tableName, ' ADD PARTITION (PARTITION Part_', cast(NextYear as char(4)), '04', ' VALUES LESS THAN (to_days(''', cast(NextYear + 1 as char(4)), '-01-01''', ')));');
      START TRANSACTION;
         prepare stmt1 from @v_add1;
         execute stmt1;
         deallocate prepare stmt1;
         prepare stmt2 from @v_add2;
         execute stmt2;
         deallocate prepare stmt2;
         prepare stmt3 from @v_add3;
         execute stmt3;
         deallocate prepare stmt3;
         prepare stmt4 from @v_add4;
         execute stmt4;
         deallocate prepare stmt4;
     COMMIT;
  END IF;
END;

CALL SP_CreateNextYearPartitionByQuarter('ec_billingrecord',Year(NOW())-1);

DROP EVENT IF EXISTS `e_AutoCreatePartiton`;
CREATE EVENT `e_AutoCreatePartiton` ON SCHEDULE EVERY 1 DAY STARTS '2017-01-01 00:00:00' ON COMPLETION PRESERVE ENABLE DO 
BEGIN
  CALL SP_CreateNextYearPartitionByQuarter('ec_billingrecord',0);
END;