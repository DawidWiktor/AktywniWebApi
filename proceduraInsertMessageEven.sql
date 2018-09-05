USE [AktywniDB]
GO
/****** Object:  StoredProcedure [dbo].[InsertMessageEvent]    Script Date: 04.09.2018 21:18:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SelectMessageEvent]
	 @myId int
	,@eventId int
as
	DECLARE @messages TABLE (messageId int, eventId int, userFromId int)

	Select 
		 me.MessageID
		,me.EventID 
		,me.UserFromID 
		Into #TempMessages 
		From MessageEvent me 
		Where me.EventID = @eventId
			and me.UserToId = @myId 

	
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
			FETCH NEXT FROM MY_CURSOR INTO @counterMessageId
		END
		CLOSE MY_CURSOR
		DEALLOCATE MY_CURSOR
	

	Select tmp.EventID, tmp.MessageID,  
		From #TempMessages tmp
			JOIN Users usr on tmp.UserFromID = usr.UserID
			Join Messages ms on tmp.MessageID = ms.MessageID
		
