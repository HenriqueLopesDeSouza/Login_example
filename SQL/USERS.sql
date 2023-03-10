/*
   quinta-feira, 29 de dezembro de 202216:48:42
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
CREATE TABLE dbo.USERS
	(
	ID int NOT NULL IDENTITY (1, 1),
	NAME varchar(200) NOT NULL,
	EMAIL varchar(200) NOT NULL,
	USER_NAME varchar(200) NOT NULL,
	PASSWORD varchar(MAX) NOT NULL,
	ROLE varchar(100) NOT NULL,
	SALT varchar(MAX) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.USERS ADD CONSTRAINT
	PK_USERS PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.USERS SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.USERS', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.USERS', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.USERS', 'Object', 'CONTROL') as Contr_Per 