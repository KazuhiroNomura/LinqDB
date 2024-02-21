use TableFunction
truncate table [MERGE]
declare @i int=0
while @i<10 begin
	insert into [MERGE](text)VALUES
	('merge'+cast(@i as nchar(10)));
	set @i=@i+1
end
merge into [MERGE] as M
using(
	select I.ID I_ID,U.ID U_ID,D.ID D_ID,I.[text] I_TEXT,U.[text] U_TEXT
	from [INSERT]I
	full join [UPDATE]U ON I.ID=U.ID
	full join [DELETE]D ON I.ID=D.ID OR U.ID=D.ID
)as T
on M.ID=T.U_ID
when matched then
	update set M.text=T.U_TEXT,‘€ì='U'
WHEN NOT MATCHED BY TARGET
	THEN insert (text,‘€ì)values(T.I_TEXT,'I')
WHEN NOT MATCHED BY SOURCE AND SOURCE.I_ID%2=0
	THEN DELETE;
