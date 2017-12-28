CREATE TABLE [dbo].[System_State] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [StateName]         VARCHAR (50) NULL,
    [StateCode]         VARCHAR (4)  NULL,
    [StateAbbreviation] VARCHAR (4)  NULL,
    [IsDeleted]         BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

