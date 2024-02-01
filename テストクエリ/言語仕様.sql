declare @begin_time DATETIME = NULL
declare @end_time DATETIME = NULL
--set @end_time=default
	select msdb.dbo.agent_datetime(null,default)

