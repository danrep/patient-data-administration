CREATE TABLE [dbo].[System_AuditTrail] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]        INT            NOT NULL,
    [AuditimeStamp] DATETIME       DEFAULT (getdate()) NOT NULL,
    [AuditCategory] INT            NOT NULL,
    [AuditDetail]   VARCHAR (1000) NOT NULL,
    [AuditData]     VARCHAR (MAX)  NULL,
    [IsDeleted]     BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

