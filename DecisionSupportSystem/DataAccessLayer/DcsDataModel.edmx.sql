
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/08/2018 22:05:33
-- Generated from EDMX file: C:\Users\4an41k\source\repos\DecisionSupportSystem\DecisionSupportSystem\DataAccessLayer\DcsDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DcsStorage];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_taskalternative]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[alternativeSet] DROP CONSTRAINT [FK_taskalternative];
GO
IF OBJECT_ID(N'[dbo].[FK_taskcriteria]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[criteriaSet] DROP CONSTRAINT [FK_taskcriteria];
GO
IF OBJECT_ID(N'[dbo].[FK_alternativecrit_value]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[crit_valueSet] DROP CONSTRAINT [FK_alternativecrit_value];
GO
IF OBJECT_ID(N'[dbo].[FK_criteriacrit_value]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[crit_valueSet] DROP CONSTRAINT [FK_criteriacrit_value];
GO
IF OBJECT_ID(N'[dbo].[FK_crit_scalecrit_value]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[crit_valueSet] DROP CONSTRAINT [FK_crit_scalecrit_value];
GO
IF OBJECT_ID(N'[dbo].[FK_criteriacrit_scale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[crit_scaleSet] DROP CONSTRAINT [FK_criteriacrit_scale];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[taskSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[taskSet];
GO
IF OBJECT_ID(N'[dbo].[alternativeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[alternativeSet];
GO
IF OBJECT_ID(N'[dbo].[crit_valueSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crit_valueSet];
GO
IF OBJECT_ID(N'[dbo].[crit_scaleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[crit_scaleSet];
GO
IF OBJECT_ID(N'[dbo].[criteriaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[criteriaSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'taskSet'
CREATE TABLE [dbo].[taskSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'alternativeSet'
CREATE TABLE [dbo].[alternativeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [task_id] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [rank] float  NOT NULL,
    [relative_alternative] nvarchar(max)  NOT NULL,
    [user_mark] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'crit_valueSet'
CREATE TABLE [dbo].[crit_valueSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [criteria_id] int  NOT NULL,
    [alternative_id] int  NOT NULL,
    [person_id] int  NOT NULL,
    [crit_scale_id] int  NOT NULL,
    [value] float  NOT NULL
);
GO

-- Creating table 'crit_scaleSet'
CREATE TABLE [dbo].[crit_scaleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [criteria_id] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [rank] float  NOT NULL
);
GO

-- Creating table 'criteriaSet'
CREATE TABLE [dbo].[criteriaSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [task_id] int  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [rank] float  NOT NULL,
    [relative_criteria] nvarchar(max)  NOT NULL,
    [user_mark] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'taskSet'
ALTER TABLE [dbo].[taskSet]
ADD CONSTRAINT [PK_taskSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'alternativeSet'
ALTER TABLE [dbo].[alternativeSet]
ADD CONSTRAINT [PK_alternativeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'crit_valueSet'
ALTER TABLE [dbo].[crit_valueSet]
ADD CONSTRAINT [PK_crit_valueSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'crit_scaleSet'
ALTER TABLE [dbo].[crit_scaleSet]
ADD CONSTRAINT [PK_crit_scaleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'criteriaSet'
ALTER TABLE [dbo].[criteriaSet]
ADD CONSTRAINT [PK_criteriaSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [task_id] in table 'alternativeSet'
ALTER TABLE [dbo].[alternativeSet]
ADD CONSTRAINT [FK_taskalternative]
    FOREIGN KEY ([task_id])
    REFERENCES [dbo].[taskSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_taskalternative'
CREATE INDEX [IX_FK_taskalternative]
ON [dbo].[alternativeSet]
    ([task_id]);
GO

-- Creating foreign key on [task_id] in table 'criteriaSet'
ALTER TABLE [dbo].[criteriaSet]
ADD CONSTRAINT [FK_taskcriteria]
    FOREIGN KEY ([task_id])
    REFERENCES [dbo].[taskSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_taskcriteria'
CREATE INDEX [IX_FK_taskcriteria]
ON [dbo].[criteriaSet]
    ([task_id]);
GO

-- Creating foreign key on [alternative_id] in table 'crit_valueSet'
ALTER TABLE [dbo].[crit_valueSet]
ADD CONSTRAINT [FK_alternativecrit_value]
    FOREIGN KEY ([alternative_id])
    REFERENCES [dbo].[alternativeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_alternativecrit_value'
CREATE INDEX [IX_FK_alternativecrit_value]
ON [dbo].[crit_valueSet]
    ([alternative_id]);
GO

-- Creating foreign key on [criteria_id] in table 'crit_valueSet'
ALTER TABLE [dbo].[crit_valueSet]
ADD CONSTRAINT [FK_criteriacrit_value]
    FOREIGN KEY ([criteria_id])
    REFERENCES [dbo].[criteriaSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_criteriacrit_value'
CREATE INDEX [IX_FK_criteriacrit_value]
ON [dbo].[crit_valueSet]
    ([criteria_id]);
GO

-- Creating foreign key on [crit_scale_id] in table 'crit_valueSet'
ALTER TABLE [dbo].[crit_valueSet]
ADD CONSTRAINT [FK_crit_scalecrit_value]
    FOREIGN KEY ([crit_scale_id])
    REFERENCES [dbo].[crit_scaleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_crit_scalecrit_value'
CREATE INDEX [IX_FK_crit_scalecrit_value]
ON [dbo].[crit_valueSet]
    ([crit_scale_id]);
GO

-- Creating foreign key on [criteria_id] in table 'crit_scaleSet'
ALTER TABLE [dbo].[crit_scaleSet]
ADD CONSTRAINT [FK_criteriacrit_scale]
    FOREIGN KEY ([criteria_id])
    REFERENCES [dbo].[criteriaSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_criteriacrit_scale'
CREATE INDEX [IX_FK_criteriacrit_scale]
ON [dbo].[crit_scaleSet]
    ([criteria_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------