
CREATE TABLE QueryHead
(
	TableID int NOT NULL PRIMARY KEY,
	tbTable nchar(100) NOT NULL UNIQUE,
	tbNickName nchar(100) NOT NULL DEFAULT '',
	UpdateDate timestamp NOT NULL DEFAULT current_timestamp
);



CREATE TABLE QueryDetail
(
	TableID int NOT NULL REFERENCES QueryHead(TableID),
	QueryID int NOT NULL,
	QueryName nchar(100) NOT NULL,
	ColumnID int NOT NULL,
	colColumn nchar(100) NOT NULL,
	colNickName nchar(100) NOT NULL DEFAULT '',
	colOutput int NULL,
	colConditions nchar(100) NULL,
	colSortType nchar(100) NULL,
	colSortOrder int NULL,
	UpdateDate timestamp NOT NULL DEFAULT current_timestamp,
	PRIMARY KEY(TableID,QueryID,ColumnID)
) ;


CREATE VIEW  v_QueryDetail
AS
SELECT h.TableID, h.tbTable, h.tbNickName, d.QueryID, d.QueryName, d.ColumnID,
	   d.colColumn, d.colNickName, d.colOutput, d.colConditions, d.colSortType, d.colSortOrder
FROM QueryDetail AS d INNER JOIN QueryHead AS h ON d.TableID = h.TableID
