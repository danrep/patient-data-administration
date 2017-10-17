CREATE TABLE [dbo].[Administration_ClientMap] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [ClientId]       INT      NOT NULL,
    [StaffId]        INT      NOT NULL,
    [DateConfigured] DATETIME NOT NULL,
    [IsDeleted]      BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

