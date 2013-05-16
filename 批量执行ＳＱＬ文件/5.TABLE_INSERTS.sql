/* 插入表数据 */

PRINT '正在插入数据到表 EMPLOYEE'
GO
USE TestDB
GO
INSERT  INTO DBO.EMPLOYEE
        ( FIRSTNAME, LASTNAME )
        SELECT  'JOHN' ,
                'DOE'
GO
INSERT  INTO DBO.EMPLOYEE
        ( FIRSTNAME, LASTNAME )
        SELECT  'JANE' ,
                'DOE'
GO
INSERT  INTO DBO.EMPLOYEE
        ( FIRSTNAME, LASTNAME )
        SELECT  '黄' ,
                'DOE'
GO
