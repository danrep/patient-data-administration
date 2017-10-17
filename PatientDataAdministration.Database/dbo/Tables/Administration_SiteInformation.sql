CREATE TABLE [dbo].[Administration_SiteInformation] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [SiteName]       VARCHAR (100) NOT NULL,
    [SiteAddress]    VARCHAR (100) NOT NULL,
    [SiteStateId]    INT           NOT NULL,
    [DateConfigured] DATETIME      NOT NULL,
    [IsDeleted]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

