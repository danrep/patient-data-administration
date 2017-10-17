CREATE TABLE [dbo].[Patient_PatientInformation] (
    [Id]                        INT           IDENTITY (1, 1) NOT NULL,
    [PepId]                     VARCHAR (100) NOT NULL,
    [Title]                     VARCHAR (100) NOT NULL,
    [Surname]                   VARCHAR (100) NOT NULL,
    [Othername]                 VARCHAR (100) NOT NULL,
    [Sex]                       VARCHAR (10)  NOT NULL,
    [DateOfBirth]               DATETIME      NOT NULL,
    [PatientPhysicalDataHeight] DECIMAL (18)  NULL,
    [PatientPhysicalDataWeight] DECIMAL (18)  NULL,
    [StateOfOrigin]             INT           NOT NULL,
    [SiteId]                    INT           NOT NULL,
    [HospitalId]                INT           NOT NULL,
    [BioDataId]                 INT           NOT NULL,
    [PassportData]              IMAGE         NOT NULL,
    [IsDeleted]                 BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

