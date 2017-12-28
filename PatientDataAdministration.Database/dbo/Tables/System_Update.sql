CREATE TABLE [dbo].[System_Update] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [DateProvided]   DATETIME      NOT NULL,
    [DateDownloaded] DATETIME      NOT NULL,
    [IsNew]          BIT           NOT NULL,
    [VersionNumber]  VARCHAR (100) NOT NULL,
    [ServerLocation] VARCHAR (100) NOT NULL,
    [ServerUsername] VARCHAR (100) NOT NULL,
    [ServerPassword] VARCHAR (100) NOT NULL,
    [FolderLocation] VARCHAR (MAX) NOT NULL,
    [IsDeleted]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

