CREATE TABLE [dbo].[Usuario] (
    [UsuarioId] INT           IDENTITY (1, 1) NOT NULL,
    [Nome]      VARCHAR (100) NULL,
    [Login]     VARCHAR (50)  NULL,
    [Senha]     VARCHAR (50)  NOT NULL,
    [IsAdmin]   BIT           NULL,
    PRIMARY KEY CLUSTERED ([UsuarioId] ASC)
);

