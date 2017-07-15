
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2014 13:02:08
-- Generated from EDMX file: C:\Users\Dominik\Desktop\VU\3 semestras\Taikomasis objektinis programavimas\Muzikos Duomenų bazė\Muzika\Muzika\BloggingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AtlikejasAlbumas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Albumai] DROP CONSTRAINT [FK_AtlikejasAlbumas];
GO
IF OBJECT_ID(N'[dbo].[FK_DainaAutorius]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dainos] DROP CONSTRAINT [FK_DainaAutorius];
GO
IF OBJECT_ID(N'[dbo].[FK_AlbumasPriklauso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Priklausantys] DROP CONSTRAINT [FK_AlbumasPriklauso];
GO
IF OBJECT_ID(N'[dbo].[FK_PriklausoDaina]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Priklausantys] DROP CONSTRAINT [FK_PriklausoDaina];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Atlikejai]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Atlikejai];
GO
IF OBJECT_ID(N'[dbo].[Albumai]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Albumai];
GO
IF OBJECT_ID(N'[dbo].[Autoriai]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Autoriai];
GO
IF OBJECT_ID(N'[dbo].[Dainos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dainos];
GO
IF OBJECT_ID(N'[dbo].[Priklausantys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Priklausantys];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Atlikejai'
CREATE TABLE [dbo].[Atlikejai] (
    [Slapyvardis] nvarchar(15)  NOT NULL,
    [Karjeros_pradzia] int  NULL,
    [Tautybe] nvarchar(10)  NULL
);
GO

-- Creating table 'Albumai'
CREATE TABLE [dbo].[Albumai] (
    [Pavadinimas] nvarchar(25)  NOT NULL,
    [Turinis_Min] float  NULL,
    [Atlikejas] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'Autoriai'
CREATE TABLE [dbo].[Autoriai] (
    [Nr] int IDENTITY(1,1) NOT NULL,
    [Slapyvardis] nvarchar(25)  NOT NULL,
    [Tautybe] nvarchar(10)  NULL,
    [Karjeros_pradzia] int  NULL
);
GO

-- Creating table 'Dainos'
CREATE TABLE [dbo].[Dainos] (
    [Pavadinimas] nvarchar(25)  NOT NULL,
    [Trukme_Min] float  NULL,
    [Zanras] nvarchar(15)  NOT NULL,
    [Autorius] int  NOT NULL
);
GO

-- Creating table 'Priklausantys'
CREATE TABLE [dbo].[Priklausantys] (
    [PriklausoNr] smallint IDENTITY(1,1) NOT NULL,
    [Albumo_Pavadinimas] nvarchar(25)  NOT NULL,
    [Dainos_Pavadinimas] nvarchar(25)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Slapyvardis] in table 'Atlikejai'
ALTER TABLE [dbo].[Atlikejai]
ADD CONSTRAINT [PK_Atlikejai]
    PRIMARY KEY CLUSTERED ([Slapyvardis] ASC);
GO

-- Creating primary key on [Pavadinimas] in table 'Albumai'
ALTER TABLE [dbo].[Albumai]
ADD CONSTRAINT [PK_Albumai]
    PRIMARY KEY CLUSTERED ([Pavadinimas] ASC);
GO

-- Creating primary key on [Nr] in table 'Autoriai'
ALTER TABLE [dbo].[Autoriai]
ADD CONSTRAINT [PK_Autoriai]
    PRIMARY KEY CLUSTERED ([Nr] ASC);
GO

-- Creating primary key on [Pavadinimas] in table 'Dainos'
ALTER TABLE [dbo].[Dainos]
ADD CONSTRAINT [PK_Dainos]
    PRIMARY KEY CLUSTERED ([Pavadinimas] ASC);
GO

-- Creating primary key on [PriklausoNr] in table 'Priklausantys'
ALTER TABLE [dbo].[Priklausantys]
ADD CONSTRAINT [PK_Priklausantys]
    PRIMARY KEY CLUSTERED ([PriklausoNr] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Atlikejas] in table 'Albumai'
ALTER TABLE [dbo].[Albumai]
ADD CONSTRAINT [FK_AtlikejasAlbumas]
    FOREIGN KEY ([Atlikejas])
    REFERENCES [dbo].[Atlikejai]
        ([Slapyvardis])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AtlikejasAlbumas'
CREATE INDEX [IX_FK_AtlikejasAlbumas]
ON [dbo].[Albumai]
    ([Atlikejas]);
GO

-- Creating foreign key on [Autorius] in table 'Dainos'
ALTER TABLE [dbo].[Dainos]
ADD CONSTRAINT [FK_DainaAutorius]
    FOREIGN KEY ([Autorius])
    REFERENCES [dbo].[Autoriai]
        ([Nr])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DainaAutorius'
CREATE INDEX [IX_FK_DainaAutorius]
ON [dbo].[Dainos]
    ([Autorius]);
GO

-- Creating foreign key on [Albumo_Pavadinimas] in table 'Priklausantys'
ALTER TABLE [dbo].[Priklausantys]
ADD CONSTRAINT [FK_AlbumasPriklauso]
    FOREIGN KEY ([Albumo_Pavadinimas])
    REFERENCES [dbo].[Albumai]
        ([Pavadinimas])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AlbumasPriklauso'
CREATE INDEX [IX_FK_AlbumasPriklauso]
ON [dbo].[Priklausantys]
    ([Albumo_Pavadinimas]);
GO

-- Creating foreign key on [Dainos_Pavadinimas] in table 'Priklausantys'
ALTER TABLE [dbo].[Priklausantys]
ADD CONSTRAINT [FK_PriklausoDaina]
    FOREIGN KEY ([Dainos_Pavadinimas])
    REFERENCES [dbo].[Dainos]
        ([Pavadinimas])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PriklausoDaina'
CREATE INDEX [IX_FK_PriklausoDaina]
ON [dbo].[Priklausantys]
    ([Dainos_Pavadinimas]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------