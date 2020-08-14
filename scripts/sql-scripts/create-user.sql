USE [master];
GO
CREATE LOGIN financialservicesuser 
    WITH PASSWORD    = N'financialservicesuser',
    CHECK_POLICY     = OFF,
    CHECK_EXPIRATION = OFF;
GO
EXEC sp_addsrvrolemember 
    @loginame = N'financialservicesuser', 
    @rolename = N'sysadmin';

CREATE DATABASE financialservice