CREATE PROCEDURE FI_SP_VerificaCliente
    @CPF varchar(11)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[CLIENTES]
    WHERE [CPF] = @CPF;
END
