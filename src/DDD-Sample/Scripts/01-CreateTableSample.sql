use SampleDB
GO

create table Sample(
	Id uniqueidentifier not null,
	Name nvarchar(50) not null,
	SomeDate datetime null,
	constraint PK_Sample primary key (Id)
)
go