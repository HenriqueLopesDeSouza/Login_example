USE [PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERT_USER]    Script Date: 29/12/2022 00:19:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HENRIQUE SOUZA
-- Create date: 28/12/2022 - dd/mm/yyyy
-- Description:	none
-- TESTE 
/*  
DECLARE @RESULT INT
DECLARE @MESSAGE VARCHAR(MAX)
EXEC [dbo].[USP_INSERT_USER] 'TESTE1','TESTE1','TESTE1','TESTE1','TESTE1', @RESULT OUT,@MESSAGE OUT
PRINT @MESSAGE
SELECT * FROM  [dbo].[USERS]
*/
-- =============================================
ALTER PROCEDURE [dbo].[USP_INSERT_USER]
				 @NAME VARCHAR(200),
				 @EMAIL VARCHAR(100),
				 @USER_NAME VARCHAR(100),
				 @PASSWORD VARCHAR(100),
				 @ROLE VARCHAR(100),
				 @RESULT INT OUT,
				 @MESSAGE VARCHAR(MAX) OUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		IF EXISTS (SELECT U.USER_NAME FROM [dbo].[USERS] U WHERE U.USER_NAME LIKE @USER_NAME)
		BEGIN 
			SET @RESULT = 0;
			SET @MESSAGE = 'USER NAME NOT AVAILABLE'

			RETURN @RESULT;
		END

		IF EXISTS (SELECT U.EMAIL FROM [dbo].[USERS] U WHERE U.EMAIL LIKE @EMAIL)
		BEGIN 
			SET @RESULT = 0;
			SET @MESSAGE = 'EMAIL ALREADY REGISTERED'

			RETURN @RESULT;
		END

		BEGIN TRY  
			INSERT INTO [dbo].[USERS]([NAME],[EMAIL],[USER_NAME],[PASSWORD],[ROLE])
			VALUES(@NAME, @EMAIL, @USER_NAME,@PASSWORD, @ROLE)

			SET @RESULT = 1;
			SET @MESSAGE = 'NEW USER CREATED'

			RETURN @RESULT;

		END TRY  
		BEGIN CATCH  
			
			SET @RESULT = -1;
			SET @MESSAGE =  Error_Message(); 

			RETURN @RESULT;

		END CATCH    
END
