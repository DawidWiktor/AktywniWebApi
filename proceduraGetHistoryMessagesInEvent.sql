USE [AktywniDB]
GO
/****** Object:  StoredProcedure [dbo].[GetHistoryMessagesInEvent]    Script Date: 22.09.2018 18:25:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[GetHistoryMessagesInEvent]
	 @myId int
	,@eventId int
	,@latestMessageId int 
as
	DROP TABLE IF EXISTS #TempMessages
	DECLARE @messages TABLE (messageId int, eventId int, userFromId int)
	Select TOP 10
		 me.MessageID
		,me.EventID 
		,me.UserFromID 
		Into #TempMessages 
		From MessageEvent me 
		Where me.EventID = @eventId
			and me.UserToId = @myId 
			and me.MessageID < @latestMessageId
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
			UPDATE MessageEvent
				SET IsOpened = 1
				Where MessageID = @counterMessageId
				AND UserFromID = @myId
			FETCH NEXT FROM MY_CURSOR INTO @counterMessageId
		END
		CLOSE MY_CURSOR
		DEALLOCATE MY_CURSOR
		
	Select tmp.EventID, 
			tmp.MessageID,  
			ev.Name, 
			usr.Login, 
			ms.Date, 
			ms.Content
		From #TempMessages tmp
			JOIN Users usr on tmp.UserFromID = usr.UserID
			Join Messages ms on tmp.MessageID = ms.MessageID
			Join Events ev on tmp.EventID = ev.EventID
		