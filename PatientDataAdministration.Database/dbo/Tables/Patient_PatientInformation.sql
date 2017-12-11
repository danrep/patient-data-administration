CREATE TABLE [dbo].[Patient_PatientInformation] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PepId]             VARCHAR (100) NOT NULL,
    [PreviousId]        VARCHAR (100) NOT NULL,
    [SiteId]            INT           NOT NULL,
    [Title]             VARCHAR (100) NOT NULL,
    [Surname]           VARCHAR (100) NOT NULL,
    [Othername]         VARCHAR (100) NOT NULL,
    [Sex]               VARCHAR (10)  NOT NULL,
    [PhoneNumber]       VARCHAR (10)  NOT NULL,
    [MaritalStatus]     VARCHAR (10)  NOT NULL,
    [DateOfBirth]       DATETIME      NOT NULL,
    [StateOfOrigin]     INT           NOT NULL,
    [HouseAddress]      VARCHAR (100) NOT NULL,
    [HouseAddressState] INT           NOT NULL,
    [HouseAddresLga]    INT           NOT NULL,
    [HospitalNumber]    VARCHAR (100) NOT NULL,
    [BioDataId]         INT           NOT NULL,
    [PassportData]      IMAGE         NULL,
    [IsDeleted]         BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



