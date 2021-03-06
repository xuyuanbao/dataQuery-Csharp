
CREATE TABLE [dbo].[QueryHead](
	[TableID] int NOT NULL PRIMARY KEY,
	[tbTable] [nchar](100) NOT NULL UNIQUE,
	[tbNickName] [nchar](100) NOT NULL DEFAULT(''),
	[UpdateDate] [datetime] NOT NULL DEFAULT (getdate()),
) ON [PRIMARY];


CREATE TABLE [dbo].[QueryDetail](
	[TableID] int NOT NULL REFERENCES QueryHead([TableID]),
	[QueryID] int NOT NULL,
	[QueryName] [nchar](100) NOT NULL,
	[ColumnID] int NOT NULL,
	[colColumn] [nchar](100) NOT NULL,
	[colNickName] [nchar](100) NOT NULL DEFAULT(''),
	[colOutput] int NOT NULL DEFAULT(''),
	[colConditions] [nchar](100) NOT NULL DEFAULT(''),
	[colSortType] [nchar](100) NOT NULL DEFAULT(''),
	[colChart] [nchar](100) NOT NULL DEFAULT(''),
	[UpdateDate] [datetime] NOT NULL DEFAULT (getdate()),
	PRIMARY KEY([TableID],[QueryID],[ColumnID])
) ON [PRIMARY];


CREATE VIEW [dbo].[v_QueryDetail]
AS
SELECT h.[TableID], h.[tbTable], h.[tbNickName], d.[QueryID], d.[QueryName], d.[ColumnID],
	   d.[colColumn], d.[colNickName], d.[colOutput], d.[colConditions], d.[colSortType], d.[colChart] 
FROM dbo.[QueryDetail] AS d INNER JOIN dbo.[QueryHead] AS h ON d.[TableID] = h.[TableID];
