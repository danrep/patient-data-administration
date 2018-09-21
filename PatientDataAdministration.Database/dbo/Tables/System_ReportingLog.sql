CREATE TABLE [dbo].[System_ReportingLog] (
    [Id]         BIGINT   IDENTITY (1, 1) NOT NULL,
    [ReportId]   INT      NOT NULL,
    [IntervalId] INT      NOT NULL,
    [ReportDate] DATETIME NOT NULL,
    [IsCurrent]  BIT      NOT NULL,
    [IsDeleted]  BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

