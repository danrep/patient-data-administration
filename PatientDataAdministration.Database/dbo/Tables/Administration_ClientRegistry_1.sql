CREATE TABLE [dbo].[Administration_ClientRegistry] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [ClientName]     VARCHAR (100) NOT NULL,
    [ClientMAC]      VARCHAR (100) NOT NULL,
    [ClientState]    INT           NOT NULL,
    [DateConfigured] DATETIME      NOT NULL,
    [IsDeleted]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

