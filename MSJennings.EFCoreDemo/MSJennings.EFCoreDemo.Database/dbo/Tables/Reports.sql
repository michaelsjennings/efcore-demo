CREATE TABLE [dbo].[Reports] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [EventId] INT            NOT NULL,
    [Title]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reports_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id])
);






GO
CREATE NONCLUSTERED INDEX [IX_Reports_EventId]
    ON [dbo].[Reports]([EventId] ASC);

