/*
   segunda-feira, 8 de agosto de 201116:27:02
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
ALTER TABLE dbo.t29_acoes_vinculadas_projeto ADD
	fl_deletado bit NULL
GO
ALTER TABLE dbo.t29_acoes_vinculadas_projeto SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
