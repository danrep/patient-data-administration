CREATE TABLE [dbo].[Patient_PatientNearFieldCommunicationData] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [PepId]          VARCHAR (100) NOT NULL,
    [CardId]         VARCHAR (MAX) NOT NULL,
    [CardData]       VARCHAR (MAX) NOT NULL,
    [DateRegistered] DATETIME      NOT NULL,
    [IsValid]        BIT           NOT NULL,
    [IsDeleted]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

