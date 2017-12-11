CREATE TABLE [dbo].[System_LocalGovermentArea] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [LocalGovermentAreaName] VARCHAR (100) NULL,
    [StateID]                INT           NULL,
    [LocalGovermentAreaCode] CHAR (3)      NULL,
    [IsDeleted]              BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

