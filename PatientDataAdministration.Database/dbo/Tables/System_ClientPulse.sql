CREATE TABLE [dbo].[System_ClientPulse] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [CheckInPeriod] DATETIME      DEFAULT (getdate()) NOT NULL,
    [ClientId]      VARCHAR (100) NOT NULL,
    [UserId]        BIGINT        NOT NULL,
    [AppVersion]    VARCHAR (100) NOT NULL,
    [IsDeleted]     BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

