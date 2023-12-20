CREATE PROCEDURE sp_ListaUsuario
AS
BEGIN
	SELECT UsuarioId, Nome, Login, IsAdmin FROM Usuario
END