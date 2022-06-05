CREATE TABLE [clf].[Languages] (
    [Id]          SMALLINT       IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Alpha2]      CHAR (2)       NOT NULL,
    [Notes]       NVARCHAR (MAX) NULL,
    [DigitalCode] CHAR (3)       NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Alpha2] UNIQUE NONCLUSTERED ([Alpha2] ASC)
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Classifier Languages',
    @level0type = N'SCHEMA',
    @level0name = N'clf',
    @level1type = N'TABLE',
    @level1name = N'Languages',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ISO language name',
    @level0type = N'SCHEMA',
    @level0name = N'clf',
    @level1type = N'TABLE',
    @level1name = N'Languages',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Codes for the representation of names of languages—Part 1: Alpha-2 code',
    @level0type = N'SCHEMA',
    @level0name = N'clf',
    @level1type = N'TABLE',
    @level1name = N'Languages',
    @level2type = N'COLUMN',
    @level2name = N'Alpha2'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The digital code consisting of 3 Arabic numerals and assigned to languages arranged in the order of Russian names.',
    @level0type = N'SCHEMA',
    @level0name = N'clf',
    @level1type = N'TABLE',
    @level1name = N'Languages',
    @level2type = N'COLUMN',
    @level2name = N'DigitalCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Language notes',
    @level0type = N'SCHEMA',
    @level0name = N'clf',
    @level1type = N'TABLE',
    @level1name = N'Languages',
    @level2type = N'COLUMN',
    @level2name = N'Notes'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ID',
    @level0type = N'SCHEMA',
    @level0name = N'clf',
    @level1type = N'TABLE',
    @level1name = N'Languages',
    @level2type = N'COLUMN',
    @level2name = N'Id'