USE [AktywniDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure InsertMessage
	 @userFromId int
	,@userId int
	,@date Datetime
	,@content nvarchar(MAX)
as
	DECLARE @message TABLE (messageId int)

	INSERT INTO Messages(Date, Content)
	OUTPUT Inserted.MessageID INTO @message(messageId)
	VALUES (@date, @content)


	INSERT INTO MessageUser(UserID, MessageID, IsOpened, UserFromID)
	VALUES (@userId, (select * from @message), 0, @userFromId)