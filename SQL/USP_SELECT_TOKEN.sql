USE [PROJECT]
GO
/****** Object:  StoredProcedure [dbo].[USP_SELECT_TOKEN]    Script Date: 30/12/2022 20:34:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[USP_SELECT_TOKEN]
				 @TOKEN VARCHAR(MAX) = NULL,
				 @USERNAME VARCHAR(200) = NULL


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[TOKEN] T WHERE T.TOKEN LIKE @TOKEN OR @TOKEN IS NULL AND
										T.USERNAME LIKE @USERNAME OR @USERNAME IS NULL
END
