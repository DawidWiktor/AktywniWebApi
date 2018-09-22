USE [AktywniDB]
GO
/****** Object:  StoredProcedure [dbo].[SelectLastMessageEvent]    Script Date: 22.09.2018 21:12:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[SelectLastMessageEvent]
	 @myId int
	,@eventId int
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
				AND UserToId = @myId
				AND EventID = @eventId
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
		