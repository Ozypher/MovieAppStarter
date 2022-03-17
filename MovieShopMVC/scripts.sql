IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220302213114_InitialMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Genres] DROP CONSTRAINT [PK_Genres];
GO

EXEC sp_rename N'[Genres]', N'Genre';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genre]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Genre] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Genre] ALTER COLUMN [Name] nvarchar(64) NOT NULL;
GO

ALTER TABLE [Genre] ADD CONSTRAINT [PK_Genre] PRIMARY KEY ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220302213805_ChangingGenreMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Movie] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(256) NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220302215305_MovieMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Movie] ADD [BackdropUrl] nvarchar(2084) NULL;
GO

ALTER TABLE [Movie] ADD [Budget] decimal(18,4) NULL DEFAULT 9.9;
GO

ALTER TABLE [Movie] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Movie] ADD [CreatedDate] datetime2 NULL DEFAULT (getdate());
GO

ALTER TABLE [Movie] ADD [ImdbUrl] nvarchar(2084) NULL;
GO

ALTER TABLE [Movie] ADD [OriginalLanguage] nvarchar(64) NULL;
GO

ALTER TABLE [Movie] ADD [Overview] nvarchar(max) NULL;
GO

ALTER TABLE [Movie] ADD [PosterUrl] nvarchar(2084) NULL;
GO

ALTER TABLE [Movie] ADD [Price] decimal(5,2) NULL DEFAULT 9.9;
GO

ALTER TABLE [Movie] ADD [ReleaseDate] datetime2 NULL;
GO

ALTER TABLE [Movie] ADD [Revenue] decimal(18,4) NULL DEFAULT 9.9;
GO

ALTER TABLE [Movie] ADD [RunTime] int NULL;
GO

ALTER TABLE [Movie] ADD [Tagline] nvarchar(512) NULL;
GO

ALTER TABLE [Movie] ADD [TmdbUrl] nvarchar(2084) NULL;
GO

ALTER TABLE [Movie] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Movie] ADD [UpdatedDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303153557_MovieMigrationFinal', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Trailer] (
    [Id] int NOT NULL IDENTITY,
    [TrailerURL] nvarchar(2048) NOT NULL,
    [Name] nvarchar(256) NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Trailer] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Trailer_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Trailer_MovieId] ON [Trailer] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303154330_TrailerMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieGenre] (
    [MovieID] int NOT NULL,
    [GenreID] int NOT NULL,
    CONSTRAINT [PK_MovieGenre] PRIMARY KEY ([GenreID], [MovieID]),
    CONSTRAINT [FK_MovieGenre_Genre_GenreID] FOREIGN KEY ([GenreID]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieGenre_Movie_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieGenre_MovieID] ON [MovieGenre] ([MovieID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303155923_MovieGenreJunctionMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cast] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(128) NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [ImdbUrl] nvarchar(2084) NOT NULL,
    [ProfilePath] nvarchar(2084) NOT NULL,
    CONSTRAINT [PK_Cast] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303173840_CastMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieCast] (
    [MovieId] int NOT NULL,
    [CastId] int NOT NULL,
    [Character] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_MovieCast] PRIMARY KEY ([CastId], [MovieId], [Character]),
    CONSTRAINT [FK_MovieCast_Cast_CastId] FOREIGN KEY ([CastId]) REFERENCES [Cast] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCast_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCast_MovieId] ON [MovieCast] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220303174730_MovieCastMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MovieGenre] DROP CONSTRAINT [FK_MovieGenre_Genre_GenreID];
GO

ALTER TABLE [MovieGenre] DROP CONSTRAINT [FK_MovieGenre_Movie_MovieID];
GO

ALTER TABLE [MovieGenre] DROP CONSTRAINT [PK_MovieGenre];
GO

DROP INDEX [IX_MovieGenre_MovieID] ON [MovieGenre];
GO

EXEC sp_rename N'[Trailer].[TrailerURL]', N'TrailerUrl', N'COLUMN';
GO

EXEC sp_rename N'[MovieGenre].[MovieID]', N'MovieId', N'COLUMN';
GO

EXEC sp_rename N'[MovieGenre].[GenreID]', N'GenreId', N'COLUMN';
GO

EXEC sp_rename N'[Cast].[ImdbUrl]', N'TmdbUrl', N'COLUMN';
GO

ALTER TABLE [MovieGenre] ADD CONSTRAINT [PK_MovieGenre] PRIMARY KEY ([MovieId], [GenreId]);
GO

CREATE INDEX [IX_MovieGenre_GenreId] ON [MovieGenre] ([GenreId]);
GO

ALTER TABLE [MovieGenre] ADD CONSTRAINT [FK_MovieGenre_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [MovieGenre] ADD CONSTRAINT [FK_MovieGenre_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305203436_GithubMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(128) NULL,
    [LastName] nvarchar(128) NULL,
    [DateOfBirth] datetime2(7) NULL,
    [Email] nvarchar(128) NULL,
    [HashedPassword] nvarchar(1028) NULL,
    [Salt] nvarchar(1028) NULL,
    [PhoneNumber] nvarchar(16) NULL,
    [TwoFactorEnabled] bit NULL,
    [LockoutEndDate] datetime2(7) NULL,
    [LastLoginDateTime] datetime2(7) NULL,
    [IsLocked] bit NULL,
    [AccessFailedCount] int NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305214943_UsersMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Role] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRole] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_UserRole_RoleId] ON [UserRole] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305221847_UserRolesMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Review] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [Rating] decimal(3,2) NOT NULL,
    [ReviewText] nvarchar(max) NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Review_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Review_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Review_UserId] ON [Review] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305224109_ReviewsMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchase] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [PurchaseNumber] uniqueidentifier NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [PurchaseDateTime] datetime2(7) NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Purchase_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Purchase_MovieId] ON [Purchase] ([MovieId]);
GO

CREATE INDEX [IX_Purchase_UserId] ON [Purchase] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305231650_PurchaseMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Favorite] (
    [Id] int NOT NULL IDENTITY,
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Favorite] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Favorite_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Favorite_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Favorite_MovieId] ON [Favorite] ([MovieId]);
GO

CREATE INDEX [IX_Favorite_UserId] ON [Favorite] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305232559_FavoritesMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'DateOfBirth');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [User] ALTER COLUMN [DateOfBirth] datetime2(7) NOT NULL;
ALTER TABLE [User] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [DateOfBirth];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchase]') AND [c].[name] = N'PurchaseNumber');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Purchase] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Purchase] ALTER COLUMN [PurchaseNumber] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220315043230_ReviewMigration', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [UserRole] DROP CONSTRAINT [PK_UserRole];
GO

DROP INDEX [IX_UserRole_RoleId] ON [UserRole];
GO

ALTER TABLE [UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId], [UserId]);
GO

CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220317044058_finalMigration', N'6.0.2');
GO

COMMIT;
GO

