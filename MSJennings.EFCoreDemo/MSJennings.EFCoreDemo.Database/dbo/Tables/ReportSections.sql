CREATE TABLE [dbo].[ReportSections] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [ReportId] INT            NOT NULL,
    [Title]    NVARCHAR (100) NOT NULL,
    [Body]     NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ReportSections] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportSections_Reports_ReportId] FOREIGN KEY ([ReportId]) REFERENCES [dbo].[Reports] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_ReportSections_ReportId]
    ON [dbo].[ReportSections]([ReportId] ASC);

