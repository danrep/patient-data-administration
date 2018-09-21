CREATE TABLE [dbo].[System_OperationLog] (
    [Id]                 BIGINT   IDENTITY (1, 1) NOT NULL,
    [OperationProcessId] INT      NOT NULL,
    [IntervalId]         INT      NOT NULL,
    [OperationId]        INT      NOT NULL,
    [OperationDate]      DATETIME NOT NULL,
    [IsCurrent]          BIT      NOT NULL,
    [IsDeleted]          BIT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

