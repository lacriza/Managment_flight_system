-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ada Salii
-- Create date: 11 Aug 2021
-- Description:	Insert Flight to Flight table
-- =============================================
CREATE PROCEDURE stpInsertFlight  
	@flightNo varchar(10),
	@depDate datetimeoffset(7),
	@arrDate datetimeoffset(7),
	@depIATA char(3),
	@type char(3),
	@arrIATA int,
	@basePrice money,
	@total money
AS
BEGIN
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	INSERT INTO [dbo].[Flight]
           ([flightNumber]
           ,[departureDateTime]
           ,[arrivalDateTime]
           ,[arrivalAirport]
           ,[departureAirport]
           ,[flightType]
           ,[basePriceNIS]
           ,[totalPriceNIS])
     VALUES
           (@flightNo, @depDate, @arrDate, @arrIATA, @depIATA, @type, @basePrice, @total)
END
GO
