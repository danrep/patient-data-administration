CREATE TABLE [dbo].[Administration_SiteInformation] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [StateId]                INT            DEFAULT ((0)) NOT NULL,
    [SiteCode]               NVARCHAR (255) NULL,
    [SiteCodeExposedInfants] NVARCHAR (255) NULL,
    [SiteCodePediatric]      NVARCHAR (255) NULL,
    [SiteCodePMTCT]          NVARCHAR (255) NULL,
    [SiteCodeVCT]            NVARCHAR (255) NULL,
    [SiteNameInformal]       NVARCHAR (255) NULL,
    [SiteNameOfficial]       NVARCHAR (255) NULL,
    [IsDeleted]              BIT            CONSTRAINT [DF_Administration_SiteInformation_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Administration_SiteInformation] PRIMARY KEY CLUSTERED ([Id] ASC)
);



