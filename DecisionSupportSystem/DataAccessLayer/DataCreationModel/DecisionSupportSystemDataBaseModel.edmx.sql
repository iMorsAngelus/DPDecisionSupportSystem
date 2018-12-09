
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2018 13:51:48
-- Generated from EDMX file: C:\Users\4an41k\source\repos\DPDecisionSupportSystem\DecisionSupportSystem\DataAccessLayer\DataCreationModel\DecisionSupportSystemDataBaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DSSDBv1.0];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TaskAlternative]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AlternativeSet] DROP CONSTRAINT [FK_TaskAlternative];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskCriteria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CriteriaSet] DROP CONSTRAINT [FK_TaskCriteria];
GO
IF OBJECT_ID(N'[dbo].[FK_FirstCriteriaPairCriteria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PairCriteriaSet] DROP CONSTRAINT [FK_FirstCriteriaPairCriteria];
GO
IF OBJECT_ID(N'[dbo].[FK_SecondCriteriaPairCriteria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PairCriteriaSet] DROP CONSTRAINT [FK_SecondCriteriaPairCriteria];
GO
IF OBJECT_ID(N'[dbo].[FK_SecondAlternativePairAltternative]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PairAlternativeSet] DROP CONSTRAINT [FK_SecondAlternativePairAltternative];
GO
IF OBJECT_ID(N'[dbo].[FK_FirstAlternativePairAltternative]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PairAlternativeSet] DROP CONSTRAINT [FK_FirstAlternativePairAltternative];
GO
IF OBJECT_ID(N'[dbo].[FK_CriteriaPairAltternative]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PairAlternativeSet] DROP CONSTRAINT [FK_CriteriaPairAltternative];
GO
IF OBJECT_ID(N'[dbo].[FK_CriteriaCriteriaPriorityVector]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CriteriaPriorityVectorSet] DROP CONSTRAINT [FK_CriteriaCriteriaPriorityVector];
GO
IF OBJECT_ID(N'[dbo].[FK_AlternativeAlternativePriorityVector]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AlternativePriorityVectorSet] DROP CONSTRAINT [FK_AlternativeAlternativePriorityVector];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TaskSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskSet];
GO
IF OBJECT_ID(N'[dbo].[CriteriaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CriteriaSet];
GO
IF OBJECT_ID(N'[dbo].[AlternativeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AlternativeSet];
GO
IF OBJECT_ID(N'[dbo].[PairAlternativeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PairAlternativeSet];
GO
IF OBJECT_ID(N'[dbo].[PairCriteriaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PairCriteriaSet];
GO
IF OBJECT_ID(N'[dbo].[CriteriaPriorityVectorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CriteriaPriorityVectorSet];
GO
IF OBJECT_ID(N'[dbo].[AlternativePriorityVectorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AlternativePriorityVectorSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TaskSet'
CREATE TABLE [dbo].[TaskSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NULL
);
GO

-- Creating table 'CriteriaSet'
CREATE TABLE [dbo].[CriteriaSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [taskId] bigint  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NULL
);
GO

-- Creating table 'AlternativeSet'
CREATE TABLE [dbo].[AlternativeSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [taskId] bigint  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PairAlternativeSet'
CREATE TABLE [dbo].[PairAlternativeSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [firstAlternativeId] bigint  NOT NULL,
    [secondAlternativeId] bigint  NOT NULL,
    [criteriaId] bigint  NOT NULL,
    [minRangeValue] float  NOT NULL,
    [middleRangeValue] float  NOT NULL,
    [maxRangeValue] float  NOT NULL
);
GO

-- Creating table 'PairCriteriaSet'
CREATE TABLE [dbo].[PairCriteriaSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [firstCriteriaId] bigint  NOT NULL,
    [secondCriteriaId] bigint  NOT NULL,
    [minRangeValue] float  NOT NULL,
    [middleRangeValue] float  NOT NULL,
    [maxRangeValue] float  NOT NULL
);
GO

-- Creating table 'CriteriaPriorityVectorSet'
CREATE TABLE [dbo].[CriteriaPriorityVectorSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [criteriaId] bigint  NOT NULL,
    [value] float  NOT NULL,
    [criteria_id] bigint  NOT NULL
);
GO

-- Creating table 'AlternativePriorityVectorSet'
CREATE TABLE [dbo].[AlternativePriorityVectorSet] (
    [id] bigint IDENTITY(1,1) NOT NULL,
    [alternativeId] bigint  NOT NULL,
    [value] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'TaskSet'
ALTER TABLE [dbo].[TaskSet]
ADD CONSTRAINT [PK_TaskSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'CriteriaSet'
ALTER TABLE [dbo].[CriteriaSet]
ADD CONSTRAINT [PK_CriteriaSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'AlternativeSet'
ALTER TABLE [dbo].[AlternativeSet]
ADD CONSTRAINT [PK_AlternativeSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'PairAlternativeSet'
ALTER TABLE [dbo].[PairAlternativeSet]
ADD CONSTRAINT [PK_PairAlternativeSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'PairCriteriaSet'
ALTER TABLE [dbo].[PairCriteriaSet]
ADD CONSTRAINT [PK_PairCriteriaSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'CriteriaPriorityVectorSet'
ALTER TABLE [dbo].[CriteriaPriorityVectorSet]
ADD CONSTRAINT [PK_CriteriaPriorityVectorSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'AlternativePriorityVectorSet'
ALTER TABLE [dbo].[AlternativePriorityVectorSet]
ADD CONSTRAINT [PK_AlternativePriorityVectorSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [taskId] in table 'AlternativeSet'
ALTER TABLE [dbo].[AlternativeSet]
ADD CONSTRAINT [FK_TaskAlternative]
    FOREIGN KEY ([taskId])
    REFERENCES [dbo].[TaskSet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskAlternative'
CREATE INDEX [IX_FK_TaskAlternative]
ON [dbo].[AlternativeSet]
    ([taskId]);
GO

-- Creating foreign key on [taskId] in table 'CriteriaSet'
ALTER TABLE [dbo].[CriteriaSet]
ADD CONSTRAINT [FK_TaskCriteria]
    FOREIGN KEY ([taskId])
    REFERENCES [dbo].[TaskSet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskCriteria'
CREATE INDEX [IX_FK_TaskCriteria]
ON [dbo].[CriteriaSet]
    ([taskId]);
GO

-- Creating foreign key on [secondCriteriaId] in table 'PairCriteriaSet'
ALTER TABLE [dbo].[PairCriteriaSet]
ADD CONSTRAINT [FK_FirstCriteriaPairCriteria]
    FOREIGN KEY ([secondCriteriaId])
    REFERENCES [dbo].[CriteriaSet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FirstCriteriaPairCriteria'
CREATE INDEX [IX_FK_FirstCriteriaPairCriteria]
ON [dbo].[PairCriteriaSet]
    ([secondCriteriaId]);
GO

-- Creating foreign key on [firstCriteriaId] in table 'PairCriteriaSet'
ALTER TABLE [dbo].[PairCriteriaSet]
ADD CONSTRAINT [FK_SecondCriteriaPairCriteria]
    FOREIGN KEY ([firstCriteriaId])
    REFERENCES [dbo].[CriteriaSet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SecondCriteriaPairCriteria'
CREATE INDEX [IX_FK_SecondCriteriaPairCriteria]
ON [dbo].[PairCriteriaSet]
    ([firstCriteriaId]);
GO

-- Creating foreign key on [firstAlternativeId] in table 'PairAlternativeSet'
ALTER TABLE [dbo].[PairAlternativeSet]
ADD CONSTRAINT [FK_SecondAlternativePairAltternative]
    FOREIGN KEY ([firstAlternativeId])
    REFERENCES [dbo].[AlternativeSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SecondAlternativePairAltternative'
CREATE INDEX [IX_FK_SecondAlternativePairAltternative]
ON [dbo].[PairAlternativeSet]
    ([firstAlternativeId]);
GO

-- Creating foreign key on [secondAlternativeId] in table 'PairAlternativeSet'
ALTER TABLE [dbo].[PairAlternativeSet]
ADD CONSTRAINT [FK_FirstAlternativePairAltternative]
    FOREIGN KEY ([secondAlternativeId])
    REFERENCES [dbo].[AlternativeSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FirstAlternativePairAltternative'
CREATE INDEX [IX_FK_FirstAlternativePairAltternative]
ON [dbo].[PairAlternativeSet]
    ([secondAlternativeId]);
GO

-- Creating foreign key on [criteriaId] in table 'PairAlternativeSet'
ALTER TABLE [dbo].[PairAlternativeSet]
ADD CONSTRAINT [FK_CriteriaPairAltternative]
    FOREIGN KEY ([criteriaId])
    REFERENCES [dbo].[CriteriaSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CriteriaPairAltternative'
CREATE INDEX [IX_FK_CriteriaPairAltternative]
ON [dbo].[PairAlternativeSet]
    ([criteriaId]);
GO

-- Creating foreign key on [criteria_id] in table 'CriteriaPriorityVectorSet'
ALTER TABLE [dbo].[CriteriaPriorityVectorSet]
ADD CONSTRAINT [FK_CriteriaCriteriaPriorityVector]
    FOREIGN KEY ([criteria_id])
    REFERENCES [dbo].[CriteriaSet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CriteriaCriteriaPriorityVector'
CREATE INDEX [IX_FK_CriteriaCriteriaPriorityVector]
ON [dbo].[CriteriaPriorityVectorSet]
    ([criteria_id]);
GO

-- Creating foreign key on [alternativeId] in table 'AlternativePriorityVectorSet'
ALTER TABLE [dbo].[AlternativePriorityVectorSet]
ADD CONSTRAINT [FK_AlternativeAlternativePriorityVector]
    FOREIGN KEY ([alternativeId])
    REFERENCES [dbo].[AlternativeSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlternativeAlternativePriorityVector'
CREATE INDEX [IX_FK_AlternativeAlternativePriorityVector]
ON [dbo].[AlternativePriorityVectorSet]
    ([alternativeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------