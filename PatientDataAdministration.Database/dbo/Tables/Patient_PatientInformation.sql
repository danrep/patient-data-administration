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
    [DateOfBirth]       DATETIME      NOT NULL,
    [StateOfOrigin]     INT           NOT NULL,
    [HouseAddress]      VARCHAR (MAX) NOT NULL,
    [HouseAddressState] INT           NOT NULL,
    [HouseAddresLga]    INT           NOT NULL,
    [HospitalNumber]    VARCHAR (100) NOT NULL,
    [PassportData]      IMAGE         NULL,
    [IsDeleted]         BIT           NOT NULL,
    [WhenCreated]       DATETIME      CONSTRAINT [DF__Patient_P__WhenC__6B24EA82] DEFAULT (getdate()) NULL,
    [LastUpdated]       DATETIME      CONSTRAINT [DF__Patient_P__LastU__6C190EBB] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK__tmp_ms_x__3214EC07D794095D] PRIMARY KEY CLUSTERED ([Id] ASC)
);

