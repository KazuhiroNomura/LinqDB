declare @t table(
	a int,b int,c int
)
insert into @t(b,c,a)
select 1,1,1
select t.a from @t t

