CREATE PROCEDURE [DatabaseAdministration].[uspCreateReferenceTable]
	@schemaName VARCHAR(128)
	,@tableName VARCHAR(128)
	,@dataType VARCHAR(128)
	,@link NVARCHAR(MAX)
AS
BEGIN	
	-- If the table doesn't already exist...
	IF OBJECT_ID(@schemaName + '.' + @tableName) IS NULL
	BEGIN
		DECLARE @tsql NVARCHAR(MAX) = '
		CREATE TABLE [' + @schemaName + '].[' + @tableName + '](
			[InternalKey] [SMALLINT] NOT NULL
			,[DateTimeCreated] [DATETIME] NOT NULL
			,[DateTimeUpdated] [DATETIME] NULL
			,[DateTimeDeleted] [DATETIME] NULL
			,[ValidFromDate] [DATE] NOT NULL
			,[ValidToDate] [DATE] NOT NULL
			,[NationalCode] ' + @dataType + ' NOT NULL
			,[NationalDescription] [VARCHAR](500) NOT NULL
			,[ShortDescription] [VARCHAR](50) NOT NULL
			,CONSTRAINT [PK_' + @tableName + '] PRIMARY KEY CLUSTERED (
				[InternalKey] ASC
			) WITH (
				PAD_INDEX = OFF
				,STATISTICS_NORECOMPUTE = OFF
				,IGNORE_DUP_KEY = OFF
				,ALLOW_ROW_LOCKS = ON
				,ALLOW_PAGE_LOCKS = ON
				,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
			) ON [PRIMARY]
		) ON [PRIMARY]
		'

		EXEC sp_executesql @stmt = @tsql

		SET @tsql = '
		ALTER TABLE [' + @schemaName + '].[' + @tableName + ']
		ADD CONSTRAINT [DF_' + @tableName + '_DateTimeCreated] DEFAULT (GETDATE()) 
		FOR [DateTimeCreated]
		'

		EXEC sp_executesql @stmt = @tsql

		SET @tsql = '
		ALTER TABLE [' + @schemaName + '].[' + @tableName + '] 
		ADD CONSTRAINT [DF_' + @tableName + '_ValidFromDate] DEFAULT (''1900-01-01'') 
		FOR [ValidFromDate]
		'

		EXEC sp_executesql @stmt = @tsql

		SET @tsql = '
		ALTER TABLE [' + @schemaName + '].[' + @tableName + '] 
		ADD CONSTRAINT [DF_' + @tableName + '_ValidToDate] DEFAULT (''9999-12-31'') 
		FOR [ValidToDate]
		'

		EXEC sp_executesql @stmt = @tsql

		EXEC sys.sp_addextendedproperty 
			@name = N'MS_Description'
			,@value = @link
			,@level0type = N'SCHEMA'
			,@level0name = @schemaName
			,@level1type = N'TABLE'
			,@level1name = @tableName

		SET @tsql = '
		CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueRow] ON [' + @schemaName + '].[' + @tableName + '] (
			[NationalCode] ASC,
			[ValidFromDate] ASC,
			[ValidToDate] ASC
		) INCLUDE (
			[DateTimeCreated]
			,[DateTimeUpdated]
			,[DateTimeDeleted]
			,[NationalDescription]
			,[ShortDescription]
		) 
		'

		EXEC sp_executesql @stmt = @tsql
	END
END