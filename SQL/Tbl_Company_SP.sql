Use CMSDb

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

create PROCEDURE [dbo].[UserSP_UpdateCompany]
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

GO 
CREATE PROCEDURE [dbo].[UserSP_CompanyById]
    -- Add the parameters for the stored procedure here
    @ID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
	if exists(select ID from [CMSDb].[dbo].[Tbl_Company] where [ID]=@ID )
	begin
		select *  FROM[dbo].[Tbl_Company]
		where [ID] = @ID
	end

END
Go 
create PROCEDURE [dbo].[UserSP_GetCompany]
(
	@SearchValue NVARCHAR(50) = NULL,
	@PageNo INT = 1,
	@PageSize INT = 10,
	@SortColumn NVARCHAR(20) = 'Name',
	@SortOrder NVARCHAR(20) = 'ASC'
)
 AS BEGIN
 SET NOCOUNT ON;

 SET @SearchValue = LTRIM(RTRIM(@SearchValue))

 ; WITH CTE_Results AS 
(
    SELECT [ID],[Name],[CompanyABN_CAN],[Description],[URL] from [dbo].[Tbl_Company] 
	WHERE (@SearchValue IS NULL OR [Name] LIKE '%' + @SearchValue + '%') 
	 	    ORDER BY
   	 CASE WHEN (@SortColumn = 'Name' AND @SortOrder='ASC')
                    THEN [Name]
        END ASC,
        CASE WHEN (@SortColumn = 'Name' AND @SortOrder='DESC')
                   THEN [Name]
		END DESC,
	 CASE WHEN (@SortColumn = 'Code' AND @SortOrder='ASC')
                    THEN [CompanyABN_CAN]
        END ASC,
        CASE WHEN (@SortColumn = 'Code' AND @SortOrder='DESC')
                   THEN [CompanyABN_CAN]
		END DESC 
      OFFSET @PageSize * (@PageNo - 1) ROWS
      FETCH NEXT @PageSize ROWS ONLY
	),
CTE_TotalRows AS 
(
 select count([ID]) as MaxRows from [dbo].[Tbl_Company]  WHERE (@SearchValue IS NULL OR [Name] LIKE '%' + @SearchValue + '%')
)
   Select CAST( MaxRows AS int) as MaxRows, CAST( t.ID AS int) as ID,  CAST( t.[Name] as nvarchar(200))as [Name], CAST( t.[CompanyABN_CAN] as nchar(10))as [CompanyABN_CAN], CAST(t.[Description]as varchar(800)) as [Description],CAST (t.[URL] as Varchar(200)) As [URL] from [dbo].[Tbl_Company]  as t, CTE_TotalRows 
   WHERE EXISTS (SELECT 1 FROM CTE_Results WHERE CTE_Results.ID = t.ID)


   END
GO


--CREATE PROCEDURE [dbo].[UserSP_GetCompany]
-- @page INT,
-- @size INT,
-- @sort nvarchar(50),
-- @SearchString nvarchar(50),
-- @totalrow INT  OUTPUT
--AS
--BEGIN
--    DECLARE @offset INT
--    DECLARE @newsize INT
--    DECLARE @sql NVARCHAR(MAX)

--    IF(@page=0)
--       begin
--            SET @offset = @page
--            SET @newsize = @size
--       end
--    ELSE 
--      begin
--        SET @offset = @page+1
--        SET @newsize = @size-1
--     end
--     SET NOCOUNT ON
--     SET @sql = '
--     WITH OrderedSet AS
--    (
--      SELECT *, ROW_NUMBER() OVER (ORDER BY ' + @sort + ') AS ''Index''
--      FROM [dbo].[Mk] 
--    )
--   SELECT * FROM OrderedSet WHERE [Index] BETWEEN ' + CONVERT(NVARCHAR(12), @offset) + ' AND ' + CONVERT(NVARCHAR(12), (@offset + @newsize)) 
--   EXECUTE (@sql)
--   SET @totalrow = (SELECT COUNT(*) FROM [dbo].[Tbl_Company])
--END