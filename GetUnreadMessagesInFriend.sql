USE [AktywniDB]
GO
/****** Object:  StoredProcedure [dbo].[SelectLastMessageEvent]    Script Date: 05.09.2018 22:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[GetUnreadMessagesInFriend]
	 @myId int
	,@friendId int
as
	DROP TABLE IF EXISTS #TempMessages
	DECLARE @messages TABLE (messageId int, userFrom int, userID int)
	Select TOP 10
		 me.UserFromID
		,me.UserID
		,me.MessageID 
		Into #TempMessages 
		From MessageUser me 
		Where 
			   ((me.UserFromID = @myId and me.UserID = @friendId) 
			or (me.UserFromID = @friendId and me.UserID = @myId)) 
			and me.IsOpened = 0
		Order by me.MessageID desc

	
		DECLARE @counterMessageId int
		
		DECLARE MY_CURSOR CURSOR 
		  LOCAL STATIC READ_ONLY FORWARD_ONLY
		FOR 

		Select tmp.MessageID from #TempMessages tmp

		OPEN MY_CURSOR
		FETCH NEXT FROM MY_CURSOR INTO @counterMessageId
		WHILE @@FETCH_STATUS = 0
		BEGIN 
			UPDATE MessageUser
				SET IsOpened = 1
				Where MessageID = @counterMessageId
			FETCH NEXT FROM MY_CURSOR INTO @counterMessageId
		END
		CLOSE MY_CURSOR
		DEALLOCATE MY_CURSOR
		
	Select tmp.UserFromID, 
			tmp.UserID,  
			tmp.MessageID, 
			ms.Date, 
			ms.Content
		From #TempMessages tmp
			Join Messages ms on tmp.MessageID = ms.MessageID
		