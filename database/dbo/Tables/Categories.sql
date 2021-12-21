create table dbo.Categories
(
	Id int not null identity(1,1) constraint PK_Categories primary key,
	Name varchar(64) not null,
	Description varchar(max) null
);
