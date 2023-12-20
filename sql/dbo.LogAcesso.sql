USE [AleffDB]
GO

/****** Object: Table [dbo].[LogAcesso] Script Date: 20/12/2023 16:13:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LogAcesso] (
    [LogAcessoId]    INT          IDENTITY (1, 1) NOT NULL,
    [UsuarioId]      INT          NULL,
    [DataHoraAcesso] DATETIME     NULL,
    [EnderecoIp]     VARCHAR (50) NULL
);


