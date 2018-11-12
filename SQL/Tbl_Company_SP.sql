CREATE PROCEDURE [dbo].[UserSP_InsertCompany]
    -- Add the parameters for the stored procedure here   
    @Name varchar(200),
	@CompanyABN_CAN nchar(10),
	@Description varchar(800),
	@URL varchar(200)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

        INSERT INTO [CMSDb].[dbo].[Tbl_Company]([Name],[CompanyABN_CAN],[Description],[URL])
        VALUES(@Name, @CompanyABN_CAN,@Description,@URL)

    SELECT SCOPE_IDENTITY() AS ID

END

go

CREATE PROCEDURE [dbo].[UserSP_UpdateCompany]
    -- Add the parameters for the stored procedure here
    @ID int,
   @Name varchar(200),
	@CompanyABN_CAN nchar(10),
	@Description varchar(800),
	@URL varchar(200)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
	INSERT INTO [CMSDb].[dbo].[Tbl_Company]([Name],[CompanyABN_CAN],[Description],[URL])
        VALUES(@Name, @CompanyABN_CAN,@Description,@URL)
		if exists(select ID from [CMSDb].[dbo].[Tbl_Company] where [ID]=@ID )
		begin
			Update [CMSDb].[dbo].[Tbl_Company] 
			set [Name] = @Name,[CompanyABN_CAN] = @CompanyABN_CAN,[Description]=@Description,[URL]=@URL
			where [ID] = @ID;
		end
END

GO 
CREATE PROCEDURE [dbo].[UserSP_DeleteCompany]
    -- Add the parameters for the stored procedure here
    @ID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
	if exists(select ID from [CMSDb].[dbo].[Tbl_Company] where [ID]=@ID )
	begin
		DELETE FROM[dbo].[Tbl_Company]
		where ID = @ID
	end

END