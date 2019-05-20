CREATE TABLE [dbo].[Administration_ClientLog] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [ClientId]        INT           NOT NULL,
    [CurrentUserId]   INT           NOT NULL,
    [DateLog]         DATETIME      NOT NULL,
    [LocationLat]     VARCHAR (100) NOT NULL,
    [LocationLong]    VARCHAR (100) NOT NULL,
    [ChipData]        VARCHAR (MAX) NOT NULL,
    [ClientIpAddress] VARCHAR (100) NOT NULL,
    [IsDeleted]       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

