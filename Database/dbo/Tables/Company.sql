CREATE TABLE [dbo].[Company] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Isin]     NVARCHAR (450)   NOT NULL,
    [Name]     NVARCHAR (MAX)   NOT NULL,
    [Exchange] NVARCHAR (MAX)   NOT NULL,
    [Ticker]   NVARCHAR (MAX)   NOT NULL,
    [Website]  NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Company_Isin]
    ON [dbo].[Company]([Isin] ASC);

