CREATE TABLE [dbo].[Integration_AppointmentDataManifest] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateLog]   DATETIME      NOT NULL,
    [SiteId]    INT           NOT NULL,
    [ClientId]  VARCHAR (100) NOT NULL,
    [UserId]    INT           NOT NULL,
    [IsDeleted] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

