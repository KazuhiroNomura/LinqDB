select case when 1=0    and 1=0    then 'true'else 'false' end [and],case when 1=0    or 1=0    then 'true'else 'false' end [or] union all
select case when 1=0    and 1=1    then 'true'else 'false' end [and],case when 1=0    or 1=1    then 'true'else 'false' end [or] union all
select case when 1=1    and 1=0    then 'true'else 'false' end [and],case when 1=1    or 1=0    then 'true'else 'false' end [or] union all
select case when 1=1    and 1=1    then 'true'else 'false' end [and],case when 1=1    or 1=1    then 'true'else 'false' end [or] union all
select case when 1=0    and 1=NULL then 'true'else 'false' end [and],case when 1=0    or 1=NULL then 'true'else 'false' end [or] union all
select case when 1=1    and 1=null then 'true'else 'false' end [and],case when 1=1    or 1=null then 'true'else 'false' end [or] union all
select case when 1=null and 1=0    then 'true'else 'false' end [and],case when 1=null or 1=0    then 'true'else 'false' end [or] union all
select case when 1=null and 1=1    then 'true'else 'false' end [and],case when 1=null or 1=1    then 'true'else 'false' end [or] union all
select case when 1=null and 1=null then 'true'else 'false' end [and],case when 1=null or 1=null then 'true'else 'false' end [or]  
