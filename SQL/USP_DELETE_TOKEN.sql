USE [PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[USP_DELETE_TOKEN]    Script Date: 30/12/2022 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[USP_DELETE_TOKEN]
				 @USERNAME VARCHAR(100) = NULL,
				 @TOKEN VARCHAR(Max) = NULL,
				 @RESULT INT OUT,
				 @MESSAGE VARCHAR(MAX) OUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	 
	 IF @USERNAME IS NULL AND @TOKEN IS NULL
		BEGIN 
			SET @RESULT = 0;
			SET @MESSAGE = 'PLEASE SEND THE USERNAME OR TOKEN'

			RETURN @RESULT;
		END  

	 BEGIN TRY  
		DELETE FROM dbo.TOKEN  
			WHERE  (USERNAME LIKE @USERNAME OR @USERNAME IS NULL) AND
				   (TOKEN LIKE @TOKEN OR @TOKEN IS NULL)
		
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
