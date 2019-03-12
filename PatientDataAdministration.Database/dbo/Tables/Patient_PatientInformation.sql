CREATE TABLE [dbo].[Patient_PatientInformation] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PepId]             VARCHAR (100) NOT NULL,
    [PreviousId]        VARCHAR (100) NOT NULL,
    [SiteId]            INT           NOT NULL,
    [Title]             VARCHAR (100) NOT NULL,
    [Surname]           VARCHAR (100) NOT NULL,
    [Othername]         VARCHAR (100) NOT NULL,
    [Sex]               VARCHAR (10)  NOT NULL,
    [PhoneNumber]       VARCHAR (20)  NOT NULL,
    [MaritalStatus]     VARCHAR (10)  NOT NULL,
    [DateOfBirth]       DATETIME      NULL,
    [StateOfOrigin]     INT           NOT NULL,
    [HouseAddress]      VARCHAR (MAX) NOT NULL,
    [HouseAddressState] INT           NOT NULL,
    [HouseAddresLga]    INT           NOT NULL,
    [HospitalNumber]    VARCHAR (100) NOT NULL,
    [PassportData]      IMAGE         NULL,
    [IsDeleted]         BIT           NOT NULL,
    [WhenCreated]       DATETIME      DEFAULT (getdate()) NULL,
    [LastUpdated]       DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);






GO
CREATE NONCLUSTERED INDEX [Patient_PatientInformation_On_SiteId_IsDeleted_Include_LastUpdated]
    ON [dbo].[Patient_PatientInformation]([SiteId] ASC, [IsDeleted] ASC)
    INCLUDE([LastUpdated]);


GO
CREATE NONCLUSTERED INDEX [Patient_PatientInformation_On_IsDeleted_Include_PepId_WhenCreated_LastUpdated]
    ON [dbo].[Patient_PatientInformation]([IsDeleted] ASC)
    INCLUDE([PepId], [WhenCreated], [LastUpdated]);


GO
CREATE NONCLUSTERED INDEX [Patient_PatientInformation_On_IsDeleted_Include_PepId_SiteId_Sex_WhenCreated_LastUpdated]
    ON [dbo].[Patient_PatientInformation]([IsDeleted] ASC)
    INCLUDE([PepId], [SiteId], [Sex], [WhenCreated], [LastUpdated]);


GO
CREATE NONCLUSTERED INDEX [Patient_PatientInformation_On_IsDeleted_Include_PepId]
    ON [dbo].[Patient_PatientInformation]([IsDeleted] ASC)
    INCLUDE([PepId]);


GO
CREATE NONCLUSTERED INDEX [Patient_PatientInformation_On_DateOfBirth]
    ON [dbo].[Patient_PatientInformation]([DateOfBirth] ASC);


GO
CREATE NONCLUSTERED INDEX [Patient_PatientBiometricData_On_IsDeleted_Include_PepId_WhenCreated]
    ON [dbo].[Patient_PatientInformation]([IsDeleted] ASC)
    INCLUDE([PepId], [WhenCreated]);

