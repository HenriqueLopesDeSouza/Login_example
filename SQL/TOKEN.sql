/*
   quinta-feira, 29 de dezembro de 202220:06:26
   Usuário: 
   Servidor: HENRIQUE
   Banco de Dados: PROJECT
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
CREATE TABLE dbo.TOKEN
	(
	USERNAME varchar(200) NOT NULL,
	TOKEN varchar(MAX) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.TOKEN SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TOKEN', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TOKEN', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TOKEN', 'Object', 'CONTROL') as Contr_Per 