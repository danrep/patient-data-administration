CREATE TABLE [dbo].[Administration_StaffInformation] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [StaffId]             VARCHAR (100) NULL,
    [Surname]             VARCHAR (100) NOT NULL,
    [FirstName]           VARCHAR (100) NOT NULL,
    [Email]               VARCHAR (100) NOT NULL,
    [PhoneNumber]         VARCHAR (100) NOT NULL,
    [PasswordData]        VARCHAR (100) NOT NULL,
    [PasswordSalt]        VARCHAR (100) NOT NULL,
    [DateRegistered]      DATETIME      NOT NULL,
    [SiteId]              INT           NOT NULL,
    [AuthenticationState] INT           NOT NULL,
    [IsDeleted]           BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

