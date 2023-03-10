USE [PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[USP_SELECT_USERS]    Script Date: 30/12/2022 20:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HENRIQUE SOUZA
-- Create date: 28/12/2022
-- Description:	NONE
-- =============================================
ALTER PROCEDURE [dbo].[USP_SELECT_USERS]
				 @NAME		VARCHAR = NULL,
				 @EMAIL		VARCHAR = NULL,
				 @USER_NAME VARCHAR = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[USERS] USERS 
	WHERE (USERS.NAME LIKE  @NAME OR  @NAME IS NULL ) AND
		  (USERS.EMAIL LIKE  @EMAIL OR  @EMAIL IS NULL ) AND
		  (USERS.USER_NAME LIKE  @USER_NAME OR  @USER_NAME IS NULL ) 
END
