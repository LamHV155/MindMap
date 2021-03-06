CREATE DATABASE MINDMAP
GO

USE MINDMAP
GO

CREATE TABLE BOARD
(
	ID INT PRIMARY KEY,
	WIDTH INT NOT NULL,
	HEIGHT INT NOT NULL,
	COLOR VARCHAR(10) NOT NULL
)

CREATE TABLE TOPIC
(
	ID INT PRIMARY KEY,
	ID_BOARD INT FOREIGN KEY REFERENCES BOARD(ID) NOT NULL,
	ID_PARENT INT NULL,
	LABEL_TP NVARCHAR(100) NOT NULL,
	POS_X INT NOT NULL,
	POS_Y INT NOT NULL,
	WIDTH INT NOT NULL,
	HEIGHT INT NOT NULL,
	SIZE FLOAT NOT NULL,
	BACKCOLOR VARCHAR(10) NOT NULL,
	FORECOLOR VARCHAR(10) NOT NULL,
	SHAPE VARCHAR(15) NOT NULL,
	TEXT_SIZE INT NOT NULL,
	FONT VARCHAR(20) NOT NULL,
	STYLE_PATH VARCHAR(15) NOT NULL,
	COLOR_PATH VARCHAR(10) NOT NULL,
	SIZE_PATH INT NOT NULL
	
)


CREATE TABLE MENU
(
	ID INT PRIMARY KEY IDENTITY,
	COLOR_BOARD VARCHAR(10) NOT NULL,
	SHAPE_PARENT VARCHAR(15) NOT NULL,
	SHAPE_CHILD VARCHAR(15) NOT NULL,
	COLOR_PARENT VARCHAR(10) NOT NULL,
	COLOR_CHILD VARCHAR(10) NOT NULL,
	COLOR_PATH VARCHAR(10) NOT NULL,
	STYLE_PATH VARCHAR(15) NOT NULL
)

CREATE TABLE STORAGE
(
	NAME_S VARCHAR(100),
	ID_BOARD INT FOREIGN KEY REFERENCES BOARD(ID),
	DATE_MODIFIED DATETIME,
	PRIMARY KEY(NAME_S, ID_BOARD)
)




INSERT INTO MENU VALUES
('#FFFFFF','Rectangle','Rectangle','#EE4000','#D3D3D3','#000000','Line'),
('#ADD8E6','Rectangle','Rectangle','#FFFFE0','#C0C0C0','#000000','Curve'),
('#FFFFE0','Rectangle','Rectangle','#CD8162','#FFFFFF','#000000','Line'),
('#000000','Rectangle','Rhombus','#20B2AA','#90EE90','#FFFFFF','Curve'),
('#FFC0CB','Rectangle','Rhombus','#87CEFA','#FFFFFF','#000000','Line'),
('#CD853F','Rectangle','Rhombus','#FFFFFF','#FFFFE0','#FFFFFF','Curve'),
('#F08080','Rectangle','Ellipse','#CD8162','#A020F0','#000000','Line'),
('#E0FFFF','Rectangle','Ellipse','#FFE1FF','#FFA500','#000000','Curve'),
('#D3D3D3','Rectangle','Ellipse','#00F5FF','#FFFFFF','#FFFFFF','Line'),
('#FFFFFF','Rectangle','Rectangle','#2D6097','#A3CAEB','#F7D7C8','Curve'),
('#F7FCFD','Rectangle','Rectangle','#7C9AAB','#B4D1E1','#FCD9DD','Line'),
('#ECE3DA','Rhombus','Rhombus','#667894','#75859B','#E8CCC9','Curve'),
('#F7F4F4','Rhombus','Rhombus','#7FA6C9','#CCE1F3','#3F7CA5','Line'),
('#E9E8F2','Rhombus','Rhombus','#8CADD2','#F6D2C9','#EA695F','Curve'),
('#CFE1F7','Ellipse','Ellipse','#F25B67','#F7E8E9','#296FC7','Line'),
('#FDF3F4','Ellipse','Ellipse','#F4ABB4','#FED3DD','#7EABD0','Curve'),
('#ECECF3','Ellipse','Ellipse','#B790A4','#FBC3C8','#336397','Line')
GO