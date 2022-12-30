USE [PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[USP_RETURN_PASSWORD_AND_SALT]    Script Date: 30/12/2022 20:34:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HENRIQUE SOUZA
-- Create date: 28/12/2022 - dd/mm/yyyy
-- Description:	NONE
-- =============================================
ALTER PROCEDURE [dbo].[USP_RETURN_PASSWORD_AND_SALT]
				 @USER_NAME VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[USERS] WHERE USER_NAME LIKE @USER_NAME
END
