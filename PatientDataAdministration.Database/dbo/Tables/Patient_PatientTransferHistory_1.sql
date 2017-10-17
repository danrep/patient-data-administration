CREATE TABLE [dbo].[Patient_PatientTransferHistory] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [PepId]          VARCHAR (100) NOT NULL,
    [PreviousSiteId] INT           NOT NULL,
    [NewSiteId]      INT           NOT NULL,
    [DateMoved]      DATETIME      NOT NULL,
    [IsDeleted]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

