declare @byte     binary(2)=0x0001;
declare @tinyint  tinyint  =1
declare @binary2  binary(2)=0x0100;
declare @smallint smallint =0x0100;
declare @binary4  binary(4)=0x01000000;
declare @int      int      =0x01000000;
declare @binary8  binary(8)=0x0100000000000000;
select @byte&@tinyint,@binary2&@smallint,@binary4&@int,@binary8&@int
