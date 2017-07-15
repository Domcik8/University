
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/10/2014 12:32:34
-- Generated from EDMX file: C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\Muzikos Duomenų bazė\Mini Entity Frame\Mini Entity Frame\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [mini];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Daina__AutoriusI__164452B1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Daina] DROP CONSTRAINT [FK__Daina__AutoriusI__164452B1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Autorius]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Autorius];
GO
IF OBJECT_ID(N'[dbo].[Daina]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Daina];
GO
IF OBJECT_ID(N'[dbo].[Table]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Table];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Autorius'
CREATE TABLE [dbo].[Autorius] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Autorius] nchar(15)  NULL,
    [Tautybe] nchar(15)  NULL
);
GO

-- Creating table 'Dainas'
CREATE TABLE [dbo].[Dainas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Pavadinimas] nchar(15)  NULL,
    [Trukme] int  NULL,
    [AutoriusId] int  NULL
);
GO

-- Creating table 'Tables'
CREATE TABLE [dbo].[Tables] (
    [Id] int  NOT NULL,
    [Pavadinimas] nchar(15)  NULL,
    [Trukme] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Autorius'
ALTER TABLE [dbo].[Autorius]
ADD CONSTRAINT [PK_Autorius]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dainas'
ALTER TABLE [dbo].[Dainas]
ADD CONSTRAINT [PK_Dainas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tables'
ALTER TABLE [dbo].[Tables]
ADD CONSTRAINT [PK_Tables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AutoriusId] in table 'Dainas'
ALTER TABLE [dbo].[Dainas]
ADD CONSTRAINT [FK__Daina__AutoriusI__164452B1]
    FOREIGN KEY ([AutoriusId])
    REFERENCES [dbo].[Autorius]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__Daina__AutoriusI__164452B1'
CREATE INDEX [IX_FK__Daina__AutoriusI__164452B1]
ON [dbo].[Dainas]
    ([AutoriusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------