create database [CMSDb]

USE [CMSDb]
GO

/****** Object:  Table [dbo].[Tbl_Company]    Script Date: 11/13/2018 5:37:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tbl_Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[CompanyABN_CAN] [nchar](10) NOT NULL,
	[Description] [varchar](800) NULL,
	[URL] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Tbl_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [CMSDb]
GO

/****** Object:  Table [dbo].[Tbl_Title]    Script Date: 11/13/2018 5:37:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tbl_Title](
	[ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Title] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
insert into [dbo].[Tbl_Title] values('Mr.');
insert into [dbo].[Tbl_Title] values('Mrs.')
GO

/****** Object:  Table [dbo].[Tbl_Contact]    Script Date: 11/13/2018 5:36:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
USE [CMSDb]
CREATE TABLE [dbo].[Tbl_Contact](
	[ID] [int] NOT NULL,
	[TitleId] [tinyint] NOT NULL,
	[FirstName] [varchar](200) NOT NULL,
	[LastName] [varchar](200) NOT NULL,
	[ContractType] [tinyint] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[PhoneNo] [nchar](20) NOT NULL,
	[Department] [varchar](100) NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tbl_Contact]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Contact_Tbl_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Tbl_Company] ([ID])
GO

ALTER TABLE [dbo].[Tbl_Contact] CHECK CONSTRAINT [FK_Tbl_Contact_Tbl_Company]
GO

ALTER TABLE [dbo].[Tbl_Contact]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Contact_Tbl_Title] FOREIGN KEY([TitleId])
REFERENCES [dbo].[Tbl_Title] ([ID])
GO

ALTER TABLE [dbo].[Tbl_Contact] CHECK CONSTRAINT [FK_Tbl_Contact_Tbl_Title]
GO


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


create PROCEDURE [dbo].[UserSP_GetCompanyByName]
(
	@SearchNameValue NVARCHAR(200) = NULL,
	@SearchUrlValue NVARCHAR(50) = NULL
	
)
 AS BEGIN
 SET NOCOUNT ON;

 SET @SearchNameValue = LTRIM(RTRIM(@SearchNameValue))
  SET @SearchUrlValue = LTRIM(RTRIM(@SearchUrlValue))
  
    SELECT [ID],[Name],[CompanyABN_CAN],[Description],[URL] from [dbo].[Tbl_Company] 
	WHERE (@SearchNameValue IS NULL OR [Name] LIKE '%' + @SearchNameValue + '%')  and (@SearchUrlValue IS NULL OR [URL] LIKE '%' + @SearchUrlValue + '%')
	 	    ORDER BY [Name] ASC
   	  
      


   END
GO