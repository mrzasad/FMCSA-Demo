Go
INSERT	dbo.Users(CreatedBy,CreatedOn,LastModifiedBy,LastModifiedOn,IsActive,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount)
VALUES
(1,  SYSDATETIME(), 1,   SYSDATETIME(), 1,N'Admin', N'ADMIN', N'admin@admin.com', N'ADMIN@ADMIN.COM',1,N'Admin@123',N'000000000000',1,1,0,0)

Go
INSERT dbo.UserClaims(UserId,ClaimType,	ClaimValue)	VALUES	( 1,N'Admin', N'admin')

Go
INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,1,'IL','60610','WEDRIVEU AMERICA LLC','Chicago','1678014','https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO


INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,1,'IL','60610','COMPASS TRANSPORTATION LLC','Skokie','1678014','https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,1,'IL','60610','WESTWAY COACH INC','Skokie','1678014','https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,2,'IL','60610','UNITED BUS INC','Chicago','1678014',
'https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,2,'IL','60610','COOK COUNTY SCHOOL BUS INC','Crestwood','1678014',
'https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,2,'IL','60610','KICKERT BUS LINES INC','Lynwood','1678014',
'https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,3,'IL','60610','AMMONS TRANSPORTATION SERVICE INC','Chicago','1678014',
'https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

INSERT INTO [dbo].[VehicleInfos]
([CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn],[IsActive],[Type],[State],[ZipCode],[CompanyName],[Location],[DOTNumber],[SafetyLink])
VALUES
(1,GETDATE(),1,GETDATE(),1,3,'IL','60610','COTTAGE HILL OPERATING COMPANY','Chicago','1678014',
'https://ai.fmcsa.dot.gov/SMS/Carrier/1678014/Overview.aspx')
GO

