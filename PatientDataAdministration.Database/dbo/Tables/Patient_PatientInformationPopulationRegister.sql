CREATE TABLE [dbo].[Patient_PatientInformationPopulationRegister] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [HtsId]        VARCHAR (100)  NOT NULL,
    [Surname]      VARCHAR (100)  NULL,
    [OtherName]    VARCHAR (100)  NULL,
    [HouseAddress] VARCHAR (1000) NULL,
    [SiteId]       INT            NOT NULL,
    [Sex]          VARCHAR (10)   NOT NULL,
    [PhoneNumber]  VARCHAR (20)   NOT NULL,
    [IsDeleted]    BIT            NOT NULL,
    [WhenCreated]  DATETIME       DEFAULT (getdate()) NULL,
    [LastUpdated]  DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



