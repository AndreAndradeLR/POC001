/*
   quarta-feira, 10 de agosto de 201116:59:10
   Usuário: 
   Servidor: .
   Banco de Dados: dbBH
   Aplicativo: 
*/

/* Para impedir possíveis problemas de perda de dados, analise este script detalhadamente antes de executá-lo fora do contexto do designer de banco de dados.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.t26_arearesultado ADD
	nu_ordem int NULL
GO
ALTER TABLE dbo.t26_arearesultado SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
