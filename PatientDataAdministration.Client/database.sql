USE [{dbLocation}\LOCALPDA.MDF]
CREATE TABLE [dbo].[System_EndPointLog] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [EndPointId]  INT           NOT NULL,
    [EndPointUrl] VARCHAR (MAX) NOT NULL,
    [IsDeleted]   BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
~
USE [{dbLocation}\LOCALPDA.MDF];
ALTER TABLE [dbo].[System_BioDataStore] ADD [IsLocalPush] BIT NOT NULL DEFAULT(0);
~
USE [{dbLocation}\LOCALPDA.MDF]
DECLARE @query varchar(max)
SET @query = 
'
	CREATE PROCEDURE [dbo].[Sp_System_CleanUp]
	AS
	BEGIN
		UPDATE System_BioDataStore SET PepId = LTRIM(RTRIM(REPLACE(PepId, ''?'', '' ''))) WHERE LEN(PepId) > 10;
		UPDATE System_BioDataStore SET PepId = LTRIM(RTRIM(REPLACE(PepId, ''='', ''-''))) WHERE PepId LIKE ''%=%''
	END
'
EXEC (@query)
~
USE [{dbLocation}\LOCALPDA.MDF];
ALTER TABLE [dbo].[System_ErrorLog] ALTER COLUMN [ErrorString] VARCHAR(MAX) NULL;
ALTER TABLE [dbo].[System_ErrorLog] ALTER COLUMN [ErrorMessage] VARCHAR(MAX) NULL;
~
USE [{dbLocation}\LOCALPDA.MDF]
CREATE TABLE [dbo].[System_BioDataStore_PopulationRegister] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [HtsId]           VARCHAR (100) NOT NULL,
    [TestResult]           VARCHAR (100) NOT NULL,
    [SiteId]          INT           NOT NULL,
    [PrimaryFinger]   VARCHAR (MAX) NULL,
    [SecondaryFinger] VARCHAR (MAX) NULL,
    [PatientData]     VARCHAR (MAX) NULL,
    [LastUpdate]      DATETIME      DEFAULT (getdate()) NOT NULL,
    [LastSync]        DATETIME      DEFAULT (getdate()) NOT NULL,
    [IsSync]          BIT           NOT NULL,
    [IsLocalPush]     BIT           DEFAULT ((0)) NOT NULL,
    [IsDeleted]       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
~
USE [{dbLocation}\LOCALPDA.MDF];
ALTER TABLE [dbo].[System_BioDataStore] ADD [PrimaryFingerPosition] VARCHAR (20) NULL;
~
USE [{dbLocation}\LOCALPDA.MDF];
ALTER TABLE [dbo].[System_BioDataStore] ADD [SecondaryFingerPosition] VARCHAR (20) NULL;
~
USE [{dbLocation}\LOCALPDA.MDF];
ALTER TABLE [dbo].[System_BioDataStore_PopulationRegister] ADD [PrimaryFingerPosition] VARCHAR (20) NULL;
~
USE [{dbLocation}\LOCALPDA.MDF];
ALTER TABLE [dbo].[System_BioDataStore_PopulationRegister] ADD [SecondaryFingerPosition] VARCHAR (20) NULL;
~
USE [{dbLocation}\LOCALPDA.MDF];
CREATE TABLE [dbo].[System_AuditTrail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	AuditTimeStamp DATETIME NOT NULL DEFAULT(GETDATE()), 
	IsRestrcitedOperation BIT NOT NULL, 
	UserPerformed VARCHAR(MAX) NOT NULL, 
	ActionPerformed VARCHAR(MAX) NOT NULL,
	IsDeleted BIT NOT NULL
);
~
USE [{dbLocation}\LOCALPDA.MDF];
UPDATE System_BioDataStore SET PrimaryFingerPosition = 'Left Thumb', PrimaryFingerPosition = 'Right Thumb';
UPDATE System_BioDataStore_PopulationRegister SET PrimaryFingerPosition = 'Left Thumb', PrimaryFingerPosition = 'Right Thumb';
UPDATE System_Setting SET SettingValue = 'http://pbs.apin.org.ng' WHERE SettingValue = 'http://41.242.57.43'; 