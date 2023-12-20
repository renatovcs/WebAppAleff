CREATE TABLE Usuario (
    UsuarioId INT IDENTITY(1,1) NOT NULL,
    Nome VARCHAR(100),
    Login VARCHAR(50),
    Senha VARCHAR(50),
    IsAdmin BIT,
    PRIMARY KEY (UsuarioId)
);


CREATE TABLE LogAcesso (
    LogAcessoId INT IDENTITY(1,1) NOT NULL,
    UsuarioId INT,
    DataHoraAcesso DATETIME,
    EnderecoIp VARCHAR(50),
    PRIMARY KEY (LogAcessoId),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId)
);
