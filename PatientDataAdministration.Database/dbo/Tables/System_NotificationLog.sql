CREATE TABLE [dbo].[System_NotificationLog] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [NotificationId] INT      NOT NULL,
    [DateLastSent]   DATETIME NOT NULL,
    [Interval]       INT      NOT NULL,
    [IsDeleted]      BIT      NOT NULL,
    [IsLatest]       BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

