CREATE TABLE [dbo].[Administration_PatientRegistrationLog] (
    [Id]                BIGINT       IDENTITY (1, 1) NOT NULL,
    [DateLogged]        DATETIME     NOT NULL,
    [PepId]             VARCHAR (20) NOT NULL,
    [ClientId]          INT          NOT NULL,
    [IsBioDataCaptured] BIT          NOT NULL,
    [IsNfcDataCaptured] BIT          NOT NULL,
    [IsDeleted]         BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

