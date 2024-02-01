
declare @bit              bit             = 1
declare @tinyint          tinyint         = 2
declare @smallint         smallint        = 3
declare @int              int             = 4
declare @bigint           bigint          = 5
declare @real             real            =11
declare @float            float           =12
declare @money            money           = 6
declare @decimal          decimal         = 9
declare @numeric          numeric         =10
declare @binary           binary          =11
declare @varbinary        varbinary       =12
declare @char             char            ='a'
declare @nchar            nchar           ='b'
declare @varchar          char            ='c'
declare @nvarchar         nchar           ='d'
declare @date             date            ='2001/01/01'
declare @datetime         datetime        ='2002/02/02'
declare @smalldatetime    smalldatetime   ='2003/03/03'
declare @timestamp        timestamp       =convert(timestamp,'2004/04/04')
declare @uniqueidentifier uniqueidentifier='1AAAAAAA-BBBB-CCCC-DDDD-3EEEEEEEEEEE"'
select @binary=convert(binary,@date)
select @binary
      select convert(bit,@bit     ),convert(tinyint,@bit     ),convert(smallint,@bit     ),convert(int,@bit     ),convert(bigint,@bit     ),convert(real,@bit     ),convert(float,@bit     ),convert(money,@bit     ),convert(decimal,@bit     ),convert(numeric,@bit     ),convert(binary,@bit     ),convert(varbinary,@bit     ),convert(char,@bit     ),convert(nchar,@bit     ),convert(varchar,@bit     ),convert(nvarchar,@bit     )
union select convert(bit,@tinyint ),convert(tinyint,@tinyint ),convert(smallint,@tinyint ),convert(int,@tinyint ),convert(bigint,@tinyint ),convert(real,@tinyint ),convert(float,@tinyint ),convert(money,@tinyint ),convert(decimal,@tinyint ),convert(numeric,@tinyint ),convert(binary,@tinyint ),convert(varbinary,@tinyint ),convert(char,@tinyint ),convert(nchar,@tinyint ),convert(varchar,@tinyint ),convert(nvarchar,@tinyint )
union select convert(bit,@smallint),convert(tinyint,@smallint),convert(smallint,@smallint),convert(int,@smallint),convert(bigint,@smallint),convert(real,@smallint),convert(float,@smallint),convert(money,@smallint),convert(decimal,@smallint),convert(numeric,@smallint),convert(binary,@smallint),convert(varbinary,@smallint),convert(char,@smallint),convert(nchar,@smallint),convert(varchar,@smallint),convert(nvarchar,@smallint)
union select convert(bit,@int     ),convert(tinyint,@int     ),convert(smallint,@int     ),convert(int,@int     ),convert(bigint,@int     ),convert(real,@int     ),convert(float,@int     ),convert(money,@int     ),convert(decimal,@int     ),convert(numeric,@int     ),convert(binary,@int     ),convert(varbinary,@int     ),convert(char,@int     ),convert(nchar,@int     ),convert(varchar,@int     ),convert(nvarchar,@int     )
union select convert(bit,@bigint  ),convert(tinyint,@bigint  ),convert(smallint,@bigint  ),convert(int,@bigint  ),convert(bigint,@bigint  ),convert(real,@bigint  ),convert(float,@bigint  ),convert(money,@bigint  ),convert(decimal,@bigint  ),convert(numeric,@bigint  ),convert(binary,@bigint  ),convert(varbinary,@bigint  ),convert(char,@bigint  ),convert(nchar,@bigint  ),convert(varchar,@bigint  ),convert(nvarchar,@bigint  )

      select convert(binary,@binary),convert(varbinary,@varbinary),convert(char,@bit     ),convert(varchar,@bit     ),convert(nchar,@bit     ),convert(nvarchar,@bit     ),convert(float,@bit     ),convert(money,@bit     ),convert(decimal,@bit     ),convert(numeric,@bit     ),convert(binary,@bit     ),convert(varbinary,@bit     ),convert(char,@bit     ),convert(nchar,@bit     ),convert(varchar,@bit     ),convert(nvarchar,@bit     )
union select convert(binary,@binary),convert(varbinary,@varbinary),convert(char,@tinyint ),convert(varchar,@tinyint ),convert(nchar,@tinyint ),convert(nvarchar,@tinyint ),convert(float,@tinyint ),convert(money,@tinyint ),convert(decimal,@tinyint ),convert(numeric,@tinyint ),convert(binary,@tinyint ),convert(varbinary,@tinyint ),convert(char,@tinyint ),convert(nchar,@tinyint ),convert(varchar,@tinyint ),convert(nvarchar,@tinyint )
union select convert(binary,@binary),convert(varbinary,@varbinary),convert(char,@smallint),convert(varchar,@smallint),convert(nchar,@smallint),convert(nvarchar,@smallint),convert(float,@smallint),convert(money,@smallint),convert(decimal,@smallint),convert(numeric,@smallint),convert(binary,@smallint),convert(varbinary,@smallint),convert(char,@smallint),convert(nchar,@smallint),convert(varchar,@smallint),convert(nvarchar,@smallint)
union select convert(binary,@binary),convert(varbinary,@varbinary),convert(char,@int     ),convert(varchar,@int     ),convert(nchar,@int     ),convert(nvarchar,@int     ),convert(float,@int     ),convert(money,@int     ),convert(decimal,@int     ),convert(numeric,@int     ),convert(binary,@int     ),convert(varbinary,@int     ),convert(char,@int     ),convert(nchar,@int     ),convert(varchar,@int     ),convert(nvarchar,@int     )
union select convert(binary,@binary),convert(varbinary,@varbinary),convert(char,@bigint  ),convert(varchar,@bigint  ),convert(nchar,@bigint  ),convert(nvarchar,@bigint  ),convert(float,@bigint  ),convert(money,@bigint  ),convert(decimal,@bigint  ),convert(numeric,@bigint  ),convert(binary,@bigint  ),convert(varbinary,@bigint  ),convert(char,@bigint  ),convert(nchar,@bigint  ),convert(varchar,@bigint  ),convert(nvarchar,@bigint  )
