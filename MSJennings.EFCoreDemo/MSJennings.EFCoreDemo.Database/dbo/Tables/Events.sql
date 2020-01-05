CREATE TABLE [dbo].[Events] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (100) NOT NULL,
    [Date]       DATE           NOT NULL,
    [LocationId] INT            NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Events_LocationId]
    ON [dbo].[Events]([LocationId] ASC);

