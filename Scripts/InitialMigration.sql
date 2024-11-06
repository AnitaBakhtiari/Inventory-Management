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

CREATE TABLE [IssuanceDocuments] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWSEQUENTIALID()),
    [Type] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_IssuanceDocuments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] bigint NOT NULL IDENTITY,
    [BrandName] nvarchar(100) NOT NULL,
    [Type] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProductInstances] (
    [Id] bigint NOT NULL IDENTITY,
    [SerialNumber] varchar(100) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [IsAvailable] bit NOT NULL,
    [ProductId] bigint NOT NULL,
    CONSTRAINT [PK_ProductInstances] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProductInstances_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [IssuanceDocumentProductInstance] (
    [IssuanceDocumentsId] uniqueidentifier NOT NULL,
    [ProductInstancesId] bigint NOT NULL,
    CONSTRAINT [PK_IssuanceDocumentProductInstance] PRIMARY KEY ([IssuanceDocumentsId], [ProductInstancesId]),
    CONSTRAINT [FK_IssuanceDocumentProductInstance_IssuanceDocuments_IssuanceDocumentsId] FOREIGN KEY ([IssuanceDocumentsId]) REFERENCES [IssuanceDocuments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_IssuanceDocumentProductInstance_ProductInstances_ProductInstancesId] FOREIGN KEY ([ProductInstancesId]) REFERENCES [ProductInstances] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_IssuanceDocumentProductInstance_ProductInstancesId] ON [IssuanceDocumentProductInstance] ([ProductInstancesId]);
GO

CREATE INDEX [IX_ProductInstances_ProductId] ON [ProductInstances] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106074054_initialMigration', N'8.0.10');
GO

COMMIT;
GO
