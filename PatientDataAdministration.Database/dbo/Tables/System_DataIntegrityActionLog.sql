CREATE TABLE [dbo].[System_DataIntegrityActionLog] (
    [Id]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [ActionDate]         DATETIME      NOT NULL,
    [ActionUser]         INT           NOT NULL,
    [ActionType]         INT           NOT NULL,
    [DataBefore]         VARCHAR (MAX) NULL,
    [DataAfter]          VARCHAR (MAX) NULL,
    [IsDeleted]          BIT           NOT NULL,
    [IsReversed]         BIT           DEFAULT ('0') NOT NULL,
    [ActionReversedUser] INT           DEFAULT ((0)) NOT NULL,
    [ActionReversedDate] DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

