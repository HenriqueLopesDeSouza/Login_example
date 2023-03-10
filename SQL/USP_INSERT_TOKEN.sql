USE [PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[USP_INSERT_TOKEN]    Script Date: 30/12/2022 20:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HENRIQUE SOUZA
-- Create date: 29/12/2022
-- Description:	NONE
-- =============================================
ALTER PROCEDURE [dbo].[USP_INSERT_TOKEN]
				 @TOKEN VARCHAR(Max),
				 @USER_NAME VARCHAR(100),
				 @RESULT INT OUT,
				 @MESSAGE VARCHAR(MAX) OUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

			BEGIN TRY  

				INSERT INTO [dbo].[TOKEN]([TOKEN],[USERNAME])
				VALUES(@TOKEN,@USER_NAME)

				SET @RESULT = 1;
				SET @MESSAGE = 'OK'

				RETURN @RESULT;

    		END TRY 
			BEGIN CATCH  
				SET @RESULT = -1;
				SET @MESSAGE =  Error_Message(); 

				RETURN @RESULT;
			END CATCH    


END
